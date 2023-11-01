using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExamineSystem;

public class InteractionHighlight : MonoBehaviour
{
    public bool showNameHighlight = true;
    public string interactionName = "앉기";

    public string highlightText = "잠금헤제";
    private EndingProcess endingProcess;
    private ScreenDoorTrigger screenDoorTrigger;
    public EventType eventType = EventType.None;

    public enum EventType { None, ScreenDoorOpen, Ending}

    private void Start()
    {
        switch (eventType)
        {
            case EventType.None:
                break;
            case EventType.ScreenDoorOpen:
                screenDoorTrigger = GetComponent<ScreenDoorTrigger>();
                break;
            case EventType.Ending:
                endingProcess = GetComponent<EndingProcess>();
                break;
        }
    }

    public void Highlight(bool isHighlighted)
    {
        if (showNameHighlight)
        {
            if (isHighlighted)
            {
                ExamineUIController.instance.interactionItemNameUI.text = interactionName;
                ExamineUIController.instance.interactionTextUI.text = highlightText;
                ExamineUIController.instance.interactionNameMainUI.SetActive(true);
            }
            else
            {
                ExamineUIController.instance.interactionItemNameUI.text = interactionName;
                ExamineUIController.instance.interactionNameMainUI.SetActive(false);
            }
        }
    }

    public void Interaction()
    {
        switch (eventType)
        {
            case EventType.None:
                break;
            case EventType.ScreenDoorOpen:
                screenDoorTrigger.messageActive = true;
                break;
            case EventType.Ending:
                endingProcess.isActive = true;
                break;
        }
    }
}
