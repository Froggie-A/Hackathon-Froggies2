using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FrogManager : MonoBehaviour
{
    // GLOBAL COUNTER - Shared across all scenes!
    public static int globalFrogsFound = 0;
    
    [Header("Display Mode")]
    [Tooltip("Show global count across all levels, or just this level's count?")]
    public bool showGlobalCount = false;
    
    [Header("Level Settings")]
    [Tooltip("Set this to match the number of frogs in your level")]
    public int totalFrogs = 10;
    
    [Tooltip("Total frogs across ALL levels (for global display)")]
    public int totalFrogsAllLevels = 3;

    [Header("UI References")]
    [Tooltip("Drag the UI Text component here")]
    public TextMeshProUGUI frogCounterText;

    
    [Tooltip("Optional: Panel to show when all frogs are found")]
    public GameObject winPanel;

    private int frogsFoundThisLevel = 0;

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
        frogsFoundThisLevel++;
        globalFrogsFound++;  // Increment global counter too!
        
        Debug.Log($"Frog found! This level: {frogsFoundThisLevel}/{totalFrogs} | Global: {globalFrogsFound}");
        
        UpdateUI();
        
        // Check if all frogs in THIS level have been found
        if (frogsFoundThisLevel >= totalFrogs)
        {
            AllFrogsFound();
        }
    }

    void UpdateUI()
    {
        if (frogCounterText != null)
        {
            if (showGlobalCount)
            {
                // Show global count across all levels
                frogCounterText.text = $"Frogs Found: {globalFrogsFound}/{totalFrogsAllLevels}";
            }
            else
            {
                // Show just this level's count
                frogCounterText.text = $"Frogs Found: {frogsFoundThisLevel}/{totalFrogs}";
            }
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
        frogsFoundThisLevel = 0;
        UpdateUI();
        
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }
    
    // Optional: Reset global counter (use in main menu or game restart)
    public static void ResetGlobalCount()
    {
        globalFrogsFound = 0;
    }
}