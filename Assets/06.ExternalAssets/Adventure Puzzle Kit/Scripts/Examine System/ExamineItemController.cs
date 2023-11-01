using UnityEngine;
using FlashlightSystem;
using ThemedKeySystem;
using FuseboxSystem;
using AdventurePuzzleKit;
using UnityEngine.InputSystem;

namespace ExamineSystem
{
    public class ExamineItemController : MonoBehaviour
    {
        UnderWay inputActions;

        [Header("Item Name")]
        public string itemName;

        [Header("Can you collect it?")]
        [SerializeField] private bool isCollectable = false;
        [SerializeField] private SystemType _systemType = SystemType.None;
        [SerializeField] private SecondarySystemType secondarySystemType = SecondarySystemType.None;
        private enum SystemType { None, FlashlightSys, GeneratorSys, GasMaskSys, ThemedKeySys, ChessSys, FuseBoxSys, 
            BackpackSys, InventoryItemSys, MedicineSys, EatItemSys, DrinkItemSys}

        private enum SecondarySystemType { None, ThemedKeySys, FuseBoxSys}

        [Header("Item Name Settings")]
        [SerializeField] private int textSize = 40;
        [SerializeField] private Font fontType = null;
        [SerializeField] private FontStyle fontStyle = FontStyle.Normal;
        [SerializeField] private Color fontColor = Color.white;

        [Header("Initial Rotation for objects")]
        [SerializeField] private Vector3 initialRotationOffset = new Vector3(0, 0, 0);

        [Header("Zoom Settings")]
        [SerializeField] private float initialZoom = 1f;
        [SerializeField] private Vector2 zoomRange = new Vector2(0.5f, 2f);
        [SerializeField] private float zoomSensitivity = 0.1f;

        [Header("Examine Rotation")]
        [SerializeField] private float horizontalSpeed = 5.0F;
        [SerializeField] private float verticalSpeed = 5.0F;

        [Header("Emissive Highlight")]
        [SerializeField] private bool showEmissionHighlight = false;
        [SerializeField] private bool showNameHighlight = false;

        [Header("Item UI Type")]
        [SerializeField] private UIType _UIType = UIType.None;

        [Header("IspectPoints - ONLY add the Inspect points that you want to appear when first examining")]
        [SerializeField] private GameObject[] inspectPoints = null;
        private LayerMask myMask;
        private bool hasInspectPoints = false;
        private float viewDistance = 25;

        [Header("Item Interaction Sound")]
        [SerializeField] private string pickupSound = "YourSound";

        private Material thisMat;
        Vector3 originalPosition;
        Quaternion originalRotation;
        //private Vector3 startPos;
        private bool canRotate;
        private float currentZoom = 1;
        private const string emissive = "_EMISSION";
        private const string interact = "Interact";
        private const string examineLayer = "ExamineLayer";
        private const string defaultLayer = "Default";

        private FlashlightItemController _flashlightItemController;
        private ThemedKeyItemController _themedKeyItemController;
        private FuseItemController _fuseboxItemController;
        private Medicine medicine;
        private BackPack backpack;
        private EatItem eatItem;
        private DrinkItem drinkItem;
        private InventoryItem inventoryItem;

        private Camera mainCamera;
        private Transform examinePoint;

        private AdventureKitRaycast raycastManager;

        public enum UIType { None, BasicLowerUI, RightSideUI }

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

        void Start()
        {
            myMask = 1 << LayerMask.NameToLayer("InspectPointMask");

            initialZoom = Mathf.Clamp(initialZoom, zoomRange.x, zoomRange.y);
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            //startPos = gameObject.transform.localEulerAngles;

            thisMat = GetComponent<Renderer>().material;
            thisMat.DisableKeyword(emissive);

            if (isCollectable)
            {
                SetType();
                print("1");
            }

            mainCamera = Camera.main;
            raycastManager = mainCamera.GetComponent<AdventureKitRaycast>();
            examinePoint = GameObject.FindWithTag("ExaminePoint").GetComponent<Transform>();
            inputActions.Player.RightMouseClick.performed += RightMouseClick_performed;
            inputActions.Player.Interaction.performed += Interaction_performed;
        }

        private void Interaction_performed(InputAction.CallbackContext obj)
        {
            if (canRotate)
            {
                if (isCollectable)
                {
                    CollectItem();
                }
            }
        }

        private void RightMouseClick_performed(InputAction.CallbackContext obj)
        {
            if (canRotate)
            {
                StopInteractingObject();
                raycastManager.doOnce = false;
            }
        }

