using UnityEngine;
using AdventurePuzzleKit;
using UnityEngine.InputSystem;

namespace SafeUnlockSystem
{
    public class SafeTrigger : MonoBehaviour
    {
        public UnderWay inputActions;
        [Header("Safe Controller Object")]
        [SerializeField] private SafeItemController _safeItemController = null;

        private bool canUse;
        private const string playerTag = "Player";
        
        InputAction Interaction;

        private void Awake()
        {
            inputActions = new UnderWay();
        }


        private void OnEnable()
        {
            Interaction = inputActions.Player.Interaction;
            Interaction.Enable();
        }

        private void OnDisable()
        {
            Interaction.Disable();
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
                if (Interaction.ReadValue<float>() != 0)
                {
                    AKUIManager.instance.triggerInteractPrompt.SetActive(false);
                    _safeItemController.ShowSafeLock();
                }
            }
        }
    }
}
