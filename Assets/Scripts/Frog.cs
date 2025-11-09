using UnityEngine;

public class Frog : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Tooltip("Maximum distance player can interact from")]
    public float interactionDistance = 5f;
    
    [Tooltip("Show a prompt when player looks at this frog")]
    public bool showPrompt = true;

    private bool isLookingAt = false;
    private Camera playerCamera;

    void Start()
    {
        // Find the main camera (player's camera)
        playerCamera = Camera.main;
        
        if (playerCamera == null)
        {
            Debug.LogError("No Main Camera found! Make sure your camera is tagged as MainCamera");
        }
    }

    void Update()
    {
        // Check if player is looking at this frog
        CheckIfPlayerLookingAt();

        // If looking at frog and press E, collect it
        if (isLookingAt && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Jump")))
        {
            CollectFrog();
        }
    }

    void CheckIfPlayerLookingAt()
    {
        if (playerCamera == null) return;

        // Cast a ray from camera forward
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Check if ray hits something within interaction distance
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Check if we hit THIS frog
            if (hit.collider.gameObject == gameObject)
            {
                isLookingAt = true;
                return;
            }
        }

        isLookingAt = false;
    }

    void CollectFrog()
    {
        // Tell the FrogManager that we found a frog
        FrogManager manager = FindObjectOfType<FrogManager>();
        
        if (manager != null)
        {
            manager.FrogFound();
        }
        else
        {
            Debug.LogWarning("No FrogManager found in scene!");
        }

        // Destroy this frog GameObject
        Destroy(gameObject);
    }

    // Optional: Visual feedback in Scene view (for debugging)
    void OnDrawGizmos()
    {
        if (isLookingAt)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}