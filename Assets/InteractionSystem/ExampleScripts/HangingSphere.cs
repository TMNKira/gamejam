using UnityEngine;

public class HangingSphere : MonoBehaviour
{
    private EventHandler eventHandler;
    private Rigidbody rb;

    private void Awake()
    {
        this.eventHandler = EventHandler.Instance;
        this.rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        this.eventHandler.SimpleButtonPressedEvent += DropDown;
    }

    private void OnDisable()
    {
        this.eventHandler.SimpleButtonPressedEvent += DropDown;
    }

    private void DropDown()
    {
        this.rb.useGravity = true;
    }
}
