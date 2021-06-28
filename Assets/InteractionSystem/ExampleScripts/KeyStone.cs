using UnityEngine;

public class KeyStone : MonoBehaviour, IPickable
{
    public Items Item => Items.Stone;

    [SerializeField] private LayerMask interractableLayer;
    [SerializeField] private float throwForce;

    private Rigidbody rb;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody>();
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public void BePickedUp(Transform parent)
    {
        this.rb.isKinematic = true;
        this.rb.useGravity = false;
        this.rb.velocity = Vector3.zero;
        this.rb.angularVelocity = Vector3.zero;
        this.rb.constraints = RigidbodyConstraints.FreezeAll;
        this.rb.mass = 0f;
                
        this.transform.SetParent(parent);
        this.transform.localPosition = Vector3.zero;
        this.transform.localEulerAngles = Vector3.zero;

        this.gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void BeThrowed()
    {
        this.transform.SetParent(null);

        this.rb.mass = 1f;
        this.rb.isKinematic = false;
        this.rb.useGravity = true;
        this.rb.AddForce(this.transform.forward * this.throwForce, ForceMode.VelocityChange);

        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}
