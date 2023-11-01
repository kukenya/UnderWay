using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Text hungryText;
    public Text mentalityText;
    public Text thirstText;

    public Image hungryLoadingBar;
    public Image mentalityLoadingBar;
    public Image thirstLoadingBar;

    bool hungryDown = true;
    bool mentalityDown = true;

    public bool isMoving = false;

    public float downHungryPerSec = -0.1f;
    public float downMentalityPerSec = -0.1f;
    public float downThirstPerSec = -0.1f;

    // Update is called once per frame
    void Update()
    {
        if(DataManager.instance.nowPlayer.hungry <= 80)
        {
            //downHungryPerSec = -2f;
        }
        else
        {
            //downHungryPerSec = -1f;
        }
        DownStatusPerTime();
        SetStatusTextAndBar();
    }

    public void UpDownHungry(float hungry)
    {
        DataManager.instance.nowPlayer.hungry = Mathf.Clamp(DataManager.instance.nowPlayer.hungry + hungry, 0, 100);
    }

    public void UpDownMentality(float mentality)
    {
        DataManager.instance.nowPlayer.mentality = Mathf.Clamp(DataManager.instance.nowPlayer.mentality + mentality, 0, 100);
    }

    public void UpDownThirst(float energy)
    {
        DataManager.instance.nowPlayer.thirst = Mathf.Clamp(DataManager.instance.nowPlayer.thirst + energy, 0, 100);
    }

    private void DownStatusPerTime()
    {
        if (hungryDown)
        {
            UpDownHungry(downHungryPerSec * Time.deltaTime);
        }
        if (mentalityDown)
        {
            UpDownMentality(downMentalityPerSec * Time.deltaTime);
        }
        if (isMoving)
        {
            UpDownThirst(downThirstPerSec * Time.deltaTime);
        }
    }

    public void SetStatusTextAndBar()
    {
        hungryText.text = ((int)DataManager.instance.nowPlayer.hungry).ToString() + "%";
        hungryLoadingBar.fillAmount = DataManager.instance.nowPlayer.hungry / 100;

        mentalityText.text = ((int)DataManager.instance.nowPlayer.mentality).ToString() + "%";
        mentalityLoadingBar.fillAmount = DataManager.instance.nowPlayer.mentality / 100;

        thirstText.text = ((int)DataManager.instance.nowPlayer.thirst).ToString() + "%";
        thirstLoadingBar.fillAmount = DataManager.instance.nowPlayer.thirst / 100;
    }
}
