using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public static LevelEditor inst;
    private void Awake()
    {
        inst = this;
    }
    //public int ID_Editing;
    public LevelInfo levelInfo;
    public LevelsBase levelsBase => Resources.Load<LevelsBase>("Levels");

    public List<ItemEditor> itemPrefab;

    public void Save()
    {
        levelInfo.items.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<ItemEditor>() != null)
            {
                var info = new ItemInfo();
                info.type = transform.GetChild(i).GetComponent<ItemEditor>().type;
                info.pos = transform.GetChild(i).position;
                levelInfo.items.Add(info);
            }
            else
            {
                Debug.LogError($"Item {transform.GetChild(i).name} not item editor");
            }
        }


        levelsBase.UpdateLevel(levelInfo);
        print("Save level");
    }
    public void Load()
    {
        Load(levelInfo.ID);
        gameObject.name = $"Level {levelInfo.ID}";
    }
    public void Load(int ID)
    {
        DeleteLevelNowFromScene();
        levelInfo = levelsBase.GetByID(ID);

        foreach (var item in levelInfo.items)
        {
            GameObject go = Instantiate(itemPrefab.FirstOrDefault(x => x.type == item.type).gameObject, transform);
            go.transform.position = new Vector3(item.pos.x, item.pos.y, 0);

        }

        print("Load level");
    }
    public void DeleteLevelNowFromScene()
    {
        var trans = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            trans.Add(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < trans.Count; i++)
        {
            DestroyImmediate(trans[i].gameObject);
        }
    }
}
