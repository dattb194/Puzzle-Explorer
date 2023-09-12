using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayDisplay : MonoBehaviour
{
    public List<ButtonDraw> buttonDraws;
    public Slider enegyDisplay;
    public void SetData()
    {
        gameObject.SetActive(true);
        SetbtnsDraw();
    }
    private void Update()
    {
        enegyDisplay.value = (float)LevelMng.inst.Enegy / LevelMng.inst.MaxEnegy;
    }
    public void SetbtnsDraw()
    {
        for (int i = 0; i < LevelMng.inst.lineInfos.Count; i++)
        {
            for (int j = 0; j < buttonDraws.Count; j++)
            {
                if(buttonDraws[j].style == LevelMng.inst.lineInfos[i].style)
                    buttonDraws[j].SetData(LevelMng.inst.lineInfos[i]);
            }
            //buttonDraws[i].SetData(LevelMng.inst.lineInfos[i]);
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
            item.EndDraw();
        }
    }
    public void Replay()
    {
        GPMng.inst.ReloadScene();
    }
}
