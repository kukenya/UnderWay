using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PadlockSystem;

public class PadlockRay : MonoBehaviour
{
    public LayerMask layerMask;
    UnderWay inputActions;
    InputAction mouse;
    private void Awake()
    {
        inputActions = new UnderWay();
        mouse = inputActions.UI.Point;
    }

    public void MyMousClick(InputAction.CallbackContext context)
    {
        /*if (context.action.phase == InputActionPhase.Performed)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 Worldpos = Camera.main.ScreenToWorldPoint(mousePos);
            RaycastHit hit;
            Physics.Raycast(transform.position, Worldpos, out hit);
            Debug.DrawRay(transform.position, Worldpos, Color.blue, 10f);
            if (hit.collider != null)
            {
                
                print(hit.collider.name);
                hit.transform.GetComponent<SpinnerScript>().MouseClick();
            }
        }*/
    }

    private void Update()
    {
       
    }
}
