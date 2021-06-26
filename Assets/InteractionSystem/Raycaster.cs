using UnityEngine;
using TMPro;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private Camera raycastingCamera;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private Transform itemHolder;

    private IPickable pickableItem;

    private void Update()
    {
        if (InputListener.instance.IsInterractionButtonPressed() && this.pickableItem != null)
        {
            this.DropItem();
        }
        this.CheckForInteractible();
    }

    private void CheckForInteractible()
    {
        Vector3 position = this.raycastingCamera.transform.position;
        Vector3 forward = this.raycastingCamera.transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(position, forward, out RaycastHit hit, this.rayLength, this.interactableLayers))
        {
            if (hit.collider.gameObject.TryGetComponent(out IPickable item))
            {
                this.interactText.enabled = true;

                if (InputListener.instance.IsInterractionButtonPressed())
                {
                    this.PickUpItem(item);
                }
            }
        }
        else
        {
            this.interactText.enabled = false;
        }
    }

    private void PickUpItem(IPickable item)
    {
        this.pickableItem = item;
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;

        item.BePickedUp(this.itemHolder);
    }

    private void DropItem()
    {
        this.pickableItem.BeDropped();
        this.pickableItem = null;        
    }
}
