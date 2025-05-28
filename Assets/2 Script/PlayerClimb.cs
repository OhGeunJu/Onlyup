using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerClimb : MonoBehaviour
{
    public float climbCheckDistance = 1.0f;
    public float climbHeightOffset = 1.2f;
    public float climbDuration = 1.0f;
    public LayerMask climbableLayer;

    private CharacterController controller;
    private Animator animator;

    public bool isClimbing = false;
    private Vector3 climbStartPos;
    private Vector3 climbEndPos;
    private float climbTimer = 0f;

    public bool IsClimbing => isClimbing;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isClimbing)
        {
            climbTimer += Time.deltaTime;
            float t = climbTimer / climbDuration;
            controller.enabled = false;
            transform.position = Vector3.Lerp(climbStartPos, climbEndPos, t);
            if (t >= 1f)
            {
                isClimbing = false;
                controller.enabled = true;
                animator.SetBool("Climbing", false);
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

    bool CheckForClimbable(out RaycastHit hitInfo)
    {
        Vector3 start = transform.position + Vector3.up * 0.5f;
        Vector3 end = transform.position + Vector3.up * 1.5f;
        float radius = 0.3f;
        Vector3 direction = transform.forward;

        Debug.DrawRay(start, direction * climbCheckDistance, Color.green);

        return Physics.CapsuleCast(start, end, radius, direction, out hitInfo, climbCheckDistance, climbableLayer);
    }

    void StartClimb(RaycastHit hit)
    {
        isClimbing = true;
        climbTimer = 0f;

        climbStartPos = transform.position;
        Vector3 wallOffset = hit.normal * 0.5f; // 벽 방향 반대쪽으로 0.5m
        climbEndPos = hit.point + Vector3.up * climbHeightOffset + wallOffset;

        animator.SetBool("Climbing", true);
    }
}
