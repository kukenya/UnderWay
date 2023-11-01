using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityStandardAssets.ImageEffects;

public class GameStartProcess : MonoBehaviour
{
    public Canvas mainUi;
    public Canvas crosshairUi;

    public int messageNum = 1;
    public int messageNumOrigin = 1;

    public string message1;
    public string message2;
    public string message3;
    public string message2_1, message2_2, message2_3, message2_4, message2_5, message2_6;
    public string message3_1, message3_2, message3_3, message3_4, message3_5;

    public Animator mainCamera;
    public StoryTelling storyTelling;
    public StoryGameManager storyGameManager;
    public VignetteAndChromaticAberration cameraEffect;
    public StormLighter stormLighter;
    public ScreenDoorTrigger screenDoorTrigger;

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
        Cursor.lockState = CursorLockMode.Locked;
        storyGameManager.OnPlayerStop();
        mainUi.enabled = false;
        crosshairUi.enabled = false;
        stormLighter.transform.gameObject.SetActive(false);
    }

    public void LeftMouseClick_performed(InputAction.CallbackContext obj)
    {
        if (messageNumOrigin != 4 && messageNumOrigin != 5 && messageNumOrigin != 13 && messageNumOrigin != 14)
        {
            MessageNumPlus();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (messageNum)
        {
            /*case 1:
                messageNum = 0;
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
                mainCamera.Play("EyeDown");
                if (mainCamera.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && mainCamera.GetCurrentAnimatorStateInfo(0).IsName("EyeDown"))
                {
                    MessageNumPlus();
                    print(messageNum);
                }
                break;*/
            case 5:
                mainCamera.Play("EyeUp");
                if (mainCamera.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && mainCamera.GetCurrentAnimatorStateInfo(0).IsName("EyeUp"))
                {
                    MessageNumPlus();
                    print(messageNum);
                }
                break;
            case 6:
                messageNum = 0;
                storyTelling.OnStoryText(message2_1, false);
                break;
            case 7:
                messageNum = 0;
                storyTelling.OnStoryText(message2_2, false);
                break;
            case 8:
                messageNum = 0;
                storyTelling.OnStoryText(message2_3, false);
                break;
            case 9:
                messageNum = 0;
                storyTelling.OnStoryText(message2_4, false);
                break;
            case 10:
                messageNum = 0;
                storyTelling.OnStoryText(message2_5, false);
                break;
            case 11:
                stormLighter.transform.gameObject.SetActive(true);
                stormLighter.LighterOpenOrClose();
                MessageNumPlus();
                break;
            case 12:
                messageNum = 0;
                storyTelling.OnStoryText(message2_6, true);
                break;
            case 13:
                mainCamera.Play("headSwing");
                if (mainCamera.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && mainCamera.GetCurrentAnimatorStateInfo(0).IsName("headSwing"))
                {
                    MessageNumPlus();
                    crosshairUi.enabled = true;
                    storyGameManager.OffPlayerStop();
                    mainCamera.enabled = false;
                }
                break;
            case 14:
                if (DataManager.instance.nowPlayer.hasBackpack)
                {
                    MessageNumPlus();
                    storyGameManager.OnPlayerStop();
                }
                break;
            case 15:
                messageNum = 0;
                storyTelling.OnStoryText(message3_1, false);
                break;
            case 16:
                messageNum = 0;
                storyTelling.OnStoryText(message3_2, false);
                break;
            case 17:
                messageNum = 0;
                mainUi.enabled = true;
                storyTelling.OnStoryText(message3_3, false);
                break;
            case 18:
                messageNum = 0;
                storyTelling.OnStoryText(message3_4, false);
                break;
            case 19:
                messageNum = 0;
                storyTelling.OnStoryText(message3_5, true);
                break;
            case 20:
                storyGameManager.OffPlayerStop();
                screenDoorTrigger.isActive = true;
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
    public void MessageNumPlus()
    {
        messageNumOrigin += 1;
        messageNum = messageNumOrigin;
    }
}
