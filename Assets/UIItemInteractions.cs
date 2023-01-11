using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemInteractions : MonoBehaviour, IPointerClickHandler
{
    private UIPlayerInventoryController _playerInventoryController;

    private void Awake()
    {
        _playerInventoryController = FindObjectOfType<UIPlayerInventoryController>(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            _playerInventoryController.ShowItemDescription(gameObject.name);
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
            _playerInventoryController.ShowItemContextMenu();
    }
}
