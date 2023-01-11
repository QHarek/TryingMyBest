using UnityEngine;

public abstract class Item
{
    private GameObject _itemDropPrefab;
    private Sprite _icon;
    private int _cost;
    private int _weight;
    private int _stackSize;
    private string _name;
    private string _description;

    internal GameObject ItemDropPrefab { get => _itemDropPrefab; set => _itemDropPrefab = value; }
    internal Sprite Icon { get => _icon; set => _icon = value; }
    internal int Cost { get => _cost; set => _cost = value; }
    internal int Weight { get => _weight; set => _weight = value; }
    internal int StackSize { get => _stackSize; set => _stackSize = value; }
    internal string Name { get => _name; set => _name = value; }
    internal string Description { get => _description; set => _description = value; }

    public abstract void Delete();
    public abstract void Drop();
    public abstract void PickUp(IInventory inventory);
}
