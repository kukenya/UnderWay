using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstLightText : MonoBehaviour
{
    UnderWay inputActions;
    public string message1;
    public string message2;
    public string message3;
    public string message4;

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
        if (isActive && messageNumOrigin != 2)
        {
            MessageNumPlus();
        }
    }

    private void OnTriggerEnter(Collider other)
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
                    StoryGameManager.instance.OnPlayerStop();
                    storyTelling.OnStoryText(message1, true);
                    break;
                case 2:
                    StoryGameManager.instance.OffPlayerStop();
                    if (DataManager.instance.nowPlayer.hasFlashLIght)
                    {
                        MessageNumPlus();
                    }
                    break;
                case 3:
                    messageNum = 0;
                    StoryGameManager.instance.OnPlayerStop();
                    storyTelling.OnStoryText(message2, false);
                    break;
                case 4:
                    messageNum = 0;
                    storyTelling.OnStoryText(message3, false);
                    break;
                case 5:
                    messageNum = 0;
                    storyTelling.OnStoryText(message4, true);
                    break;
                case 6:
                    StoryGameManager.instance.OffPlayerStop();
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
