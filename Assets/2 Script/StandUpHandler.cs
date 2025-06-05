using UnityEngine;
using System.Collections;

public class StandUpHandler : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;
    private CharacterController controller;

    private bool isStandingUp = false;
    public bool IsStandingUp => isStandingUp;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController>();
    }

    public void BeginStandUp()
    {
        if (isStandingUp) return;

        isStandingUp = true;
        animator.SetTrigger("DoStandUp");
        animator.applyRootMotion = true;

        if (controller != null)
            controller.enabled = false;
    }

    void Update()
    {
        if (!isStandingUp) return;

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        bool isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;

        if (state.IsName("StandUp") && isMoving)
        {
            Debug.Log("이동 입력 → StandUp 인터럽트");
            animator.SetTrigger("InterruptStandUp");
            EndStandUp();
            return;
        }

        if (state.IsName("StandUp") && state.normalizedTime >= 0.95f)
        {
            EndStandUp();
            return;
        }
    }

    private void EndStandUp()
    {
        isStandingUp = false;
        animator.ResetTrigger("DoStandUp");
        animator.SetBool("IsGround", true);
        animator.applyRootMotion = false;

        StartCoroutine(EnableControllerNextFrame());

        if (movement != null)
            movement.NotifyClimbFinished();
    }

    private IEnumerator EnableControllerNextFrame()
    {
        yield return null;
        if (controller != null)
            controller.enabled = true;
    }
}
