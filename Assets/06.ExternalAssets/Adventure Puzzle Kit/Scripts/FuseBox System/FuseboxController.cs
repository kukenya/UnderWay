﻿using UnityEngine;
using UnityEngine.Events;
using AdventurePuzzleKit;
using ExamineSystem;

namespace FuseboxSystem
{
    public class FuseboxController : MonoBehaviour
    {
        [Header("Fuse Inserted?")]
        [SerializeField] private bool fuse1Inserted = false;
        [SerializeField] private bool fuse2Inserted = false;
        [SerializeField] private bool fuse3Inserted = false;
        [SerializeField] private bool fuse4Inserted = false;

        private bool powerOn = false;

        [Header("Individual Fuses (Parented to the fusebox)")]
        [SerializeField] private GameObject fuseObject1 = null;
        [SerializeField] private GameObject fuseObject2 = null;
        [SerializeField] private GameObject fuseObject3 = null;
        [SerializeField] private GameObject fuseObject4 = null;

        [Header("Fusebox Lights (Parented to the fusebox)")]
        [SerializeField] private GameObject light1 = null;
        [SerializeField] private GameObject light2 = null;
        [SerializeField] private GameObject light3 = null;
        [SerializeField] private GameObject light4 = null;

        [Header("Sounds")]
        [SerializeField] private string fuseInsert = "FuseBoxZap";

        [Header("Materials (Inside the project folder)")]
        [SerializeField] private Material greenButton = null;

        [Header("Power on - When all fuses are inserted")]
        [SerializeField] private UnityEvent powerUp = null;

        void Start()
        {
            if (fuse1Inserted)
            {
                light1.GetComponent<Renderer>().material = greenButton;
                fuseObject1.SetActive(true);
            }

            if (fuse2Inserted)
            {
                light2.GetComponent<Renderer>().material = greenButton;
                fuseObject2.SetActive(true);
            }

            if (fuse3Inserted)
            {
                light3.GetComponent<Renderer>().material = greenButton;
                fuseObject3.SetActive(true);
            }

            if (fuse4Inserted)
            {
                light4.GetComponent<Renderer>().material = greenButton;
                fuseObject4.SetActive(true);
            }
        }

        void FusesEngaged()
        {
            if (fuse1Inserted && fuse2Inserted && fuse3Inserted && fuse4Inserted)
            {
                powerOn = true;
                gameObject.tag = "Untagged";
                powerUp.Invoke();
            }
        }

        public void CheckFuseBox()
        {
            if (DataManager.instance.nowPlayer.fuse <= 0 && !powerOn)
            {
                AKAudioManager.instance.Play(fuseInsert);
            }

            if (DataManager.instance.nowPlayer.fuse >= 1 && !powerOn)
            {
                if (!fuse1Inserted)
                {
                    fuseObject1.SetActive(true);
                    fuse1Inserted = true;
                    light1.GetComponent<Renderer>().material = greenButton;
                    DataManager.instance.nowPlayer.fuse -= 1;
                }

                else if (!fuse2Inserted)
                {
                    fuseObject2.SetActive(true);
                    fuse2Inserted = true;
                    light2.GetComponent<Renderer>().material = greenButton;
                    DataManager.instance.nowPlayer.fuse -= 1;
                }

                else if (!fuse3Inserted)
                {
                    fuseObject3.SetActive(true);
                    fuse3Inserted = true;
                    light3.GetComponent<Renderer>().material = greenButton;
                    DataManager.instance.nowPlayer.fuse -= 1;
                }

                else if (!fuse4Inserted)
                {
                    fuseObject4.SetActive(true);
                    fuse4Inserted = true;
                    light4.GetComponent<Renderer>().material = greenButton;
                    DataManager.instance.nowPlayer.fuse -= 1;
                }

                AKAudioManager.instance.Play(fuseInsert);
                FusesEngaged();
            }
        }
    }
}
