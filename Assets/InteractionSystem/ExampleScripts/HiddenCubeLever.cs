using UnityEngine;

public class HiddenCubeLever : MonoBehaviour, ISimpleInteractable
{
    private EventHandler eventHandler;

    private void Awake()
    {
        this.eventHandler = EventHandler.Instance;
    }

    public void Interact()
    {
        this.eventHandler.InvokeSimpleButtonPressed();
    }
}
