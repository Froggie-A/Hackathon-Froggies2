using UnityEngine;

public class MouseUnlocker : MonoBehaviour
{
    void Start()
    {
       
        UnlockMouse();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UnlockMouse();
        }
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }

    
    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}