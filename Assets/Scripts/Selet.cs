using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Selet : MonoBehaviour
{
    public GameObject creat;
    public TextMeshProUGUI[] slotText;
    public TextMeshProUGUI[] slotTextDate;
    public Text newPlayerName;

    public int currentSlotNumber = 0;


    public void SetCurrentSlotNumber(int number)
    {
        currentSlotNumber = number;
    }

    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        LoadingSceneController.Instance.LoadScene(1);
    }
}
