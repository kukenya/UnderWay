using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace AdventurePuzzleKit
{
    public class AdventureKitRaycast : MonoBehaviour
    {
        UnderWay inputActions;

        [Header("Raycast Length/Layer")]
        [SerializeField] private int rayLength = 5;
        [SerializeField] private LayerMask layerMaskInteract;
        [SerializeField] private string exludeLayerName = null;
        private AKItemController raycasted_obj;

        [Header("UI / Crosshair")]
        [SerializeField] private Image crosshair = null;
        [HideInInspector] public bool doOnce;

        private bool isCrosshairActive;
        private const string pickupTag = "InteractiveObject";

        private void Awake()
        {
            inputActions = new UnderWay();
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        private void Update()
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            int mask = 1 << LayerMask.NameToLayer(exludeLayerName) | layerMaskInteract.value;

            if (Physics.Raycast(transform.position, fwd, out RaycastHit hit, rayLength, mask))
            {
                if (hit.collider.CompareTag(pickupTag))
                {
                    if (!doOnce)
                    {
                        raycasted_obj = hit.collider.gameObject.GetComponent<AKItemController>();
                        raycasted_obj.Highlight(true);
                        CrosshairChange(true);
                    }

                    isCrosshairActive = true;
                    doOnce = true;

                    if (inputActions.Player.LeftMouseClick.IsPressed())
                    {
                        raycasted_obj.InteractionType();
                    }
                }
            }

            else
            {
                if (isCrosshairActive)
                {
                    if(raycasted_obj != null)
                    {
                        raycasted_obj.Highlight(false);
                    }
                    CrosshairChange(false);
                    doOnce = false;
                }
            }
        }

        void CrosshairChange(bool on)
        {
            if (on && !doOnce)
            {
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color = Color.white;
                isCrosshairActive = false;
            }
        }
    }
}
