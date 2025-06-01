using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 10f;
    public float gravity = 20f;
    public float jumpForce = 8f;

    private float yVelocity = 0f;
    private CharacterController controller;
    private Animator animator;
    private Transform cameraTransform;
    private PlayerClimb climb;

    private bool justFinishedClimb = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        cameraTransform = Camera.main.transform;
        climb = GetComponent<PlayerClimb>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Ŭ���̹� ���̸� ���� ����
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

        // �߷� �� ����
        if (controller.isGrounded)
        {
            yVelocity = -1f;

            // ��� �� ù �����ӿ� ���� ����
            if (justFinishedClimb)
            {
                justFinishedClimb = false;
                return;
            }

            else if (Input.GetButtonDown("Jump"))
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
        else
        {
            yVelocity -= gravity * Time.deltaTime;
        }

        Vector3 finalMove = moveDirection * currentSpeed;
        finalMove.y = yVelocity;
        if (controller.enabled)
        {
            controller.Move(finalMove * Time.deltaTime);
        }

        animator.SetBool("Walking", isWalking && !isRunning);
        animator.SetBool("Running", isRunning);
    }

    void LateUpdate()
    {
        if (cameraTransform == null || climb == null) return;

        // ��� �߿��� ȸ������ ����
        if (climb.isClimbing)
            return;

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

    // ��� �����ٴ� ��ȣ�� �ܺο��� ���� �� �ֵ��� ���� �޼���
    public void NotifyClimbFinished()
    {
        justFinishedClimb = true;
        yVelocity = 0f;
    }
}
