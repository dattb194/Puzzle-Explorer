using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class HorizontalGroupExt : UIFixed
{
    public float ratioSizeY;
    public float ratioChildSizeX;
    [SerializeField] Vector2 childSize;

    
    public override void Fix()
    {
        //mRect.sizeDelta = new Vector2(mRect.sizeDelta.x, ratioSizeY * Screen.height);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(ratioChildSizeX * screenSize.x,
                transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y);
        }
    }
}
