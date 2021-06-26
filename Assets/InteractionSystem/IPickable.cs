using UnityEngine;

public interface IPickable 
{
    public Rigidbody Rb { get; }
    public void BePickedUp(Transform parent);
    public void BeDropped();
}
