using UnityEngine;
using AdventurePuzzleKit;
using ExamineSystem;

namespace FuseboxSystem
{
    public class FuseItemController : MonoBehaviour
    {
        [Space(10)] [SerializeField] private ObjectType _objectType = ObjectType.None;

        private enum ObjectType { None, Fusebox, Fuse }

        public bool showNameHighlight = true;
        public string fuseboxName = "전기회로";
        public string highlightText = "퓨즈넣기";

        public void ObjectInteract()
        {
            if (_objectType == ObjectType.Fusebox)
            {
                gameObject.GetComponent<FuseboxController>().CheckFuseBox();
            }

            else if (_objectType == ObjectType.Fuse)
            {
                DataManager.instance.nowPlayer.fuse += 1;
                AKAudioManager.instance.Play("FuseBoxPickup");
                gameObject.SetActive(false);
            }
        }

        public void Highlight(bool isHighlighted)
        {
            if (showNameHighlight && _objectType == ObjectType.Fusebox)
            {
                if (isHighlighted)
                {
                    ExamineUIController.instance.interactionItemNameUI.text = fuseboxName;
                    ExamineUIController.instance.interactionTextUI.text = highlightText;
                    ExamineUIController.instance.interactionNameMainUI.SetActive(true);
                }
                else
                {
                    ExamineUIController.instance.interactionItemNameUI.text = fuseboxName;
                    ExamineUIController.instance.interactionNameMainUI.SetActive(false);
                }
            }
        }
    }
}
