using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryGameManager : MonoBehaviour
{
    public UnityEvent onPlayerStop;
    public UnityEvent offPlayerStop;

    public UnityEvent onPlayerLight;
    public UnityEvent offPlayerLight;

    public static StoryGameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void OnPlayerStop()
    {
        onPlayerStop.Invoke();
    }

    public void OffPlayerStop()
    {
        offPlayerStop.Invoke();
    }

    public void OnPlayerLight()
    {
        onPlayerLight.Invoke();
    }

    public void OffPlayerLight()
    {
        offPlayerLight.Invoke();
    }
}
