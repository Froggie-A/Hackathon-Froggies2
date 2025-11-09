using UnityEngine;

public class ControllerDebug : MonoBehaviour
{
    // Add both sets so it works on Windows & macOS
    private readonly string[] axes = {
        "Horizontal","Vertical","Mouse X","Mouse Y",
        "RightStickX","RightStickY",
        "RightStickX_Alt","RightStickY_Alt"
    };

    void Update()
    {
        foreach (var a in axes)
        {
            float v = 0f;
            try { v = Input.GetAxis(a); } catch { continue; } // skip if not set up
            if (Mathf.Abs(v) > 0.2f) Debug.Log($"{a}: {v:F2}");
        }

        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0 + i))
                Debug.Log($"Button {i} pressed");
        }
    }
}
