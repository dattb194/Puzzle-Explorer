using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class UIMng : MonoBehaviour
{
    public static UIMng inst;
    private void Awake()
    {
        inst = this;
    }

    public GameObject winDialog;
    public GameObject loseDialog;
    public GameplayDisplay gameplayDisplay;
    public MenuDisplay menuDisplay;
    public StageDisplay stageDisplay;
    public InventoryDisplay inventoryDisplay;

    public void ShowMenuGame()
    {
        menuDisplay.SetData();
    }

    public void OnStartGame()
    {
        menuDisplay.gameObject.SetActive(false);
        gameplayDisplay.SetData();
    }
    

    public void Win()
    {
        gameplayDisplay.gameObject.SetActive(false);
        winDialog.SetActive(true);
    }
    public void Lose()
    {
        gameplayDisplay.gameObject.SetActive(false);
        loseDialog.SetActive(true);
    }
}
