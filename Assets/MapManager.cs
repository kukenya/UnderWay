using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public Transform playerPos;
    public RectTransform playerPosImage;

    private void Update()
    {
        //playerPosImage.transform.position.x = playerPos.position.x * 8.510706147832823f;
        playerPosImage.anchoredPosition = new Vector3(playerPos.position.x * 8.510706147832823f - 503.4372759703127f, playerPos.position.z * 8.510706147832823f - 1293.452835521763f, -9);
        print(playerPos.position.x);
    }
}
