using ExamineSystem;
using FlashlightSystem;
using GeneratorSystem;
using KeypadSystem;
using PadlockSystem;
using PhoneInputSystem;
using ThemedKeySystem;
using SafeUnlockSystem;
using FuseboxSystem;
using NoteSystem;
using LeverPuzzleSystem;
using UnityEngine;

namespace AdventurePuzzleKit
{
    public class SingleObjectHighlight : MonoBehaviour
    {
        [Header("Item Name")]
        public string itemName;

        [SerializeField] private bool showNameHighlight = false;

        [Header("System Name")]
        [SerializeField] private SystemType _systemType = SystemType.None;

        private FlashlightItemController _flashlightItemController;
        private NoteController _noteController;
        private KeypadItemController _keypadItemController;
        private ThemedKeyItemController _themedKeyItemController;
        private PhonePadItemController _phonepadItemController;
        private PadlockItemController _padlockItemController;
        private SafeItemController _safeItemController;
        private ButtonDoorController _buttonDoorController;
        private FuseItemController _fuseboxItemController;
        private LeverItemController _leverItemController;
        private SingleObjectHighlight _singleObjectHighlight;

        private enum SystemType { None, FlashlightSys, GeneratorSys, NoteSys, GasMaskSys, KeypadSys, ThemedKeySys, PhoneSys, PadlockSys, ChessSys, SafeSys, buttonDoorSys, FuseBoxSys, leverSys }

        void Start()
        {
            switch (_systemType)
            {
                case SystemType.FlashlightSys: _flashlightItemController = GetComponent<FlashlightItemController>(); break;
                case SystemType.NoteSys: _noteController = GetComponent<NoteController>(); break;
                case SystemType.KeypadSys: _keypadItemController = GetComponent<KeypadItemController>(); break;
                case SystemType.ThemedKeySys: _themedKeyItemController = GetComponent<ThemedKeyItemController>(); break;
                case SystemType.PhoneSys: _phonepadItemController = GetComponent<PhonePadItemController>(); break;
                case SystemType.PadlockSys: _padlockItemController = GetComponent<PadlockItemController>(); break;
                case SystemType.SafeSys: _safeItemController = GetComponent<SafeItemController>(); break;
                case SystemType.buttonDoorSys: _buttonDoorController = GetComponent<ButtonDoorController>(); break;
                case SystemType.FuseBoxSys: _fuseboxItemController = GetComponent<FuseItemController>(); break;
                case SystemType.leverSys: _leverItemController = GetComponent<LeverItemController>(); break;
            }
        }

        public void MainHighlight(bool isHighlighted)
        {
            if (showNameHighlight)
            {
                if (isHighlighted)
                {
                    ExamineUIController.instance.interactionItemNameUI.text = itemName;
                    ExamineUIController.instance.interactionNameMainUI.SetActive(true);
                }
                else
                {
                    ExamineUIController.instance.interactionItemNameUI.text = itemName;
                    ExamineUIController.instance.interactionNameMainUI.SetActive(false);
                }
            }
        }

        public void InteractionType()
        {
            switch (_systemType)
            {
                case SystemType.FlashlightSys: _flashlightItemController.ObjectInteract(); break;
                case SystemType.NoteSys: _noteController.DisplayNotes(); break;
                case SystemType.KeypadSys: _keypadItemController.ShowKeypad(); break;
                case SystemType.ThemedKeySys: _themedKeyItemController.ObjectInteract(); break;
                case SystemType.PhoneSys: _phonepadItemController.ShowKeypad(); break;
                case SystemType.PadlockSys: _padlockItemController.ObjectInteract(); break;
                case SystemType.SafeSys: _safeItemController.ShowSafeLock(); break;
                case SystemType.buttonDoorSys: _buttonDoorController.PlayAnimation(); break;
                case SystemType.FuseBoxSys: _fuseboxItemController.ObjectInteract(); break;
                case SystemType.leverSys: _leverItemController.ObjectInteract(); break;
            }
        }
    }
}