        public void MainHighlight(bool isHighlighted)
        {
            if (showNameHighlight)
            {
                if (isHighlighted)
                {
                    ExamineUIController.instance.interactionItemNameUI.text = itemName;
                    ExamineUIController.instance.interactionTextUI.text = "조사하기";
                    ExamineUIController.instance.interactionNameMainUI.SetActive(true);
                }
                else
                {
                    ExamineUIController.instance.interactionItemNameUI.text = itemName;
                    ExamineUIController.instance.interactionNameMainUI.SetActive(false);
                }
            }

            if (showEmissionHighlight)
            {
                if (isHighlighted)
                { 
                    thisMat.EnableKeyword(emissive);
                }
                else
                {
                    thisMat.DisableKeyword(emissive);
                }
            }
        }

        private void MoveZoom(float value, bool moveSelf = true)
        {
            examinePoint.transform.localPosition = new Vector3(0, 0, value);

            if(moveSelf)
            {
                transform.position = examinePoint.transform.position;
            }
        }

        public void StopInteractingObject()
        {
            StoryGameManager.instance.OffPlayerLight();
            UIManager.instance.quickSlotUI.SetActive(true);

            gameObject.layer = LayerMask.NameToLayer(interact);
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            ExamineUIController.instance.interestPointParentUI.SetActive(false);
            AKDisableManager.instance.DisablePlayerExamine(false);

            canRotate = false;
            hasInspectPoints = false;

            switch (_UIType)
            {
                case UIType.None:
                    ExamineUIController.instance.noUICloseButton.SetActive(false);
                    break;
                case UIType.BasicLowerUI:
                    ExamineUIController.instance.basicItemNameUI.text = null;
                    ExamineUIController.instance.basicExamineUI.SetActive(false);
                    break;
                case UIType.RightSideUI:
                    ExamineUIController.instance.rightItemNameUI.text = null;
                    ExamineUIController.instance.rightExamineUI.SetActive(false);
                    break;
            }
        }

        public void ExamineObject()
        {
            UIManager.instance.quickSlotUI.SetActive(false);
            ExamineUIController.instance.examineController = gameObject.GetComponent<ExamineItemController>();
            AKAudioManager.instance.Play(pickupSound);

            if (inspectPoints.Length >= 1)
            {
                hasInspectPoints = true;

                foreach (GameObject pointToEnable in inspectPoints)
                {
                    pointToEnable.SetActive(true);
                }
            }

            currentZoom = initialZoom; MoveZoom(initialZoom);

            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
            transform.Rotate(initialRotationOffset);

            AKDisableManager.instance.DisablePlayerExamine(true);
            ExamineUIController.instance.interactionNameMainUI.SetActive(false);
            gameObject.layer = LayerMask.NameToLayer(examineLayer);
            thisMat.DisableKeyword(emissive);

            canRotate = true;

            switch (_UIType)
            {
                case UIType.None:
                    ExamineUIController.instance.noUICloseButton.SetActive(true);
                    break;
                case UIType.BasicLowerUI:
                    ExamineUIController.instance.basicItemNameUI.text = itemName;
                    TextCustomisation();
                    ExamineUIController.instance.basicExamineUI.SetActive(true);
                    break;
                case UIType.RightSideUI:
                    ExamineUIController.instance.rightItemNameUI.text = itemName;
                    TextCustomisation();
                    ExamineUIController.instance.rightExamineUI.SetActive(true);
                    break;
            }
        }

        private void TextCustomisation()
        {
            switch (_UIType)
            {
                case UIType.BasicLowerUI:
                    ExamineUIController.instance.basicItemNameUI.fontSize = textSize;
                    ExamineUIController.instance.basicItemNameUI.fontStyle = fontStyle;
                    ExamineUIController.instance.basicItemNameUI.font = fontType;
                    ExamineUIController.instance.basicItemNameUI.color = fontColor;
                    break;
                case UIType.RightSideUI:
                    ExamineUIController.instance.rightItemNameUI.fontSize = textSize;
                    ExamineUIController.instance.rightItemNameUI.fontStyle = fontStyle;
                    ExamineUIController.instance.rightItemNameUI.font = fontType;
                    ExamineUIController.instance.rightItemNameUI.color = fontColor;
                    break;
            }         
        }

