using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PopUpUI : MonoBehaviour
{
    public GameObject popUpUI;

    public TMP_Text popUpText;
    public Animator animator;

    public void PopUP(string text)
    {
        popUpUI.SetActive(true);
        popUpText.text = text;
        animator.SetTrigger("PopUp");
    }
    public void ClosePopUp()
    {
        popUpUI.SetActive(false);
    }
}

