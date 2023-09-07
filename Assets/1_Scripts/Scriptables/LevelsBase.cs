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

        var lv = new LevelJson();
        lv.ID = levelInfo.ID;
        lv.Json = JsonUtility.ToJson(levelInfo);

        levels.Add(lv);
    }
    public LevelInfo GetByID(int ID)
    {
        if (levels.Exists(x => x.ID == ID))
        {
            var lv = levels.FirstOrDefault(x => x.ID == ID);

            return JsonUtility.FromJson<LevelInfo>(lv.Json);
        }
        Debug.LogError("Level ID not found!");
        return null;
    }
}
