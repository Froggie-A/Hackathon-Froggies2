using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float speed = 2f;          // Movement speed
    public float leftBound = -50f;    // Left limit
    public float rightBound = 50f;    // Right limit
    public float minY = 15f;          // Vertical range (optional)
    public float maxY = 25f;

    private void Update()
    {
        // Move cloud to the right
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // When cloud goes beyond the right bound, reset to left side
        if (transform.position.x > rightBound)
        {
            float randomY = Random.Range(minY, maxY);
            transform.position = new Vector3(leftBound, randomY, transform.position.z);
        }
    }
}
