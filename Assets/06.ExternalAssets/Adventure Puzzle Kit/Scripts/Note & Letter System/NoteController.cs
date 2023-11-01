using UnityEngine;
using ExamineSystem;

namespace NoteSystem
{
    public class NoteController : MonoBehaviour
    {
        [Header("Item UI Type")]
        [SerializeField] private UIType _NoteType = UIType.None;

        private enum UIType { None, BasicNote, BasicReverseNote, NormalCustomNote, ReverseCustomNote }

        private BasicNoteController basicNoteController;
        private BasicReverseNoteController basicReverseNoteController;
        private NormalCustomNoteController normalCustomNoteController;
        private ReverseCustomNoteController reverseCustomController;

        public bool showNameHighlight = true;
        public string noteName = "없음";
        public string highlightText = "열어보기";

        public void DisplayNotes()
        {
            switch (_NoteType)
            {
                case UIType.BasicNote:
                    BasicNoteController basicNoteController = GetComponent<BasicNoteController>();
                    if (basicNoteController.isReadable)
                    {
                        basicNoteController.enabled = true;
                        basicNoteController.ShowNote();
                    }
                    break;
                case UIType.BasicReverseNote:
                    BasicReverseNoteController basicReverseNoteController = GetComponent<BasicReverseNoteController>();
                    if (basicReverseNoteController.isReadable)
                    {
                        basicReverseNoteController.enabled = true;
                        basicReverseNoteController.ShowNote();
                    }
                    break;
                case UIType.NormalCustomNote:
                    NormalCustomNoteController normalCustomNoteController = GetComponent<NormalCustomNoteController>();
                    if (normalCustomNoteController.isReadable)
                    {
                        normalCustomNoteController.enabled = true;
                        normalCustomNoteController.ShowNote();
                    }
                    break;
                case UIType.ReverseCustomNote:
                    ReverseCustomNoteController reverseCustomController = GetComponent<ReverseCustomNoteController>();
                    if (reverseCustomController.isReadable)
                    {
                        reverseCustomController.enabled = true;
                        reverseCustomController.ShowNote();
                    }
                    break;   
            }
        }

        public void Highlight(bool isHighlighted)
        {
            if (showNameHighlight)
            {
                if (isHighlighted)
                {
                    ExamineUIController.instance.interactionItemNameUI.text = noteName;
                    ExamineUIController.instance.interactionTextUI.text = highlightText;
                    ExamineUIController.instance.interactionNameMainUI.SetActive(true);
                }
                else
                {
                    ExamineUIController.instance.interactionItemNameUI.text = noteName;
                    ExamineUIController.instance.interactionNameMainUI.SetActive(false);
                }
            }
        }
    }
}
