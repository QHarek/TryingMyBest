public interface IInventory
{
    public void AddItem(Item item, int count);
    public void DeleteItem(Item item, int count);
    public void DropItem(Item item, int count);
}
