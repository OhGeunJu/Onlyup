using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerClimb : MonoBehaviour
{
    public float climbCheckDistance = 1.0f;
    public LayerMask climbableLayer;
    private PlayerMovement movement;

    private CharacterController controller;
    private Animator animator;

    public bool isClimbing { get; private set; }
    private bool isStandingUp = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (isClimbing)
        {
            if (state.IsName("Climbing") && state.normalizedTime >= 0.95f)
            {
                isClimbing = false;
                isStandingUp = true;

                animator.ResetTrigger("Climbing");
                animator.SetTrigger("StandUp");
                animator.applyRootMotion = true;
            }
            return;
        }

        if (isStandingUp)
        {
            if (state.IsName("StandUp") && state.normalizedTime >= 0.95f)
            {
                isStandingUp = false;
                animator.ResetTrigger("StandUp");

                animator.applyRootMotion = false;
                controller.enabled = true;

                if (movement != null)
                {
                    movement.NotifyClimbFinished();
                }
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckForClimbable(out RaycastHit hit))
            {
                StartClimb(hit);
            }
        }
    }

    bool CheckForClimbable(out RaycastHit hit)
    {
        Vector3 start = transform.position + Vector3.up * 0.5f;
        Vector3 end = transform.position + Vector3.up * 1.5f;
        Vector3 direction = transform.forward;
        float radius = 0.3f;

        Debug.DrawRay(start, direction * climbCheckDistance, Color.green);
        return Physics.CapsuleCast(start, end, radius, direction, out hit, climbCheckDistance, climbableLayer);
    }

    void StartClimb(RaycastHit hit)
    {
        isClimbing = true;

        transform.rotation = Quaternion.LookRotation(-hit.normal);

        controller.enabled = false;

        animator.applyRootMotion = true;
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("RunJump");
        animator.SetTrigger("Climbing");
    }
}
