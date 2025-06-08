using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerFall : MonoBehaviour
{
    [SerializeField] private float fallThreshold = 2.5f;

    private float fallStartY;
    private bool isFalling = false;
    private bool hasPlayedFalling = false;

    private CharacterController controller;
    private Animator animator;
    private StandUpHandler standUpHandler;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        standUpHandler = GetComponent<StandUpHandler>();
    }

    void Update()
    {
        HandleFalling();

        // IsGround�� �ڵ� ���� ������
        animator.SetBool("IsGround", controller.isGrounded);
    }

    private void HandleFalling()
    {
        bool grounded = controller.isGrounded;

        if (!grounded)
        {
            if (!isFalling)
            {
                isFalling = true;
                fallStartY = transform.position.y;
                hasPlayedFalling = false;
            }
            else
            {
                float fallDistance = fallStartY - transform.position.y;

                if (fallDistance >= fallThreshold && !hasPlayedFalling)
                {
                    animator.SetTrigger("Falling");
                    hasPlayedFalling = true;
                }
            }
        }
        else
        {
            if (isFalling)
            {
                float totalFall = fallStartY - transform.position.y;
                isFalling = false;
                fallStartY = 0f;
                hasPlayedFalling = false;

                if (totalFall >= fallThreshold && standUpHandler != null)
                {
                    standUpHandler.BeginStandUp(); // ���ĵ�� ���� ����
                }
            }
        }

        // Falling �ִϸ��̼� ���¿��� Ʈ���� �ʱ�ȭ
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Falling"))
        {
            animator.ResetTrigger("Falling");
        }
    }
}
