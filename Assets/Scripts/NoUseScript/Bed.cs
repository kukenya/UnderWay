using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public float plusEnergy = 30f;

    public Fade fade;
    public Transform sleepPos;
    public Transform playerPos;
    private Vector3 lastPos;
    private Quaternion lastRot;
    

    private bool sleeping = false;
    private float time = 0f;

    public void InSleep()
    {
        /*if(DataManager.instance.nowPlayer.energy < 60)
        {
            playerPos.gameObject.GetComponent<CharacterController>().enabled = false;
            DataManager.instance.nowPlayer.energy = Mathf.Clamp(DataManager.instance.nowPlayer.energy + plusEnergy, 0, 100);
            fade.isChange = true;
            sleeping = true;
            TelToSleepPos();
        }*/
    }

    private void Update()
    {
        if (sleeping)
        {
            time += Time.deltaTime;
            if(time >= fade.fadeTime * 2)
            {
                sleeping = false;
                playerPos.position = lastPos;
                playerPos.rotation = lastRot;
                playerPos.gameObject.GetComponent<CharacterController>().enabled = true;
                time = 0f;
            }
        }
    }

    public void TelToSleepPos()
    {
        lastPos = playerPos.position;
        lastRot = playerPos.rotation;
        playerPos.position = sleepPos.position;
        playerPos.rotation = sleepPos.rotation;
    }
}
