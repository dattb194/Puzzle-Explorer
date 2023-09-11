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

    public Image enegyDisplay;
    public GameObject winDialog;
    public GameObject loseDialog;

    public List<ButtonDraw> buttonDraws;

    private void Update()
    {
        if (GPMng.inst.IsPlaying)
        {
            enegyDisplay.fillAmount = (float)LevelMng.inst.Enegy / LevelMng.inst.MaxEnegy;
        }
    }

    public void SetbtnsDraw()
    {
        for (int i = 0; i < LevelMng.inst.lineInfos.Count; i++)
        {
            buttonDraws[i].SetData(LevelMng.inst.lineInfos[i]);
        }
    }
    public void SellectDraw()
    {
        foreach (var item in buttonDraws)
        {
            if (item.style == DrawingPhysics.inst.StyleDraw)
                item.Sellecting();
            else
                item.UnSellect();
        }
    }

    public void OnEndDrawing()
    {
        foreach (var item in buttonDraws)
        {
            item.UnSellect();
        }
        SetbtnsDraw();
    }

    public void Win()
    {
        winDialog.SetActive(true);
    }
    public void Lose()
    {
        loseDialog.SetActive(true);
    }
}
