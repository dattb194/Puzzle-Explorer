using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class GridLayoutGroupFix : UIFixed
{
    public GridLayoutGroup glg;
    public GLGStruct glgStruct;
    public override void Fix()
    {
        glg.cellSize = new Vector2(glgStruct.cellSize.x * screenSize.x, glgStruct.cellSize.y * screenSize.y);
        glg.spacing = new Vector2(glgStruct.spacing.x * screenSize.x, glgStruct.spacing.y * screenSize.y);
    }
}
[System.Serializable]
public struct GLGStruct
{
    public Vector2 cellSize;
    public Vector2 spacing;
}