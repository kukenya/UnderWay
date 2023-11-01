using ExamineSystem;
using KeypadSystem;
using PadlockSystem;
using ThemedKeySystem;
using SafeUnlockSystem;
using FuseboxSystem;
using NoteSystem;
using UnityEngine;

namespace AdventurePuzzleKit
{
    public class AKItemController : MonoBehaviour
    {
        [SerializeField] private SystemType _systemType = SystemType.None;

        private ExamineItemController _examineItemController;
        private NoteController _noteController;
        private KeypadItemController _keypadItemController;
        private ThemedKeyItemController _themedKeyItemController;
        private PadlockItemController _padlockItemController;
        private SafeItemController _safeItemController;
        private ButtonDoorController _buttonDoorController;
        private FuseItemController _fuseboxItemController;
        private DoorTextProduce textProduce;
        private DoorController doorController;
        private InteractionHighlight interactionHighlight;

        private enum SystemType { None, ExamineSys, NoteSys,KeypadSys, ThemedKeySys, 
             PadlockSys,  SafeSys, buttonDoorSys, FuseBoxSys, ObjectHighlight, InteractProduceText, DoorController, InteractionHighlight}

        private void Start()
        {
            switch (_systemType)
            {
                case SystemType.ExamineSys: _examineItemController = GetComponent<ExamineItemController>(); break;
                case SystemType.NoteSys: _noteController = GetComponent<NoteController>(); break;
                case SystemType.KeypadSys: _keypadItemController = GetComponent<KeypadItemController>(); break;
                case SystemType.ThemedKeySys: _themedKeyItemController = GetComponent<ThemedKeyItemController>(); break;
                case SystemType.PadlockSys: _padlockItemController = GetComponent<PadlockItemController>(); break;
                case SystemType.SafeSys: _safeItemController = GetComponent<SafeItemController>(); break;
                case SystemType.buttonDoorSys: _buttonDoorController = GetComponent<ButtonDoorController>(); break;
                case SystemType.FuseBoxSys: _fuseboxItemController = GetComponent<FuseItemController>(); break;
                case SystemType.InteractProduceText: textProduce = GetComponent<DoorTextProduce>(); break;
                case SystemType.DoorController: doorController = GetComponent<DoorController>(); break;
                case SystemType.InteractionHighlight: interactionHighlight = GetComponent<InteractionHighlight>(); break;
            }
        }

        public void Highlight(bool highlight)
        {
            switch (_systemType)
            {
                case SystemType.ExamineSys:
                    if (highlight)
                    {
                        _examineItemController.MainHighlight(true);
                    }
                    else
                    {
                        _examineItemController.MainHighlight(false);
                    }
                    break;
                case SystemType.DoorController:
                    if (highlight)
                    {
                        doorController.Highlight(true);
                    }
                    else
                    {
                        doorController.Highlight(false);
                    }
                    break;
                case SystemType.FuseBoxSys:
                    if (highlight)
                    {
                        _fuseboxItemController.Highlight(true);
                    }
                    else
                    {
                        _fuseboxItemController.Highlight(false);
                    }
                    break;
                case SystemType.SafeSys:
                    if (highlight)
                    {
                        _safeItemController.Highlight(true);
                    }
                    else
                    {
                        _safeItemController.Highlight(false);
                    }
                    break;
                case SystemType.InteractionHighlight:
                    if (highlight)
                    {
                        interactionHighlight.Highlight(true);
                    }
                    else
                    {
                        interactionHighlight.Highlight(false);
                    }
                    break;
                case SystemType.NoteSys:
                    if (highlight)
                    {
                        _noteController.Highlight(true);
                    }
                    else
                    {
                        _noteController.Highlight(false);
                    }
                    break;
            }
        }

        public void InteractionType()
        {
            switch (_systemType)
            {
                case SystemType.ExamineSys: _examineItemController.ExamineObject(); break;
                case SystemType.NoteSys: _noteController.DisplayNotes(); break;
                case SystemType.KeypadSys: _keypadItemController.ShowKeypad(); break;
                case SystemType.ThemedKeySys: _themedKeyItemController.ObjectInteract(); break;
                case SystemType.PadlockSys: _padlockItemController.ObjectInteract(); break;
                case SystemType.SafeSys: _safeItemController.ShowSafeLock(); break;
                case SystemType.buttonDoorSys: _buttonDoorController.PlayAnimation(); break;
                case SystemType.FuseBoxSys: _fuseboxItemController.ObjectInteract(); break;
                case SystemType.InteractProduceText: textProduce.TextProducing(); break;
                case SystemType.DoorController:  doorController.Interaction(); break;
                case SystemType.InteractionHighlight: interactionHighlight.Interaction(); break;
            }
        }
    }
}
