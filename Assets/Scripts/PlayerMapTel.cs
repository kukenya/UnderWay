using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapTel : MonoBehaviour
{
    public Vector3 telPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.GetComponent<CharacterController>().enabled = false;
            other.transform.position = telPos;
            other.transform.GetComponent<CharacterController>().enabled = true;
        }
    }
}
