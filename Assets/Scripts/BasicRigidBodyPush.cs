using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
    [Header("Push Settings")]
    public LayerMask pushLayers;
    public bool canPush = true;
    [Range(0.5f, 5f)] public float strength = 1.1f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (canPush)
        {
            PushRigidBodies(hit);
        }
    }

    private void PushRigidBodies(ControllerColliderHit hit)
    {
        // Ensure we hit a non-kinematic rigidbody
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;

        // Ensure we only push desired layer(s)
        int bodyLayerMask = 1 << body.gameObject.layer;
        if ((bodyLayerMask & pushLayers.value) == 0) return;

        // Do not push objects below us
        if (hit.moveDirection.y < -0.3f) return;

        // Calculate push direction from move direction, horizontal motion only
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

        // Apply the push and take strength into account
        body.AddForce(pushDir * strength, ForceMode.Impulse);
    }
}
