using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool isFirstStonePlaced = false;
    private bool isSecondStonePlaced = false;

    private EventHandler eventHandler;

    private void Awake()
    {
        this.eventHandler = EventHandler.Instance;
    }

    private void OnEnable()
    {
        this.eventHandler.FirstStonePlacedEvent += OnFirstStonePlaced;
        this.eventHandler.SecondStonePlacedEvent += OnSecondStonePlaced;
    }

    private void OnDisable()
    {
        this.eventHandler.FirstStonePlacedEvent -= OnFirstStonePlaced;
        this.eventHandler.SecondStonePlacedEvent -= OnSecondStonePlaced;
    }

    private void OnFirstStonePlaced()
    {
        this.isFirstStonePlaced = true;
        this.TryOpenDoor();
    }

    private void OnSecondStonePlaced()
    {
        this.isSecondStonePlaced = true;
        this.TryOpenDoor();
    }

    private void TryOpenDoor()
    {
        if (this.isFirstStonePlaced == true && this.isSecondStonePlaced == true)
            this.OpenDoor();
    }

    private void OpenDoor()
    { 
        
    }
}
