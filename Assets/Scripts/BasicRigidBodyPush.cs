using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
    public LayerMask pushLayers;
    public bool canPush;
    [Range(0.5f, 5f)] public float strength = 1.1f;
    private RaycastHit _hit;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (canPush) PushRigidBodies(hit);
        if (canPush) DoorInteract(hit);
    }

    private void PushRigidBodies(ControllerColliderHit hit)
    {
        // https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html

        // make sure we hit a non kinematic rigidbody
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;

        // make sure we only push desired layer(s)
        var bodyLayerMask = 1 << body.gameObject.layer;
        if ((bodyLayerMask & pushLayers.value) == 0) return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f) return;

        // Calculate push direction from move direction, horizontal motion only
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

        // Apply the push and take strength into account
        body.AddForce(pushDir * strength, ForceMode.Impulse);
    }

    private void DoorInteract(ControllerColliderHit hit)
    {
        // make sure we hit a door
        if (hit.transform.GetComponent<Door>())
        {
            // make sure we only interact with desired layer(s)
            var hitLayerMask = 1 << hit.transform.gameObject.layer;
            if ((hitLayerMask & pushLayers.value) == 0) return;

            // check if player presses "E"
            if (Input.GetKeyDown(KeyCode.E))
            {
                // open the door
                hit.transform.GetComponent<Door>().Open();
            }
        }
    }
}
