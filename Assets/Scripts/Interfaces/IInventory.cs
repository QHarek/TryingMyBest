using UnityEngine;

public interface IInventory
{
    public void AddItem(GameObject item, int count);
    public void DeleteItem(GameObject item, int count);
    public void DropItem(GameObject item, int count);
}
