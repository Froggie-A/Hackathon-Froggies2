using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] string interactAxisName = "Interact";
    [SerializeField] int interactButtonIndex = 2;

    IInteractable current;

    void Update()
    {
        bool pressed =
            Input.GetButtonDown(interactAxisName) ||
            Input.GetKeyDown(KeyCode.JoystickButton0 + interactButtonIndex);

        if (current != null && pressed)
            current.Interact(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        var i = other.GetComponent<IInteractable>();
        if (i != null) current = i;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() == current) current = null;
    }
}

public interface IInteractable
{
    void Interact(GameObject interactor);
}
