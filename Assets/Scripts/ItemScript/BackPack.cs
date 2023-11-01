using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPack : MonoBehaviour
{
    

    public void ObjectInteract()
    {
        DataManager.instance.nowPlayer.hasBackpack = true;
        Destroy(this.gameObject);
    }
}
