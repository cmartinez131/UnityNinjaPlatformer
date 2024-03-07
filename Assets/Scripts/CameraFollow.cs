using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // Reference to player's transform
    public float smoothSpeed = 0.3f; // Smoothness of camera movement
    public Vector3 offset; // Offset from the player's position
    public float lookAheadDistance = 2f; // How far ahead to look based on player's velocity
    public float lookAheadSpeed = 0.5f; // Speed at which camera adjusts its look-ahead position

    private Vector3 velocity; // Used for smooth damp calculations
    private Vector3 lookAheadOffset; // Offset to look ahead of the player


    private void FixedUpdate()
    {

        if (playerTransform == null)
        {
            //Debug.LogWarning("CameraFollow disabled because playerTransform is not assigned.");
            enabled = false; // Disable this script
            return;
        }

        // Calculate look-ahead offset based on player's velocity and lookAheadDistance
        Vector3 projectedPosition = (playerTransform.GetComponent<Rigidbody2D>().velocity).normalized * lookAheadDistance;
        lookAheadOffset = Vector3.Lerp(lookAheadOffset, projectedPosition, lookAheadSpeed * Time.deltaTime);

        Vector3 desiredPosition = playerTransform.position + offset + lookAheadOffset;
        desiredPosition.z = transform.position.z; // Keep the camera's original z position unchanged

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

        // Optional: Make the camera always look at the player
        // transform.LookAt(playerTransform);
    }
}
