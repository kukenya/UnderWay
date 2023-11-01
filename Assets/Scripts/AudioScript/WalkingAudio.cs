using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAudio : MonoBehaviour
{
    public PlayerStatus player;
    private bool isStop = false;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isMoving && !audioSource.isPlaying && !isStop)
        {
            audioSource.Play();
            print("a");
        }
        else if ((audioSource.isPlaying && !player.isMoving) || isStop)
        {
            audioSource.Pause();
        }
    }
     
    public void OnAudio()
    {
        isStop = false;
    }

    public void OffAudio()
    {
        isStop = true;
    }
}
