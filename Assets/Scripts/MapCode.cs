using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MapCode : MonoBehaviour
{

    private bool isVisible;


    // sets main menu to invisble upon loadup
    void Start()
    {
    }

    // Opens and closes main menu on escape button
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MapView");
        }
    }
}
