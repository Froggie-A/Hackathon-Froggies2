using UnityEngine;

public class Frog : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionDistance = 5f;
    public bool showPrompt = true;

    [SerializeField] string interactAxisName = "Interact";
    [SerializeField] int interactButtonIndex = 2;

    bool isLookingAt;
    Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
        if (!playerCamera) Debug.LogError("No Main Camera found! Tag your camera as MainCamera.");
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
        var manager = FindObjectOfType<FrogManager>();
        if (manager) manager.FrogFound(); else Debug.LogWarning("No FrogManager found in scene!");
        Destroy(gameObject);
    }
}
