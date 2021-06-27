public interface IProvidedInteractable 
{
    public Items Condition { get; }
    public bool TryInteractWith(IPickable pickable);
}
