using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubwayRouteProduce : MonoBehaviour
{
    UnderWay inputActions;
    public List<string> message = new List<string>();

    public StoryTelling storyTelling;
    public StoryGameManager storyGameManager;

    public bool isActive = false;
    public int messageNum = 1;
    public int messageNumOrigin = 1;

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
        if (isActive && messageNumOrigin != 1 && messageNumOrigin != 2 && messageNumOrigin != 8 && messageNumOrigin != 9)
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
                    MessageNumPlus();
                    break;
                case 2:
                    MessageNumPlus();
                    break;
                case 3:
                    messageNum = 0;
                    storyGameManager.OnPlayerStop();
                    storyTelling.OnStoryText(message[0], false);
                    break;
                case 4:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[1], false);
                    break;
                case 5:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[2], false);
                    break;
                case 6:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[3], false);
                    break;
                case 7:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[4], false);
                    break;
                case 8:
                    MessageNumPlus();
                    break;
                case 9:
                    MessageNumPlus();
                    break;
                case 10:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[5], false);
                    break;
                case 11:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[6], false);
                    break;
                case 12:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[7], false);
                    break;
                case 13:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[8], false);
                    break;
                case 14:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[9], false);
                    break;
                case 15:
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
