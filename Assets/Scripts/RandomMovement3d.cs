using UnityEngine;
using System.Collections;

public class RandomMovement3D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float changeDirectionInterval = 3f;
    public float movementRange = 10f;
    public float rotationSpeed = 5f;

    [Header("Obstacle Avoidance")]
    public float rayDistance = 2f;        // How far ahead to check for obstacles
    public float avoidTurnAngle = 90f;    // How much to turn away when an obstacle is hit
    public LayerMask obstacleMask;        // Select which layers to detect (set in Inspector)

    private Vector3 targetPosition;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(ChooseNewTargetPosition());
    }

    void Update()
    {
        // Move toward the target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Calculate movement direction
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        // Obstacle avoidance raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out hit, rayDistance, obstacleMask))
        {
            // Turn away from the obstacle
            transform.Rotate(Vector3.up * avoidTurnAngle);
            // Pick a new random target
            StartCoroutine(ChooseNewTargetPosition());
        }

        // Rotate to face movement direction
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator ChooseNewTargetPosition()
    {
        while (true)
        {
            Vector3 randomOffset = Random.insideUnitSphere * movementRange;
            targetPosition = initialPosition + randomOffset;
            targetPosition.y = initialPosition.y; // Keep it flat
            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the forward ray in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.5f, transform.forward * rayDistance);
    }
}
