using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    UnderWay inputActions;
    Vector2 moveDir = Vector2.zero;
    Vector2 lookDir = Vector2.zero;
    private float xRotation = 0f;

    CharacterController characterController;
    public float speed = 5;
    public float runSpeed = 10;
    public float gravity = 5f;
    public Vector3 sumVector, xVector,  zVector;

    public float upDownRange = 90;
    public float mouseSensitivity = 10f;
    public bool mapActive = false;

    public GameObject map;
    public PlayerStatus playerStatus;

    public GameObject overlay;
    public Text overlayText;

    public bool canMove = true;

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
        characterController = GetComponent<CharacterController>();
        transform.position = DataManager.instance.nowPlayer.playerLastPos;
    }

    // Update is called once per frame
    void Update()
    {
        lookDir = inputActions.Player.Look.ReadValue<Vector2>();
        moveDir = inputActions.Player.Move.ReadValue<Vector2>();
        float mouseX = lookDir.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookDir.y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
        
        if (moveDir.x != 0 || moveDir.y != 0)
        {
            playerStatus.isMoving = true;
        }
        else
        {
            playerStatus.isMoving = false;
        }
        xVector = transform.forward * speed * Time.deltaTime * moveDir.y;
        zVector = transform.right * speed * Time.deltaTime * moveDir.x;
        sumVector = xVector + zVector;
        sumVector.y -= gravity * Time.deltaTime;

        if (canMove)
        {
            characterController.Move(sumVector);
        }
    }

    public void OnMove()
    {
        canMove = true;
    }

    public void OffMove()
    {
        canMove = false;
    }

    /*void CheckObject()
    {
        Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 2f);

        if (hit.collider != null)
        {
            overlay.SetActive(true);
            overlayText.text = "[E] " + hit.collider.GetComponent<ReatObjProduce>().name;
            if (Interaction.ReadValue<float>() != 0)
            {

            }
        }
        else
        {
            overlay.SetActive(false);
        }
        
    }

    void ToggleMap()
    {
        /*if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!mapActive)
            {
                map.SetActive(true);
                mapActive = true;
            }
            else
            {
                map.SetActive(false);
                mapActive = false;
            }
            
        }
    }*/
}
