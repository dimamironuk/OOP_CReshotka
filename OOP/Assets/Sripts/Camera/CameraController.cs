using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.2f; 
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;

        Vector3 smoothPosition = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );

        transform.position = new Vector3(
            smoothPosition.x,
            smoothPosition.y,
            transform.position.z
        );
    }
}
