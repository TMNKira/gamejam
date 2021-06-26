using UnityEngine;

public class ProvidedInteractableObject : MonoBehaviour, IProvidedInteractable
{
    public Items Condition => Items.Cube;

    [SerializeField] private Transform itemHolder;

    public bool TryInteractWith(IPickable pickable)
    {
        if (this.Condition == pickable.Item)
        {
            pickable.BePickedUp(this.itemHolder);
            return true;
        }
        else
        {
            return false;
        }
    }
}