        void Update()
        {
            if (canRotate)
            {
                float h = horizontalSpeed * inputActions.Player.Look.ReadValue<Vector2>().x;
                float v = verticalSpeed * inputActions.Player.Look.ReadValue<Vector2>().y;

                if (hasInspectPoints)
                {
                    FindInspectPoints();
                }

                if (inputActions.Player.LeftMouseClick.IsPressed())
                {
                    gameObject.transform.Rotate(v, h, 0);
                }


                //Handle zooming
                bool zoomAdjusted = false;
                float scrollDelta = inputActions.UI.ScrollWheel.ReadValue<Vector2>().y;
                if (scrollDelta < 0)
                {
                    currentZoom += zoomSensitivity;
                    zoomAdjusted = true;
                }
                else if (scrollDelta > 0)
                {
                    currentZoom -= zoomSensitivity;
                    zoomAdjusted = true;
                }

                if(zoomAdjusted)
                {
                    currentZoom = Mathf.Clamp(currentZoom, zoomRange.x, zoomRange.y);
                    MoveZoom(currentZoom);
                }
            }
        }

        void FindInspectPoints()
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, viewDistance, myMask))
            {
                if (hit.transform.CompareTag("InspectPoint"))
                {
                    InspectPointUI(hit.transform.gameObject, mainCamera, true); //Enable inspect point UI
                    if (inputActions.Player.LeftMouseClick.IsPressed())
                    {
                        hit.transform.gameObject.GetComponent<ExamineInspectPoint>().InspectPointInteract();
                        StoryGameManager.instance.OnPlayerLight();
                    }
                }
                else
                {
                    InspectPointUI(null, null, false); //Disable inspect point UI
                }
            }
            else
            {
                InspectPointUI(null, null, false); //Disable inspect point UI
            }
        }

        void InspectPointUI(GameObject item, Camera camera, bool detected) // Enable/disable inspect point UI
        {
            if (detected)
            {
                ExamineUIController.instance.interestPointParentUI.SetActive(true);
                ExamineUIController.instance.interestPointParentUI.transform.position = camera.WorldToScreenPoint(item.transform.position);
                ExamineUIController.instance.interestPointText.text = item.GetComponent<ExamineInspectPoint>().InspectInformation();
            }
            else
            {
                ExamineUIController.instance.interestPointParentUI.SetActive(false); //Disable inspect UI
            }
        }

        void SetType()
        {
            switch (_systemType)
            {
                case SystemType.FlashlightSys: _flashlightItemController = GetComponent<FlashlightItemController>(); break;
                case SystemType.ThemedKeySys: _themedKeyItemController = GetComponent<ThemedKeyItemController>(); break;
                case SystemType.FuseBoxSys: _fuseboxItemController = GetComponent<FuseItemController>(); break;
                case SystemType.MedicineSys: medicine = GetComponent<Medicine>(); break;
                case SystemType.BackpackSys: backpack = GetComponent<BackPack>(); break;
                case SystemType.EatItemSys: eatItem = GetComponent<EatItem>(); break;
                case SystemType.DrinkItemSys: drinkItem = GetComponent<DrinkItem>(); break;
                case SystemType.InventoryItemSys: inventoryItem = GetComponent<InventoryItem>(); break;
            }

            switch (secondarySystemType)
            {
                case SecondarySystemType.None: break;
                case SecondarySystemType.ThemedKeySys: _themedKeyItemController = GetComponent<ThemedKeyItemController>(); break;
                case SecondarySystemType.FuseBoxSys: _fuseboxItemController = GetComponent<FuseItemController>(); break;
            }
        }

        void CollectItem()
        {
            print("2");
            switch (_systemType)
            {
                case SystemType.FlashlightSys: _flashlightItemController.ObjectInteract(); break;
                case SystemType.ThemedKeySys: _themedKeyItemController.ObjectInteract(); break;
                case SystemType.FuseBoxSys: _fuseboxItemController.ObjectInteract(); break;
                case SystemType.MedicineSys: medicine.EatMedicine(); break;
                case SystemType.BackpackSys: backpack.ObjectInteract(); break;
                case SystemType.EatItemSys: eatItem.Eat();  break;
                case SystemType.DrinkItemSys: drinkItem.Drink(); break;
                case SystemType.InventoryItemSys:
                    InventoryManager.instance.itemContainer.AddItem(inventoryItem.item);
                    DataManager.instance.nowPlayer.hasFlashLIght = true;
                    Destroy(inventoryItem.gameObject);
                    break;
            }

            switch (secondarySystemType)
            {
                case SecondarySystemType.None: break;
                case SecondarySystemType.ThemedKeySys: _themedKeyItemController.ObjectInteract(); break;
                case SecondarySystemType.FuseBoxSys: _fuseboxItemController.ObjectInteract(); break;
            }
            StopInteractingObject();
        }

        private void OnDestroy()
        {
            Destroy(thisMat);
        }
    }
}