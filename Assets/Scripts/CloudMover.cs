using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float speed = 2f;          
    public float leftBound = -50f;    
    public float rightBound = 50f;    
    public float minY = 15f;          
    public float maxY = 25f;

    private void Update()
    {
        transform.Translate(Vector3.right * speed*Time.deltaTime);

        if (transform.position.x > rightBound)
        {
            float randomY = Random.Range(minY, maxY);
            transform.position = new Vector3(leftBound, randomY, transform.position.z);
        }
    }
}
