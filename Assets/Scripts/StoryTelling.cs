using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StoryTelling : MonoBehaviour
{
    public Text storyTellingText;
    public GameObject storyCanvas;

    public float messageSpeed = 0.1f;

    public bool onceStory = false;

    Coroutine typing = null;

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
        typing = null;
    }

    private void LeftMouseClick_performed(InputAction.CallbackContext obj)
    {
        if (onceStory)
        {
            storyCanvas.SetActive(false);
            onceStory = false;
        }
    }

    public void OnStoryText(string message, bool isOnceStory)
    {
        storyCanvas.SetActive(true);
        onceStory = isOnceStory;
        if (typing != null)
        {
            StopCoroutine(typing);
        }
        typing = StartCoroutine(Typing(storyTellingText, message, messageSpeed));
    }

    IEnumerator Typing(Text typingText, string message, float speed)
    {
        for (int i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
    }
}
