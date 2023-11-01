using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class UIManager : MonoBehaviour
{
    public CurrentUI currentUI = CurrentUI.Food;
    public enum CurrentUI
    {
        Map, Food, Backpack, Tutorial
    }
    public Color resetColor = new Color(89, 89, 89);
    public GameObject tutorial;
    public GameObject tutorialDot;

    public GameObject food;
    public GameObject foodDot;

    public Transform containerUI;
    public Transform slotHolder;
    public GameObject escUI;
    public GameObject tutorialUI;

    public GameObject tutorialPage1;
    public GameObject tutorialPage2;

    public GameObject quickSlotUI;
    public GameObject statusUI;
    public GameObject storyTextUI;

    public GameObject slotOptionsUI;
    public Button itemUseButton;
    public Button itemRemoveButton;

    public GameObject questUI;
    public GameObject questText;

    public GameObject tutorialTextUI;

    private int drugKey = 0;
    private int cafeKey = 0;
    private int storeKey = 0;
    private int underStoreKey = 0;

    UnderWay inputActions;

    public static UIManager instance;

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        inputActions.Player.Inventory.performed += Inventory_performed;
        inputActions.Player.LeftMouseClick.performed += LeftMouseClick_performed;
        escUI.SetActive(false);
        SelectUI();
    }
    private void LeftMouseClick_performed(InputAction.CallbackContext obj)
    {
        if (currentUI == CurrentUI.Tutorial)
        {
            if (tutorialPage2.activeSelf)
            {
                tutorialPage2.SetActive(false);
            }
            else
            {
                tutorialPage2.SetActive(true);
            }
        }
    }

    private void Inventory_performed(InputAction.CallbackContext obj)
    {
        if (DataManager.instance.nowPlayer.hasBackpack)
        {
            OnUI();
        }
    }

    private void Awake()
    {
        inputActions = new UnderWay();
        if (instance == null) { instance = this; }
        slotOptionsUI.SetActive(false);
    }

    private void Update()
    {
        SetQuestUIText();
    }

    public void SetCurrentUI(int index)
    {
        switch (index)
        {
            case 1:
                currentUI = CurrentUI.Map;
                break;
            case 2:
                currentUI = CurrentUI.Food;
                break;
            case 3:
                currentUI = CurrentUI.Backpack;
                break;
            case 4:
                currentUI = CurrentUI.Tutorial;
                break;
        }
    }

    public void OnUI()
    {
        if (escUI.activeSelf)
        {
            StoryGameManager.instance.OffPlayerStop();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            escUI.SetActive(false);
        }
        else
        {
            StoryGameManager.instance.OnPlayerStop();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            escUI.SetActive(true);
        }
    }

    public void SelectUI()
    {
        switch (currentUI)
        {
            case CurrentUI.Map:
                break;
            case CurrentUI.Food:
                ResetUIColor();
                tutorialUI.SetActive(false);
                food.GetComponent<Image>().color = Color.white;
                foodDot.GetComponent<Image>().color = Color.white;
                ToggleFoodUI();
                break;
            case CurrentUI.Backpack:
                break;
            case CurrentUI.Tutorial:
                ResetUIColor();
                containerUI.gameObject.SetActive(false);
                slotOptionsUI.SetActive(false);
                tutorialUI.SetActive(true);
                tutorial.GetComponent<Image>().color = Color.white;
                tutorialDot.GetComponent<Image>().color = Color.white;
                break;
        }
    }

    public void SetQuestUIText()
    {
        if (DataManager.instance.nowPlayer.drugKey) drugKey = 1;
        else drugKey = 0;

        if (DataManager.instance.nowPlayer.storeKey) storeKey = 1;
        else storeKey = 0;

        if (DataManager.instance.nowPlayer.underStoreKey) underStoreKey = 1;
        else underStoreKey = 0;

        if (DataManager.instance.nowPlayer.cafeKey) cafeKey = 1;
        else cafeKey = 0;

        questText.GetComponent<Text>().text = "æ‡±π ø≠ºË " + drugKey + "/1\nƒ´∆‰ ø≠ºË " + cafeKey + "/1\n∆Ì¿«¡° ø≠ºË " 
            + storeKey + "/1\n¡ˆ«œªÛ∞° ø≠ºË " + underStoreKey + "/1\n\n«ª¡Ó " + DataManager.instance.nowPlayer.fuse + "/2"; 

    }

    public void ResetUIColor()
    {
        tutorial.gameObject.GetComponent<Image>().color = resetColor;
        tutorialDot.gameObject.GetComponent<Image>().color = resetColor;
        food.gameObject.GetComponent<Image>().color = resetColor;
        foodDot.gameObject.GetComponent<Image>().color = resetColor;
    }

    private void ToggleFoodUI()
    {
        UIManager.instance.slotOptionsUI.SetActive(false);
        containerUI.gameObject.SetActive(true);
    }
}
