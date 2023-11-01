using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EscalatorExitTrigger : MonoBehaviour
{
    public StoryTelling storyTelling;
    public string message;

    UnderWay inputActions;
    public string message1;
    public string message2;
    public string message3;

    public StoryGameManager storyGameManager;

    public bool isActive = false;
    public int messageNum = 1;
    public int messageNumOrigin = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            storyTelling.OnStoryText(message, true);
        }
    }

    private void Awake()
    {
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
        inputActions.Player.LeftMouseClick.performed += LeftMouseClick_performed;
    }

    private void LeftMouseClick_performed(InputAction.CallbackContext obj)
    {
        if (isActive)
        {
            MessageNumPlus();
        }
    }

    public void TextProducing()
    {
        isActive = true;
    }

    private void Update()
    {
        if (isActive)
        {
            switch (messageNum)
            {
                case 1:
                    messageNum = 0;
                    storyGameManager.OnPlayerStop();
                    storyTelling.OnStoryText(message1, false);
                    break;
                case 2:
                    messageNum = 0;
                    storyTelling.OnStoryText(message2, false);
                    break;
                case 3:
                    messageNum = 0;
                    storyTelling.OnStoryText(message3, true);
                    break;
                case 4:
                    storyGameManager.OffPlayerStop();
                    Destroy(this.gameObject);
                    break;
            }
        }
    }

    public void MessageNumPlus()
    {
        messageNumOrigin += 1;
        messageNum = messageNumOrigin;
    }

}
