using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerClimb : MonoBehaviour
{
    [SerializeField] private float climbCheckDistance = 1.0f;
    [SerializeField] private float maxClimbHeight = 2.3f;
    [SerializeField] private Transform climbLimit; // 기준선 트랜스폼 (높은 장애물인지 판단 기준)
    public LayerMask climbableLayer;

    private CharacterController controller;
    private Animator animator;
    private PlayerMovement movement;
    private StandUpHandler standUpHandler;

    public bool isClimbing { get; private set; }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<PlayerMovement>();
        standUpHandler = GetComponent<StandUpHandler>();
    }

    void Update()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (isClimbing)
        {
            if (state.IsName("Climbing") && state.normalizedTime >= 0.95f)
            {
                isClimbing = false;
                animator.ResetTrigger("Climbing");
                standUpHandler.BeginStandUp();
            }
            return;
        }

        if (standUpHandler != null && standUpHandler.IsStandingUp)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckForClimbable(out RaycastHit hit))
            {
                float ledgeY = hit.collider.bounds.max.y;
                float characterY = controller.bounds.min.y;

                // 기준선보다 낮은 장애물은 점프로 처리 (등반 금지)
                if (climbLimit != null && ledgeY < climbLimit.position.y)
                {
                    Debug.Log("기준선보다 낮은 장애물이므로 등반하지 않음 → 점프로 처리");
                    return;
                }

                if ((ledgeY - characterY) <= maxClimbHeight)
                {
                    Debug.Log("등반 시작");
                    StartClimb(hit);
                }
                else
                {
                    Debug.Log("너무 높아서 등반 불가");
                }
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

    void OnDrawGizmosSelected()
    {
        if (climbLimit != null)
        {
            Gizmos.color = Color.red;
            Vector3 start = new Vector3(-100f, climbLimit.position.y, transform.position.z);
            Vector3 end = new Vector3(100f, climbLimit.position.y, transform.position.z);
            Gizmos.DrawLine(start, end);
        }
    }
}
