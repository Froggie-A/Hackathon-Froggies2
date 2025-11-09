using UnityEngine;
using System.Collections.Generic;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    
    // Store which frogs have been collected (by their unique ID)
    public HashSet<string> collectedFrogIDs = new HashSet<string>();
    
    // Store frog counts per level
    public Dictionary<string, int> frogsPerLevel = new Dictionary<string, int>();
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    // Check if a frog was already collected
    public bool IsFrogCollected(string frogID)
    {
        return collectedFrogIDs.Contains(frogID);
    }
    
    // Mark a frog as collected
    public void CollectFrog(string frogID, string levelName)
    {
        if (!collectedFrogIDs.Contains(frogID))
        {
            collectedFrogIDs.Add(frogID);
            
            // Increment count for this level
            if (!frogsPerLevel.ContainsKey(levelName))
            {
                frogsPerLevel[levelName] = 0;
            }
            frogsPerLevel[levelName]++;
            
            Debug.Log($"Frog {frogID} collected in {levelName}. Total in level: {frogsPerLevel[levelName]}");
        }
    }
    
    // Get frog count for a specific level
    public int GetFrogsInLevel(string levelName)
    {
        return frogsPerLevel.ContainsKey(levelName) ? frogsPerLevel[levelName] : 0;
    }
    
    // Reset everything (for new game)
    public void ResetAll()
    {
        collectedFrogIDs.Clear();
        frogsPerLevel.Clear();
    }
}