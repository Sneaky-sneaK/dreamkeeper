using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // Assign your player object here
    public Vector3 offset;        // Optional: e.g., new Vector3(0, 5, -10)
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}