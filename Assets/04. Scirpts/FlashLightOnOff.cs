using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLightOnOff : MonoBehaviour
{
    [SerializeField]
    GameObject FlashlightLight;
    private bool FlashlightOnOff = false;

    UnderWay inputActions;
    InputAction flashOnOff;

    // Start is called before the first frame update
    void Start()
    {
        FlashlightLight.gameObject.SetActive(false);
    }

    private void Awake()
    {
        inputActions = new UnderWay();
    }

    private void OnEnable()
    {
        flashOnOff = inputActions.Player.FlashLight;
        flashOnOff.Enable();
    }

    private void OnDisable()
    {
        flashOnOff.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (flashOnOff.ReadValue<float>() != 0)
        {
            if(FlashlightOnOff == false)
            {
                FlashlightLight.gameObject.SetActive(true);
                FlashlightOnOff = true;
            }
            else
            {
                FlashlightLight.gameObject.SetActive(false);
                FlashlightOnOff = false;
            }
        }
    }
}
