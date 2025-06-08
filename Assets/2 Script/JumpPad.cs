using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 15f;
    public string targetTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;

        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.LaunchByJumpPad(jumpForce);
        }
    }
}
