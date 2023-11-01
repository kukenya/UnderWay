using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryAudioManager : MonoBehaviour
{
    public bool isAudioPlaying = false;
    public AudioClip[] storyAudioes;
    public AudioSource audioSource;

    public void StorySoundPlay(int num)
    {
        audioSource.clip = storyAudioes[num];
        audioSource.Play();
    }
}
