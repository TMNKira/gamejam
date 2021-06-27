using UnityEngine;

public class ItemsHider : MonoBehaviour
{
    [SerializeField] private GameObject[] coldOnlyGameObjects;

    private EventHandler eventHandler;

    private void Awake()
    {
        this.eventHandler = EventHandler.Instance;

        foreach (GameObject go in this.coldOnlyGameObjects)
        {
            if (go.activeInHierarchy == true)
                go.SetActive(false);
        }
    }

    private void OnEnable()
    {
        this.eventHandler.SideSwitchedEvent += OnSideSwtiched;
    }

    private void OnDisable()
    {
        this.eventHandler.SideSwitchedEvent -= OnSideSwtiched;
    }

    private void OnSideSwtiched(Sides side)
    {
        if (side == Sides.Cold)
        {
            foreach (GameObject go in this.coldOnlyGameObjects)
            {
                if (go.activeInHierarchy == false)
                    go.SetActive(true);
            }
        }
        else if (side == Sides.Warm)
        {
            foreach (GameObject go in this.coldOnlyGameObjects)
            {
                if (go.activeInHierarchy == true)                
                    go.SetActive(false);                
            }
        }
    }
}
