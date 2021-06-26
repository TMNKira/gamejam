using UnityEngine;

public class StaticInteractableObject : MonoBehaviour, ISimpleInteractable
{
    public void Interact()
    {
        Debug.Log($"Interacted with simple object {this.gameObject.name}");
    }
}