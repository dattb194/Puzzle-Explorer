using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Base/Levels", fileName = "Levels")]
public class LevelsBase : ScriptableObject
{
    public List<LevelJson> levels;
    public void UpdateLevel(LevelInfo levelInfo)
    {
        if (levels.Exists(x => x.ID == levelInfo.ID))
            levels.Remove(levels.FirstOrDefault(x => x.ID == levelInfo.ID));

        LevelJson lv = new LevelJson();
        lv.ID = levelInfo.ID;
        lv.Json = JsonUtility.ToJson(levelInfo);

        levels.Add(lv);
    }
    public LevelInfo GetByID(int ID)
    {
        List<LevelJson> list = new List<LevelJson>();
        foreach (var item in levels)
        {
            list.Add(item);
        }

        LevelJson lv = list.FirstOrDefault(x => x.ID == ID);

        if (lv != null)
        {
            LevelInfo result = JsonUtility.FromJson<LevelInfo>(lv.Json);
            return result;
        }

        Debug.LogError("Level ID_Editing not found!");
        return null;
    }
}
