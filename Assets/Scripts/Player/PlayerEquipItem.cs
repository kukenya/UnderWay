using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipItem : MonoBehaviour
{
    private PlayerEquip playerEquipType = PlayerEquip.Lighter;
    public StormLighter stormLighter;
    public FlashLight flashLIght;

    private enum PlayerEquip
    {
        Lighter, FlashLight
    }

    public bool EquipLighter()
    {
        switch (playerEquipType)
        {
            case PlayerEquip.Lighter:
                return true;
            case PlayerEquip.FlashLight:
                playerEquipType = PlayerEquip.Lighter;
                stormLighter.isActive = true;
                stormLighter.gameObject.SetActive(true);
                flashLIght.isActive = false;
                flashLIght.gameObject.SetActive(false);
                return false;
            default:
                return false;
        }
    }

    public bool EquipFlashLight()
    {
        switch (playerEquipType)
        {
            case PlayerEquip.Lighter:
                playerEquipType = PlayerEquip.FlashLight;
                stormLighter.isActive = false;
                stormLighter.gameObject.SetActive(false);
                flashLIght.isActive = true;
                flashLIght.gameObject.SetActive(true);
                return false;
            case PlayerEquip.FlashLight:
                return true;
            default:
                return false;
        }
        
    }
}
