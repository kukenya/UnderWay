﻿using AdventurePuzzleKit;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PhoneInputSystem
{
    public class PhonePadTrigger : MonoBehaviour
    {
        [Header("Keypad Object")]
        [SerializeField] private PhonePadItemController myKeypad = null;

        private const string playerTag = "Player";

        private bool canUse;
        UnderWay inputActions;
        InputAction interaction;

        private void Awake()
        {
            inputActions = new UnderWay();
        }
        private void OnEnable()
        {
            interaction = inputActions.Player.Interaction;
            interaction.Enable();
        }

        private void OnDisable()
        {
            interaction.Disable();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                canUse = true;
                AKUIManager.instance.triggerInteractPrompt.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                canUse = false;
                AKUIManager.instance.triggerInteractPrompt.SetActive(false);
            }
        }

        private void Update()
        {
            if (canUse)
            {
                if (interaction.ReadValue<float>() != 0)
                {
                    myKeypad.ShowKeypad();
                }
            }
        }
    }
}
