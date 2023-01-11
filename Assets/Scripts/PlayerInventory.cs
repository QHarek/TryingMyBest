using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public sealed class PlayerInventory : MonoBehaviour, IInventory
{

    private UIPlayerInventoryController _playerInventoryUI;
    private Dictionary<Item, int> _items;

    private void Awake()
    {
        _items = new Dictionary<Item, int>();
        _playerInventoryUI = FindObjectOfType<UIPlayerInventoryController>(true);
    }

    private bool IsItemContained(Item item)
    {
        foreach (var _item in _items)
        {
            if (item.Name == _item.Key.Name)
                return true;
        }
        return false;
    }

    private Item FindSimilarItem(Item item)
    {
        foreach (var _item in _items)
        {
            if (item.Name == _item.Key.Name)
                return _item.Key;
        }
        return item;
    }

    public void AddItem(Item item, int count)
    {
        if (IsItemContained(item))
        {
            Item existingItem = FindSimilarItem(item);
            _items[existingItem] += count;
            int itemIndex = GetItemIndex(existingItem);
            _playerInventoryUI.UpdateItemsCount(itemIndex, _items[existingItem]);
        }
        else
        {
            _items.Add(item, count);
            _playerInventoryUI.AddItem(item);
            int itemIndex = GetItemIndex(item);
            _playerInventoryUI.UpdateItemsCount(itemIndex, _items[item]);
        }
    }

    public void DeleteItem(Item item, int count)
    {
        int itemIndex = GetItemIndex(item);
        if (_items[item] == count)
        {
            _items.Remove(item);
            _playerInventoryUI.RemoveItem(itemIndex);
        }
        else
        {
            _items[item] -= count;
            _playerInventoryUI.UpdateItemsCount(itemIndex, _items[item]);
        }
    }

    public void DropItem(Item item, int count)
    {
        int itemIndex = GetItemIndex(item);
        if (_items[item] == count)
        {
            _items.Remove(item);
            _playerInventoryUI.RemoveItem(itemIndex);
        }
        else
        {
            _items[item] -= count;
            _playerInventoryUI.UpdateItemsCount(itemIndex, _items[item]);
        }
    }

    public int GetItemIndex(Item item)
    {
        int index = 0;
        foreach (var key in _items)
        {
            if (key.Key == item)
                break;
            index++;
        }
        return index;
    }

    internal Item GetItemDescription(string name)
    {
        return _items.Where(p => p.Key.Name == name).First().Key; ;
    }
}
