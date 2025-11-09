using UnityEngine;

public class FrogCollectible : MonoBehaviour, IInteractable
{
    [SerializeField] int value = 1;
    public void Interact(GameObject interactor)
    {
        var inv = interactor.GetComponent<PlayerInventory>();
        if (inv != null) inv.frogs += value;
        Destroy(gameObject);
    }
}
