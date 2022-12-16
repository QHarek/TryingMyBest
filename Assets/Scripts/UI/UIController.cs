using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameObject _inventory;

    private void Start()
    {
        _inventory = GameObject.Find("Inventory");
        _inventory.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("i"))
            _inventory.SetActive(!_inventory.activeSelf);
    }
}
