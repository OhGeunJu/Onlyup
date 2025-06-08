using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float rotationSpeed = 10f;
    public float gravity = 20f;
    public float jumpForce = 8f;

    private float yVelocity = 0f;
    private CharacterController controller;
    private Animator animator;
    private Transform cameraTransform;
    private PlayerClimb climb;
    private StandUpHandler standUpHandler;

    private bool justFinishedClimb = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        cameraTransform = Camera.main.transform;
        climb = GetComponent<PlayerClimb>();
        standUpHandler = GetComponent<StandUpHandler>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (climb != null && climb.isClimbing)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(h, 0f, v).normalized;

        bool isInputting = h != 0f || v != 0f;
        bool isRunning = isInputting && Input.GetKey(KeyCode.LeftShift);
        bool isWalking = isInputting;
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 moveDirection = Vector3.zero;

        if (isInputting)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            moveDirection = camForward * inputDir.z + camRight * inputDir.x;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        if (CanJump())
        {
            if (justFinishedClimb)
            {
                justFinishedClimb = false;
                return;
            }

            if (Input.GetButtonDown("Jump"))
            {

                if (isRunning)
                {
                    yVelocity = jumpForce * 1.3f;
                    animator.SetTrigger("RunJump");
                }
                else
                {
                    yVelocity = jumpForce;
                    animator.SetTrigger("Jump");
                }
            }
        }

        if (controller.isGrounded && yVelocity < 0f)
        {
            yVelocity = -1f;
        }
        else if (!controller.isGrounded)
        {
            yVelocity -= gravity * Time.deltaTime;
            yVelocity = Mathf.Max(yVelocity, -25f);
        }

        Vector3 finalMove = moveDirection * currentSpeed;
        finalMove.y = yVelocity;

        if (controller.enabled)
        {
            controller.Move(finalMove * Time.deltaTime);
        }

        animator.SetBool("Walking", isWalking && !isRunning);
        animator.SetBool("Running", isRunning);

        // RunJump 상태에서 착지하고 이동하면 애니메이션 전이
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("RunJump") && controller.isGrounded && isInputting)
        {
            animator.ResetTrigger("RunJump");
            animator.SetBool("Running", isRunning);
            animator.SetBool("Walking", !isRunning);
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null || climb == null) return;

        if (climb.isClimbing) return;

        if (!Input.GetKey(KeyCode.LeftAlt))
        {
            Vector3 camForward = cameraTransform.forward;
            camForward.y = 0f;

            if (camForward.sqrMagnitude < 0.01f)
                return;

            camForward.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(camForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void NotifyClimbFinished()
    {
        justFinishedClimb = true;
        yVelocity = 0f;
    }

    bool IsOnSlope()
    {
        Vector3 rayOrigin = controller.bounds.center - new Vector3(0, controller.height * 0.5f - 0.1f, 0);
        float rayLength = 0.3f;

        Debug.DrawRay(rayOrigin, Vector3.down * rayLength, Color.red);

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, rayLength))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            if (slopeAngle > 0f && slopeAngle <= controller.slopeLimit + 5f)
            {
                return true;
            }
        }

        return false;
    }

    bool CanJump()
    {
        if (controller.isGrounded || IsOnSlope()) return true;

        // 지면과 가까우면 점프 허용
        if (Physics.Raycast(transform.position, Vector3.down, 0.3f)) return true;

        return false;
    }

    public void LaunchByJumpPad(float force)
    {
        // 기존 수직 속도 제거 후 발사
        yVelocity = force;
    }
}