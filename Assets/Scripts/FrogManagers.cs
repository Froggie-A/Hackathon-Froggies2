using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FrogManager : MonoBehaviour
{
    [Header("Level Settings")]
    [Tooltip("Set this to match the number of frogs in your level")]
    public int totalFrogs = 6;

    [Header("UI References")]
    [Tooltip("Drag the UI Text component here")]
    public TextMeshProUGUI frogCounterText;
    
    [Tooltip("Optional: Panel to show when all frogs are found")]
    public GameObject winPanel;

    private int frogsFoundThisLevel = 0;
    private string currentLevelName;

    void Start()
    {
        currentLevelName = SceneManager.GetActiveScene().name;
        
        // Load saved progress for this level
        if (GameData.Instance != null)
        {
            frogsFoundThisLevel = GameData.Instance.GetFrogsInLevel(currentLevelName);
        }
        
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
            Debug.LogWarning("FrogManager: No UI Text assigned!");
        }
        
        // Check if level is already complete
        if (frogsFoundThisLevel >= totalFrogs)
        {
            AllFrogsFound();
        }
    }

    // Called by Frog.cs when a frog is collected
    public void FrogFound()
    {
        frogsFoundThisLevel++;
        
        Debug.Log($"Frog found! This level: {frogsFoundThisLevel}/{totalFrogs}");
        
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
            frogCounterText.text = $"Frogs Found: {frogsFoundThisLevel}/{totalFrogs}";
        }
    }

    void AllFrogsFound()
    {
        Debug.Log("Congratulations! All frogs found in this level!");
        
        // Show win panel if assigned
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }
}