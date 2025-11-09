using UnityEngine;
using UnityEngine.SceneManagement;
public class WMBackButtonUI : MonoBehaviour 
{

     // this is a placeholder name for the  destination scene that the will load to 
     
    [SerializeField] private string TargetSceneName = "SampleScene";
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
            Debug.LogError(" WMBackButtonUI Error: TargetSceneName is not set.");
        }
    }
}

