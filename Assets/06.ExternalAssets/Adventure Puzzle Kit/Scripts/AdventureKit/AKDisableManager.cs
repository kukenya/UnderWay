using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.Utility;
using UnityStandardAssets.ImageEffects;

namespace AdventurePuzzleKit
{
    public class AKDisableManager : MonoBehaviour
    {
        [Header("First Person Variables")]
        [SerializeField] private bool isFirstPerson = false;
        public PlayerController player = null;

        [Header("Third Person Variables")]
        [SerializeField] private bool isThirdPerson = false;
        public ThirdPersonUserControl thirdPersonController = null;
        public SimpleMouseRotator thirdPersonRotator = null;

        [Header("Generic Variables")]
        [SerializeField] private Image crosshair = null;        
        [SerializeField] private AdventureKitRaycast raycastManager = null;
        [SerializeField] private BlurOptimized blur = null;

        public static AKDisableManager instance;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }

            if (isThirdPerson)
            {
                ShowCursor(false);
            }
        }

        void ShowCursor(bool showCursor)
        {
            if (showCursor)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public void DisablePlayerDefault(bool disable)
        {
            if (disable)
            {
                raycastManager.enabled = false;
                ShowCursor(true);
                AKUIManager.instance.isInteracting = true;
                crosshair.enabled = false;

                if (isFirstPerson)
                {
                    player.enabled = false;
                }

                if (isThirdPerson)
                {
                    thirdPersonController.enabled = false;
                    thirdPersonRotator.enabled = false;
                }
            }

            else
            {
                raycastManager.enabled = true;
                ShowCursor(false);
                AKUIManager.instance.isInteracting = false;
                crosshair.enabled = true;

                if (isFirstPerson)
                {
                    player.enabled = true;
                }

                if (isThirdPerson)
                {
                    thirdPersonController.enabled = true;
                    thirdPersonRotator.enabled = true;
                }
            }
        }

        public void DisablePlayerExamine(bool disable)
        {
            if (disable)
            {
                raycastManager.enabled = false;
                ShowCursor(true);
                AKUIManager.instance.isInteracting = true;
                crosshair.enabled = false;
                blur.enabled = true;

                if (isFirstPerson)
                {
                    player.enabled = false;
                }

                if (isThirdPerson)
                {
                    thirdPersonController.enabled = false;
                    thirdPersonRotator.enabled = false;
                }
            }
            else
            {
                raycastManager.enabled = true;
                ShowCursor(false);
                AKUIManager.instance.isInteracting = false;
                crosshair.enabled = true;
                blur.enabled = false;

                if (isFirstPerson)
                {
                    player.enabled = true;
                }

                if (isThirdPerson)
                {
                    thirdPersonController.enabled = true;
                    thirdPersonRotator.enabled = true;
                }
            }
        }

        public void DisablePlayer(bool disable)
        {
            if (disable)
            {
                AKUIManager.instance.isInteracting = true;
                crosshair.enabled = false;

                if (isFirstPerson)
                {
                    player.enabled = false;
                }

                if (isThirdPerson)
                {
                    thirdPersonController.enabled = false;
                    thirdPersonRotator.enabled = false;
                }
            }
            else
            {
                AKUIManager.instance.isInteracting = false;
                crosshair.enabled = true;

                if (isFirstPerson)
                {
                    player.enabled = true;
                }

                if (isThirdPerson)
                {
                    thirdPersonController.enabled = true;
                    thirdPersonRotator.enabled = true;
                }
            }
        }

        public void DisablePlayerInventory(bool disable)
        {
            if (disable)
            {
                raycastManager.enabled = false;
                crosshair.enabled = false;

                if (isFirstPerson)
                {
                    player.enabled = false;
                }

                if (isThirdPerson)
                {
                    thirdPersonController.enabled = false;
                    thirdPersonRotator.enabled = false;
                }
            }
            else
            {
                raycastManager.enabled = true;           
                crosshair.enabled = true;

                if (isFirstPerson)
                {
                    player.enabled = true;
                }

                if (isThirdPerson)
                {
                    thirdPersonController.enabled = true;
                    thirdPersonRotator.enabled = true;
                }
            }
        }
    }
}