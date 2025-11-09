using UnityEngine;
using System.Collections;

public class RandomMovement3D : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float yValue = 0f;
    public float xValue = 0f;
    public float zValue = 0f;
    public float changeDirectionInterval = 3f;
    public float movementRange = 10f;
    public float rotationSpeed = 5f; // Controls how fast it turns

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

        // Calculate direction (ignore Y so it stays level)
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        // Rotate smoothly to face the direction of movement
        if (direction.sqrMagnitude > 0.001f) // Prevents jitter when nearly at target
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
            targetPosition.y = initialPosition.y; // Keep movement flat
            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }
}
