using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenDoorTrigger : MonoBehaviour
{
    public bool isActive = false;
    public bool messageActive = false;

    public int messageNum = 1;
    public int messageNumOrigin = 1;

    public string message1, message2;

    public StoryTelling storyTelling;
    public Animator screenDoor;
    public Animator subwayDoor;
    public StoryAudioManager storyAudioManager;
    public StoryGameManager storyGameManager;

    UnderWay inputActions;

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
        if(messageNumOrigin != 3 && messageNumOrigin != 4 && messageNumOrigin != 5 && messageActive)
        {
            MessageNumPlus();
        }
    }

    private void Update()
    {
        print(messageNum);

        if (messageActive)
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
                    storyTelling.OnStoryText(message2, true);
                    break;
                case 3:
                    storyAudioManager.StorySoundPlay(0);
                    MessageNumPlus();
                    break;
                case 4:
                    if (storyAudioManager.audioSource.isPlaying == false)
                    {
                        MessageNumPlus();
                    }
                    break;
                case 5:
                    screenDoor.Play("ScreenDoorOpen");
                    subwayDoor.Play("DoorOpen");
                    MessageNumPlus();
                    break;
                case 6:
                    if (screenDoor.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && screenDoor.GetCurrentAnimatorStateInfo(0).IsName("ScreenDoorOpen"))
                    {
                        storyGameManager.OffPlayerStop();
                        MessageNumPlus();
                    }
                    break;
                case 7:
                    this.gameObject.layer = 0;
                    this.gameObject.tag = "Untagged";
                    this.enabled = false;
                    break;
                default:
                    break;
            }
        } 
    }
    public void MessageNumPlus()
    {
        messageNumOrigin += 1;
        messageNum = messageNumOrigin;
        print(messageNumOrigin);
    }
}
