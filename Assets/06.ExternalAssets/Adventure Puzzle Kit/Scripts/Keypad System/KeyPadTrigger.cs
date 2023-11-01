using AdventurePuzzleKit;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KeypadSystem
{
    public class KeyPadTrigger : MonoBehaviour
    {
        [Header("Keypad Object")]
        [SerializeField] private KeypadItemController myKeypad = null;

        private const string playerTag = "Player";
        private bool canUse;

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

        public void InteractionClick(InputAction.CallbackContext context)
        {
            if(context.action.phase == InputActionPhase.Performed)
            {
                if (canUse)
                {
                    myKeypad.ShowKeypad();
                }
            }
        }
    }
}
