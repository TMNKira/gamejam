using UnityEngine;

public class PickableObject : MonoBehaviour, IPickable
{
    private Rigidbody rb;
    public Rigidbody Rb => this.rb;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    public void BePickedUp(Transform parent)
    {
        this.transform.SetParent(parent);
        this.transform.localPosition = Vector3.zero;
        this.transform.localEulerAngles = Vector3.zero;
    }

    public void BeDropped()
    {
        this.transform.SetParent(null);
        this.Rb.isKinematic = false;
        this.Rb.AddForce(this.transform.forward * 2, ForceMode.VelocityChange);
    }
}
