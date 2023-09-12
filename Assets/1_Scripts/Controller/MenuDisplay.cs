using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDisplay : MonoBehaviour
{
    public Text txtGold;
    public Text txtDiamond;
    public void SetData(int goldQuantity, int diamondQuantity)
    {
        txtGold.text = goldQuantity.ToString();
        txtDiamond.text = diamondQuantity.ToString();
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
    
    }
    public void Stage()
    { 
    
    }
    public void Settings()
    { 
    
    }
}
