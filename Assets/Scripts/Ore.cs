using UnityEngine;

public sealed class Ore : Item, ISellable
{
    public Ore(OreData oreData)
    {
        ItemDropPrefab = Resources.Load("Prefabs/OreItem") as GameObject;
        Icon = oreData.Icon;
        Cost = oreData.Cost;
        Weight = oreData.Weight;
        StackSize = oreData.StackSize;
        Name = oreData.Name;
        Description = oreData.Description;
    }

    public override void Delete()
    {
        
    }

    public override void Drop()
    {
        
    }

    public override void PickUp(IInventory inventory)
    {
        inventory.AddItem(this, 1);
    }

    public void Sell()
    {
        
    }
}
