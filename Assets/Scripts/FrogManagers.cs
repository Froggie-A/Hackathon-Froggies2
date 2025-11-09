using UnityEngine;
using TMPro;

public class FrogManager : MonoBehaviour
{
    [Header("Level Settings")]
    [Tooltip("Set this to match the number of frogs in your level")]
    public int totalFrogs = 10;

    [Header("UI References")]
    [Tooltip("Drag the UI Text component here")]
    public TMPro.TextMeshProUGUI frogCounterText;
    
    [Tooltip("Optional: Panel to show when all frogs are found")]
    public GameObject winPanel;

    private int frogsFound = 0;

    void Start()
    {
        // Initialize the UI
        UpdateUI();

        // Hide win panel at start if it exists
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        // Validate setup
        if (frogCounterText == null)
        {
            Debug.LogWarning("FrogManager: No UI Text assigned! Drag a Text component to 'Frog Counter Text' in Inspector");
        }
    }

    // Called by Frog.cs when a frog is collected
    public void FrogFound()
    {
        frogsFound++;
        
        Debug.Log($"Frog found! Total: {frogsFound}/{totalFrogs}");
        
        UpdateUI();
        
        // Check if all frogs have been found
        if (frogsFound >= totalFrogs)
        {
            AllFrogsFound();
        }
    }

    void UpdateUI()
    {
        if (frogCounterText != null)
        {
            frogCounterText.text = $"Frogs Found: {frogsFound}/{totalFrogs}";
        }
    }

    void AllFrogsFound()
    {
        Debug.Log("Congratulations! All frogs found!");
        
        // Show win panel if assigned
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        
        // Optional: You could add more win logic here
        // - Play victory sound
        // - Load next level
        // - Show timer
        // - etc.
    }

    // Optional: Helper method to reset the level
    public void ResetLevel()
    {
        frogsFound = 0;
        UpdateUI();
        
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    // Optional: Show current progress in Inspector while playing
    void OnGUI()
    {
        // This shows debug info in game view (top-left corner)
        // Remove this method if you don't want it
        if (Application.isPlaying)
        {
            GUI.Label(new Rect(10, 10, 300, 20), $"DEBUG: {frogsFound}/{totalFrogs} frogs found");
        }
    }
}