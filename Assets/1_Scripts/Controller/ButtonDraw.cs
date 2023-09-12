using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDraw : MonoBehaviour
{
    public Text txtQuantity;
    public Text txtTitle;
    public DrawStyle style;
    public void SetData(LineInfo info)
    {
        txtQuantity.text = info.quantity.ToString();
        txtTitle.text = info.style.ToString();
        style = info.style;
        GetComponent<Button>().onClick.AddListener(()=>
        {
            DrawingPhysics.inst.SetStateDraw((int)style);
        });
    }

    public void Sellecting()
    {
        GetComponent<Image>().color = Color.red;    
    }
    public void UnSellect()
    {
        GetComponent<Image>().color = Color.white;
    }
    private void Update()
    {
        if (LevelMng.inst.Enegy > 0 && LevelMng.inst.lineInfos.FirstOrDefault(x => x.style == style).quantity > 0)
            GetComponent<Button>().interactable = true;
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
