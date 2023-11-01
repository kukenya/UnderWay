using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformTrigger1 : MonoBehaviour
{
    public Canvas playerStatusCanvas;
    public StoryTelling storyTelling;
    public StoryAudioManager storyAudioManager;
    public StoryGameManager storyGameManager;
    public Animator screenDoor;
    public Animator mainCamera;
    public PlayerEquipItem playerEquipItem;

    public int messageNum = 1;
    public int messageNumOrigin = 1;

    public bool isActive = false;

    public string[] message;
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
        playerStatusCanvas.enabled = false;
    }

    private void LeftMouseClick_performed(InputAction.CallbackContext obj)
    {
        if (isActive && messageNumOrigin != 2 && messageNumOrigin != 3 
            && messageNumOrigin != 4  && messageNumOrigin != 6 && messageNumOrigin != 7 
            && messageNumOrigin != 9 && messageNumOrigin != 10 && messageNumOrigin != 11)
        {
            MessageNumPlus();
        }
    }

    void Update()
    {
        if (isActive)
        {
            switch (messageNum)
            {
                case 1:
                    messageNum = 0;
                    storyGameManager.OnPlayerStop();
                    storyTelling.OnStoryText(message[0], true);
                    break;
                case 2:
                    screenDoor.Play("ScreenDoorClose");
                    MessageNumPlus();
                    break;
                case 3:
                    if (screenDoor.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && screenDoor.GetCurrentAnimatorStateInfo(0).IsName("ScreenDoorClose"))
                    {
                        mainCamera.enabled = true;
                        mainCamera.Play("BackHead");
                        MessageNumPlus();
                    }
                    break;
                case 4:
                    if(mainCamera.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && mainCamera.GetCurrentAnimatorStateInfo(0).IsName("BackHead"))
                    {
                        MessageNumPlus();
                    }
                    break;
                case 5:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[1], true);
                    break;
                case 6:
                    storyAudioManager.StorySoundPlay(0);
                    MessageNumPlus();
                    break;
                case 7:
                    if (storyAudioManager.audioSource.isPlaying == false)
                    {
                        MessageNumPlus();
                    }
                    break;
                case 8:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[2], true);
                    break;
                case 9:
                    mainCamera.Play("headSwing");
                    MessageNumPlus();
                    break;
                case 10:
                    if (mainCamera.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && mainCamera.GetCurrentAnimatorStateInfo(0).IsName("headSwing"))
                    {
                        storyAudioManager.StorySoundPlay(0);
                        mainCamera.enabled = false;
                        MessageNumPlus();
                    }
                    break;
                case 11:
                    if (storyAudioManager.audioSource.isPlaying == false)
                    {
                        MessageNumPlus();
                    }
                    break;
                case 12:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[3], false);
                    break;
                case 13:
                    messageNum = 0;
                    storyTelling.OnStoryText(message[4], true);
                    break;
                case 14:
                    messageNum = 0;
                    UIManager.instance.tutorialTextUI.SetActive(true);
                    break;
                case 15:
                    messageNum = 0;
                    UIManager.instance.tutorialTextUI.SetActive(false);
                    UIManager.instance.questUI.SetActive(true);
                    playerStatusCanvas.enabled = true;
                    storyGameManager.OffPlayerStop();
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    public void MessageNumPlus()
    {
        messageNumOrigin += 1;
        messageNum = messageNumOrigin;
    }
}
