using UnityEngine;

public class RootMotionMover : MonoBehaviour
{
    private Animator animator;
    private Transform rootTransform;

    void Start()
    {
        animator = GetComponent<Animator>();
        rootTransform = transform.root; // 부모 오브젝트(Player)
    }

    void OnAnimatorMove()
    {
        if (animator.applyRootMotion && rootTransform != null)
        {
            Vector3 deltaPos = animator.deltaPosition;
            Quaternion deltaRot = animator.deltaRotation;

            rootTransform.position += deltaPos;
            rootTransform.rotation *= deltaRot;
        }
    }
}
