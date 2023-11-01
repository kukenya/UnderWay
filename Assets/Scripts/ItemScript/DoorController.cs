using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThemedKeySystem;
using ExamineSystem;

public class DoorController : MonoBehaviour
{
    public enum DoorType { None, ThemedKey}
    public DoorType doorType = DoorType.None;

    public bool showNameHighlight = true;
    public string doorName = "없음";

    private string highlightText = "잠금헤제";

    private DoorTextProduce doorTextProduce;
    private ThemedKeyItemController themedKeyItemController;
    private bool doorTextProduceActive = false;

    private void Start()
    {
        if(doorTextProduce = GetComponent<DoorTextProduce>())
        {
            doorTextProduceActive = true;
        }

        switch (doorType)
        {
            case DoorType.ThemedKey:
                themedKeyItemController = GetComponent<ThemedKeyItemController>();
                break;
        }
    }

    public void Highlight(bool isHighlighted)
    {
        if (showNameHighlight)
        {
            if (isHighlighted)
            {
                ExamineUIController.instance.interactionItemNameUI.text = doorName;
                ExamineUIController.instance.interactionTextUI.text = highlightText;
                ExamineUIController.instance.interactionNameMainUI.SetActive(true);
            }
            else
            {
                ExamineUIController.instance.interactionItemNameUI.text = doorName;
                ExamineUIController.instance.interactionNameMainUI.SetActive(false);
            }
        }
    }

    public void Interaction()
    {
        if (doorTextProduceActive)
        {
            doorTextProduce.isActive = true;
        }

        switch (doorType) 
        {
            case DoorType.None:
                break;
            case DoorType.ThemedKey:
                themedKeyItemController.ObjectInteract();
                break;
        }

    }
}
