using UnityEngine;

public interface IPickable
{
    public Items Item { get; }
    public void BePickedUp(Transform parent);
    public void BeThrowed();
}
