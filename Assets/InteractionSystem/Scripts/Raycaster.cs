using UnityEngine;
using TMPro;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private Camera raycastingCamera;
    [SerializeField] private GameObject promptHolder;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private Transform itemHolder;

    private IPickable itemInHand;

    private IPickable pickableItem;
    private ISimpleInteractable simpleInteractable;
    private IProvidedInteractable providedInteractable;

    private bool isIdentified = false;

    private void Update()
    {
        Vector3 position = this.raycastingCamera.transform.position;
        Vector3 forward = this.raycastingCamera.transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(position, forward, out RaycastHit hit, this.rayLength, this.interactableLayers))
        {
            if (this.isIdentified == false)
            {
                this.CheckForPickable(hit);
                this.CheckForSimpleInteractable(hit);                
                this.CheckForProvidedInteractable(hit);
            }
            else
            {
                if (InputListener.Instance.IsInterractionButtonPressed())
                {
                    this.OnInteractionButtonPressed();
                }
            }
        }
        else
        {
            this.interactText.text = null;
            this.promptHolder.SetActive(false);
            this.pickableItem = null;
            this.simpleInteractable = null;
            this.providedInteractable = null;
            this.isIdentified = false;
        }

        if (InputListener.Instance.IsThrowButtonPressed() && this.itemInHand != null)        
            this.ThrowItem();
    }

    private void OnInteractionButtonPressed()
    {
        if (this.pickableItem != null)
        {
            if (this.itemInHand == null)
            {
                this.pickableItem.BePickedUp(this.itemHolder);
                this.itemInHand = this.pickableItem;
                this.pickableItem = null;
            }
        }
        else if (this.simpleInteractable != null)
        {
            this.simpleInteractable.Interact();
            this.simpleInteractable = null;
        }
        else if (this.providedInteractable != null && this.itemInHand != null)
        {
            if (this.providedInteractable.TryInteractWith(this.itemInHand))
            {
                this.providedInteractable = null;
                this.itemInHand = null;
            }
            else
            {
                this.interactText.text = "Requiers different component";
            }
        }
    }

    private void CheckForPickable(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out IPickable pickable))
        {
            this.interactText.text = "Press left mouse button to pick it up";
            this.promptHolder.SetActive(true);

            this.pickableItem = pickable;

            this.isIdentified = true;
        }               
    }

    private void CheckForProvidedInteractable(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out IProvidedInteractable providedInteractable))
        {
            if (this.itemInHand != null)
                this.interactText.text = $"Press left mouse button to use {this.itemInHand.Item}";
            else
                this.interactText.text = "Requires component to work";

            this.promptHolder.SetActive(true);

            this.providedInteractable = providedInteractable;

            this.isIdentified = true;          
        }
    }

    private void CheckForSimpleInteractable(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out ISimpleInteractable simpleInteractable))
        {
            this.interactText.text = $"Press left mouse button to interact";
            this.promptHolder.SetActive(true);
            
            this.simpleInteractable = simpleInteractable;

            this.isIdentified = true;           
        }
    }

    private void ThrowItem()
    {
        this.itemInHand.BeThrowed();
        this.itemInHand = null;        
    }
}
