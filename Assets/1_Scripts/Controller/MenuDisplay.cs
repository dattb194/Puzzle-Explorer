using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDisplay : MonoBehaviour
{
    public void SetData()
    {
        gameObject.SetActive(true);
    }
    public void ForceStart()
    {
        GPMng.inst.StartGame();
        UIMng.inst.OnStartGame();
    }
    public void Shop()
    { 
    
    }
    public void Inventory()
    {
        UIMng.inst.inventoryDisplay.SetData();
    }
    public void Stage()
    {
        UIMng.inst.stageDisplay.SetData();
    }
    public void Settings()
    { 
    
    }
}
