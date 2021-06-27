using UnityEngine;

public class PickableObject : MonoBehaviour, IPickable
{
    public Items Item => Items.Cube;

    [SerializeField] private LayerMask interractableLayer;
    [SerializeField] private float throwForce;

    private Rigidbody rb;    

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody>();
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    private void Update()
    {
        if (this.transform.parent != null)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, Vector3.zero, 2f * Time.deltaTime);
        }
    }

    public void BePickedUp(Transform parent)
    {
        this.rb.isKinematic = true;
        this.rb.velocity = Vector3.zero;
        this.rb.angularVelocity = Vector3.zero;

        this.transform.SetParent(parent);        
        this.transform.localEulerAngles = Vector3.zero;

        this.gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void BeThrowed()
    {
        this.transform.SetParent(null);

        this.rb.isKinematic = false;
        this.rb.AddForce(this.transform.forward * this.throwForce, ForceMode.VelocityChange);

        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}
