using AdventurePuzzleKit;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PadlockSystem
{
    public class SpinnerScript : MonoBehaviour, IPointerDownHandler
    {
        [Header("Padlock Controller Reference")]
        [SerializeField] private PadlockController padlockController = null;

        private int spinnerNumber;
        private int spinnerLimit;

        [Header("Padlock Row")]
        [SerializeField] private PadlockRow _row = 0;

        private enum PadlockRow { row1, row2, row3, row4 }

        private void Awake()
        {
            spinnerNumber = 1;
            spinnerLimit = 9;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.Rotate(0, 0, transform.rotation.z + 40);
            padlockController.SpinSound();
            Rotate();
        }

        void Rotate()
        {
            if (spinnerNumber <= spinnerLimit - 1)
            {
                spinnerNumber++;
            }

            else
            {
                spinnerNumber = 1;
            }

            switch (_row)
            {
                case PadlockRow.row1:
                    padlockController.combinationRow1 = spinnerNumber;
                    padlockController.CheckCombination();
                    break;
                case PadlockRow.row2:
                    padlockController.combinationRow2 = spinnerNumber;
                    padlockController.CheckCombination();
                    break;
                case PadlockRow.row3:
                    padlockController.combinationRow3 = spinnerNumber;
                    padlockController.CheckCombination();
                    break;
                case PadlockRow.row4:
                    padlockController.combinationRow4 = spinnerNumber;
                    padlockController.CheckCombination();
                    break;
            }
        }

        
    }
}


