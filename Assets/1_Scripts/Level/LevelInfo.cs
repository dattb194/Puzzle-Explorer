using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelInfo
{
    public int ID;
    public int MaxEnegy;
    public CameraConfig cameraConfig;
    public List<LineInfo> lineInfos;
    [HideInInspector] public List<ItemInfo> items;
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
    public void LoadJson(string json)
    {
        items = new List<ItemInfo>();
        var _base = JsonUtility.FromJson<LevelInfo>(json);
        foreach (var item in _base.items)
        {
            items.Add(item);
        }
    }
}
[System.Serializable]
public class LevelJson
{
    public int ID;
    public string Json;
}
[System.Serializable]
public class ItemInfo
{
    public TypeItem type;
    public Vector3 pos;
}
[System.Serializable]
public class LineInfo
{
    public DrawStyle style;
    public int quantity;
}
[System.Serializable]
public class CameraConfig
{
    public bool followPlayer = false;
}
