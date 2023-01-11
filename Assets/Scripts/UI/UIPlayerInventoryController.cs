using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIPlayerInventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _itemTemplate;

    private GameObject _content;
    private GameObject _itemDescriptionCanvas;
    private List<GameObject> _items = new List<GameObject>();
    private PlayerInventory _playerInventory;

    private void Awake()
    {
        _content = GetComponentInChildren<GridLayoutGroup>().gameObject;
        _itemDescriptionCanvas = GameObject.Find("Item Description");
        _playerInventory = FindObjectOfType<PlayerInventory>();
    }

    private void Start()
    {
        IntializeItemList();
    }

    private void IntializeItemList()
    {
        _items.Clear();
        for (int i = 0; i < _content.transform.childCount; i++)
        {
            _items.Add(_content.transform.GetChild(i).gameObject);
        }
    }

    private void UpdateItemList()
    {
        for (int i = 0; i < _content.transform.childCount; i++)
        {
            _items.Add(_content.transform.GetChild(i).gameObject);
        }
    }

    private Image GetIconImageComponent(GameObject itemPrefab)
    {
        var images = itemPrefab.GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            if (image.name == "Icon")
            {
                return image;
            }
        }
        return null;
    }

    internal void ShowItemContextMenu()
    {

    }

    internal void ShowItemDescription(string name)
    {
        _itemDescriptionCanvas.SetActive(true);
        Item item = _playerInventory.GetItemDescription(name);
        _itemDescriptionCanvas.GetComponentsInChildren<TMPro.TextMeshProUGUI>().
            Where(p => p.gameObject.name == "Description").First().text = item.Description;
    }

    internal void AddItem(Item item)
    {
        GameObject newItem = Instantiate(_itemTemplate, _content.transform);
        newItem.name = item.Name;
        GetIconImageComponent(newItem).sprite = item.Icon;
        _items.Add(_content.transform.GetChild(_content.transform.childCount - 1).gameObject);
    }

    internal void RemoveItem(int index)
    {
        UpdateItemList();
    }

    internal void UpdateItemsCount(int index, int count)
    {
        _items[index].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = count.ToString();
    }

    private void OnEnable()
    {
        _itemDescriptionCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
