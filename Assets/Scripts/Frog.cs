using UnityEngine;
using UnityEngine.SceneManagement;

public class Frog : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionDistance = 5f;
    public bool showPrompt = true;

    [SerializeField] string interactAxisName = "Interact";
    [SerializeField] int interactButtonIndex = 2;
    
    // Unique ID for this frog (automatically generated)
    private string frogID;

    bool isLookingAt;
    Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
        if (!playerCamera) Debug.LogError("No Main Camera found! Tag your camera as MainCamera.");
        
        // Create unique ID based on scene and frog's name/position
        string sceneName = SceneManager.GetActiveScene().name;
        frogID = $"{sceneName}_{gameObject.name}_{transform.position.x}_{transform.position.z}";
        
        // Check if this frog was already collected
        if (GameData.Instance != null && GameData.Instance.IsFrogCollected(frogID))
        {
            // This frog was already collected, destroy it immediately
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        CheckIfPlayerLookingAt();

        bool pressed = Input.GetButtonDown(interactAxisName)
                       || Input.GetKeyDown(KeyCode.JoystickButton0 + interactButtonIndex);

        if (isLookingAt && pressed)
            CollectFrog();
    }

    void CheckIfPlayerLookingAt()
    {
        if (!playerCamera) { isLookingAt = false; return; }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        isLookingAt = Physics.Raycast(ray, out RaycastHit hit, interactionDistance) &&
                      hit.collider.gameObject == gameObject;
    }

    void CollectFrog()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        
        // Save to GameData
        if (GameData.Instance != null)
        {
            GameData.Instance.CollectFrog(frogID, currentScene);
        }
        
        // Update FrogManager UI
        var manager = FindObjectOfType<FrogManager>();
        if (manager)
        {
            manager.FrogFound();
        }
        else
        {
            Debug.LogWarning("No FrogManager found in scene!");
        }
        
        Destroy(gameObject);
    }
}