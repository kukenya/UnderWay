using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLight : MonoBehaviour
{
    public bool isActive = false;

    public Light light;
    float timer;
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

    void Start()
    {
        inputActions.Player.FlashLight.performed += FlashLight_performed;
    }

    private void Update()
    {
        if (DataManager.instance.nowPlayer.flashLightBattery == 0)
        {
            light.enabled = false;
        }
        else if (timer > 1200 && isActive)
        {
            timer = 0;
            DataManager.instance.nowPlayer.flashLightBattery--;
        }
        else
        {
            timer += Time.deltaTime;
        }
        
        
    }

    private void FlashLight_performed(InputAction.CallbackContext obj)
    {
        if(DataManager.instance.nowPlayer.flashLightBattery != 0 && isActive)
        {
            if (light.enabled)
            {
                light.enabled = false;
            }
            else
            {
                light.enabled = true;
            }
        } 
    }
}
