using UnityEngine;

public sealed class UIController : MonoBehaviour
{
    private GameObject _inventoryCanvas;

    private void Awake()
    {
        _inventoryCanvas = GameObject.Find("Inventory Canvas");
    }

    private void Start()
    {
        _inventoryCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            _inventoryCanvas.SetActive(!_inventoryCanvas.activeSelf);
        }
    }
}
