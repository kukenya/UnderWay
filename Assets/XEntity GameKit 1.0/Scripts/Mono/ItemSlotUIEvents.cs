using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//This script is attached to the ItemSlot object; it handles the dragging/dropping/click events of the container slot UI.
public class ItemSlotUIEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    //The ItemSlot that is globally hovered over currently; null if none is hovered.
    private static ItemSlot hoveredSlot;

    public bool rightClickable;

    //The ItemSlot object that this script is attached to.
    private ItemSlot mySlot;

    //The icon image of this slot UI.
    private Image slotUI;

    //The offset from the mous position when being dragged.
    private Vector3 dragOffset;

    //The original position of the slot UI.
    private Vector3 origin;

    //The original color of the slot UI.
    private Color regularColor;

    //The color of the slot when being dragged; by default the alpha is decreased.
    private Color dragColor;

    private int originalSiblingIndex;

    UnderWay inputActions;

    private void Awake()
    {
        //All the variables are initialized here.
        mySlot = GetComponent<ItemSlot>();
        slotUI = GetComponent<Image>();
        originalSiblingIndex = transform.GetSiblingIndex();

        origin = transform.localPosition;
        regularColor = slotUI.color;
        dragColor = new Color(regularColor.r, regularColor.g, regularColor.b, 0.3f);
        inputActions = new UnderWay();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        inputActions.Player.RightMouseClick.performed += RightMouseClick_performed;
    }

    private void RightMouseClick_performed(InputAction.CallbackContext obj)
    {
        if (rightClickable)
        {
            OnSlotRightClicked(mySlot);
        }
    }

    private void OnSlotRightClicked(ItemSlot slot)
    {
        UIManager.instance.itemUseButton.onClick.RemoveAllListeners();
        UIManager.instance.itemUseButton.onClick.AddListener(delegate { OnItemUseClicked(slot); UIManager.instance.slotOptionsUI.SetActive(false); });

        UIManager.instance.itemRemoveButton.onClick.RemoveAllListeners();
        UIManager.instance.itemRemoveButton.onClick.AddListener(delegate { OnRemoveItemClicked(slot); UIManager.instance.slotOptionsUI.SetActive(false); });

        UIManager.instance.slotOptionsUI.transform.position = inputActions.UI.Point.ReadValue<Vector2>();
        UIManager.instance.slotOptionsUI.SetActive(true);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (mySlot.slotItem != null)
        {
            InventoryManager.instance.OnItemInfo(mySlot.slotItem);
        }
    }

    private void OnRemoveItemClicked(ItemSlot slot)
    {
        slot.Remove(1);
    }
    private void OnItemUseClicked(ItemSlot slot)
    {
        ItemManager.Instance.UseItem(slot);
    }

    //This method is called when the mouse cursor enters this slot UI.
    public void OnPointerEnter(PointerEventData eventData)
    {
        rightClickable = true;
        hoveredSlot = mySlot;
    }

    //This method is called when the mouse cursor exists this slot UI.
    public void OnPointerExit(PointerEventData eventData)
    {
        rightClickable = false;
        hoveredSlot = null;
    }


    //This method is called when the mouse cursor starts dragging this slot UI.
    public void OnBeginDrag(PointerEventData eventData)
    {
        slotUI.transform.SetAsLastSibling();
        slotUI.color = dragColor;
        slotUI.raycastTarget = false;
        hoveredSlot = null;
        Vector3 mousePos = inputActions.UI.Point.ReadValue<Vector2>();
        dragOffset = mousePos - transform.position;
    }

    //This method is continously called when the mouse cursor is dragging this slot UI.
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = inputActions.UI.Point.ReadValue<Vector2>();
        transform.position = mousePos - dragOffset;
    }

    //This method is called when the mouse cursor stops dragging this slot UI.
    public void OnEndDrag(PointerEventData eventData)
    {
        //If there is a slot being hovered over, an attempt to transfer the items from this slot to the hovered slot will be made.
        if (hoveredSlot != null) OnDrop();

        transform.SetSiblingIndex(originalSiblingIndex);
        transform.localPosition = origin;
        slotUI.color = regularColor;
        slotUI.raycastTarget = true;
    }

    //Tries to transfer the items from this slot to the hoveredSlot. 
    private void OnDrop()
    {
        Utils.TransferItem(mySlot, hoveredSlot);
    }
}
