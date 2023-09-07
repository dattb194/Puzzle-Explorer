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
    public int ID;
    public LevelInfo levelInfo;
    public LevelsBase levelsBase;

    public List<ItemEditor> itemPrefab;

    public void Save()
    {
        levelInfo.ID = ID;
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
        Load(ID);
    }
    public void Load(int ID)
    {
        levelInfo = levelsBase.GetByID(ID);

        foreach (var item in levelInfo.items)
        {
            GameObject go = Instantiate(itemPrefab.FirstOrDefault(x => x.type == item.type).gameObject, transform);
            go.transform.position = new Vector3(item.pos.x, item.pos.y, 0);

        }

        print("Load level");
    }
}
