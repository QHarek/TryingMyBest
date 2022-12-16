using UnityEngine.UI;

public class Ore : Item, ISellable
{
    private Image _icon;

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void Drop()
    {
        
    }

    public override Image GetIcon()
    {
        return _icon;        
    }

    public void PickUp(IInventory inventory)
    {
        inventory.AddItem(gameObject, 1);
    }

    public void Sell()
    {
        
    }
}
