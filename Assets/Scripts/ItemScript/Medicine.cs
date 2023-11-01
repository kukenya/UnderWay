using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    public float medicineHaveNoPenaltyPlusMentality = 10f;
    public float medicineHavePenaltyPlusMentality = 20f;

    public MedicineType medicineType = MedicineType.None;
    public enum MedicineType { None, MedicineHaveNoPenalty, MedicineHavePenalty}

    public void EatMedicine()
    {
        switch (medicineType)
        {
            case MedicineType.MedicineHaveNoPenalty:
                DataManager.instance.nowPlayer.mentality += medicineHaveNoPenaltyPlusMentality;
                Destroy(this.gameObject);
                break;
            case MedicineType.MedicineHavePenalty:
                if (DataManager.instance.nowPlayer.eatPenaltyMedicine > 3
                    && Random.Range(0, 5) == 2)
                {
                    //플레이어 사망
                }
                else
                {
                    DataManager.instance.nowPlayer.mentality += medicineHavePenaltyPlusMentality;
                    Destroy(this.gameObject);
                }
                break;
        }
    }
}
