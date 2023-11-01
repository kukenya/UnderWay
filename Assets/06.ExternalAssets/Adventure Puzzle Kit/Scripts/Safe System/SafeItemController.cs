using UnityEngine;
using ExamineSystem;

namespace SafeUnlockSystem
{
    public class SafeItemController : MonoBehaviour
    {
        [SerializeField] private SafeController _safeController = null;

        public bool showNameHighlight = true;
        public string safeName = "금고";
        public string highlightText = "잠금헤제";

        public void ShowSafeLock()
        {
            _safeController.ShowSafeLock();
        }

        public void Highlight(bool isHighlighted)
        {
            if (showNameHighlight)
            {
                if (isHighlighted)
                {
                    ExamineUIController.instance.interactionItemNameUI.text = safeName;
                    ExamineUIController.instance.interactionTextUI.text = highlightText;
                    ExamineUIController.instance.interactionNameMainUI.SetActive(true);
                }
                else
                {
                    ExamineUIController.instance.interactionItemNameUI.text = safeName;
                    ExamineUIController.instance.interactionNameMainUI.SetActive(false);
                }
            }
        }
    }
}
