using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDraw : MonoBehaviour
{
    public DrawStyle style;

    Text txtQuantity;
    Image imgChild;

    Button btn;

    public void SetData(LineInfo info)
    {
        if (info.quantity <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        btn = GetComponent<Button>();
        txtQuantity = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        txtQuantity.text = info.quantity.ToString();

        btn.onClick.AddListener(() =>
        {
            DrawingPhysics.inst.SetStateDraw((int)style);
        });
        imgChild = transform.GetChild(0).GetComponent<Image>();
    }

    public void Sellecting()
    {
        imgChild.color = Color.red;
        btn.interactable = false;
    }
    public void UnSellect()
    {
        if (!gameObject.activeInHierarchy) return;
        imgChild.color = Color.white;
        btn.interactable = false;
        imgChild.color = new Color(imgChild.color.r, imgChild.color.g, imgChild.color.b, .5f);
    }
    public void EndDraw()
    {
        if (!gameObject.activeInHierarchy) return;
        if (LevelMng.inst.Enegy <= 0 || LevelMng.inst.lineInfos.FirstOrDefault(x => x.style == style).quantity < 1)
        {
            gameObject.SetActive(false);
            return;
        }
        btn.interactable = true;
        txtQuantity.text = LevelMng.inst.lineInfos.FirstOrDefault(x => x.style == style).quantity.ToString();
        if (imgChild.color.a != 1)
            imgChild.color = new Color(imgChild.color.r, imgChild.color.g, imgChild.color.b, 1);
        if (imgChild.color != Color.white)
            imgChild.color = Color.white;
    }
}
