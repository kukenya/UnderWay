using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LightMapStart : MonoBehaviour
{
    public string message1;
    public string message2;
    public string message3;

    public int messageNum = 1;
    public int messageNumOrigin = 1;

    public StoryTelling storyTelling;
    public Animator mainCamera;

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
        Cursor.visible = false;
    }

    private void LeftMouseClick_performed(InputAction.CallbackContext obj)
    {
        if(messageNumOrigin != 4)
        {
            MessageNumPlus();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (messageNum)
        {
            case 1:
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
                }
                break;
            case 5:
                LoadingSceneController.Instance.LoadScene(2);
                Destroy(this.gameObject);
                break;
        }
    }
    public void MessageNumPlus()
    {
        messageNumOrigin += 1;
        messageNum = messageNumOrigin;
    }

}
