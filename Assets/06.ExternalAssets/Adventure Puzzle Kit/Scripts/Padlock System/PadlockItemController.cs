﻿using UnityEngine;

namespace PadlockSystem
{
    public class PadlockItemController : MonoBehaviour
    {
        [SerializeField] private PadlockController _padlockController = null;

        public void ObjectInteract()
        {
            _padlockController.ShowPadlock();
        }
    }
}
