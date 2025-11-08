using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{

     // this is a placeholder name for the  destinationscene that the will load to 
     
    [SerializeField] private string TargetSceneName = "WorldMapPH";
    // specifically what the button calls when clicked
    public void LoadGameScene()
    {
        //loads the scenes
        if (TargetSceneName != "")
        {
            //loads the scene that's passed in
            SceneManager.LoadScene(TargetSceneName);
        }
        else
        {
            Debug.LogError(" ButtonUI Error: TargetSceneName is not set.");
        }
    }
}
