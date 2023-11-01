using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip fearSound1;

    public bool isSoundPlaying = false;
    public float time = 0;

    public int fearGrade = 0;

    public AudioClip[] grade1Music = new AudioClip[4];
    public AudioClip[] grade2Music = new AudioClip[5];
    public AudioClip[] grade3Music = new AudioClip[6];

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = fearSound1;
    }

    // Update is called once per frame
    void Update()
    {
        if(DataManager.instance.nowPlayer.mentality <= 70 && DataManager.instance.nowPlayer.mentality > 50 && fearGrade != 1)
        {
            fearGrade = 1;
        }
        else if(DataManager.instance.nowPlayer.mentality <= 50 && DataManager.instance.nowPlayer.mentality > 30 && fearGrade != 2)
        {
            fearGrade = 2;
        }
        else if(DataManager.instance.nowPlayer.mentality <= 30 && fearGrade != 3)
        {
            fearGrade = 3;
        }
    }

    public void FearSoundPlay()
    {
        if (audioSource.clip == null)
        {
            switch (fearGrade)
            {
                case 1:
                    audioSource.clip = grade1Music[Random.Range(0, grade1Music.Length)];
                    break;
                case 2:
                    audioSource.clip = grade2Music[Random.Range(0, grade2Music.Length)];
                    break;
                case 3:
                    audioSource.clip = grade3Music[Random.Range(0, grade3Music.Length)];
                    break;
                default:
                    break;
            }
        }
        else if(audioSource.clip != null && !audioSource.isPlaying)
        {
            if(time > 10f && Random.Range(0, 6) == 1)
            {
                time = 0;
                audioSource.Play();
            }
            else
            {
                time += Time.deltaTime;
            }
        }
        
    }
}
