using UnityEngine;

public class SecondPodium : MonoBehaviour, IProvidedInteractable
{
    public Items Condition => Items.Stone;

    [SerializeField] private Transform itemHolder;

    private EventHandler eventHandler;
    
    private void Awake()
    {
        this.eventHandler = EventHandler.Instance;
    }

    public bool TryInteractWith(IPickable pickable)
    {
        if (pickable.Item == this.Condition)
        {
            pickable.BePickedUp(this.itemHolder);
            this.eventHandler.InvokeSecondStonePlaced();
            return true;
        }
        else
        {
            return false;
        }
    }
}
