using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerMessageSave : MonoBehaviour
{
    public StoryTelling storyTelling;
    public StoryGameManager storyGameManager;
    public string message;

    public int messageNum = 1;
    public int messageNumOrigin = 1;

    UnderWay inputActions;
    public bool isActive = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
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
                    storyTelling.OnStoryText(message, true);
                    break;
                case 2:
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
