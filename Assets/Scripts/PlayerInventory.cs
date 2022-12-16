using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInventory
{

    private UIPlayerInventoryController _playerInventoryUI;
    private Dictionary<GameObject, int> _items;

    private void Start()
    {
        _items = new Dictionary<GameObject, int>();
        _playerInventoryUI = GetComponent<UIPlayerInventoryController>();
    }

    public void AddItem(GameObject item, int count)
    {
        if (_items.ContainsKey(item))
        {
            _items[item] += count;
            int itemPosition = GetItemPosition(item);
            _playerInventoryUI.UpdateItemsCount(itemPosition, _items[item]);
        }
        else
        {
            _items.Add(item, count);
            _playerInventoryUI.AddItem(item.GetComponent<Item>().GetIcon(), count);
        }
    }

    public void DeleteItem(GameObject item, int count)
    {
        int itemPosition = GetItemPosition(item);
        if (_items[item] == count)
        {
            _items.Remove(item);
            _playerInventoryUI.RemoveItem(itemPosition);
        }
        else
        {
            _items[item] -= count;
            _playerInventoryUI.UpdateItemsCount(itemPosition, _items[item]);
        }
    }

    public void DropItem(GameObject item, int count)
    {
        int itemPosition = GetItemPosition(item);
        if (_items[item] == count)
        {
            _items.Remove(item);
            _playerInventoryUI.RemoveItem(itemPosition);
        }
        else
        {
            _items[item] -= count;
            _playerInventoryUI.UpdateItemsCount(itemPosition, _items[item]);
        }
    }

    public int GetItemPosition(GameObject item)
    {
        int position = 0;
        foreach (var key in _items)
        {
            if (key.Key == item)
                break;
            position++;
        }
        return position;
    }
}
