using UnityEngine;
using TMPro;

public class FrogInteractionPrompt : MonoBehaviour
{
    [Header("Prompt Settings")]
    [Tooltip("The UI Text that shows 'Press E to collect'")]
    public TextMeshProUGUI promptText;
    
    [Tooltip("Distance to check for frogs")]
    public float checkDistance = 5f;

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
        
        if (promptText != null)
        {
            promptText.text = "";
        }
    }

    void Update()
    {
        if (playerCamera == null || promptText == null) return;

        // Cast ray from camera
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Check if we're looking at a frog
        if (Physics.Raycast(ray, out hit, checkDistance))
        {
            // Check if the object has a Frog component
            Frog frog = hit.collider.GetComponent<Frog>();
            
            if (frog != null)
            {
                // Show prompt
                promptText.text = "Press E to collect frog";
                return;
            }
        }

        // Not looking at a frog, hide prompt
        promptText.text = "";
    }
}