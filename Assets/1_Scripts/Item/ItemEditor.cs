using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemEditor : MonoBehaviour
{
    public TypeItem type;
    public bool makePrefab = false;
    private void Start()
    {
        LoadFrefab();
    }
    public void LoadFrefab()
    {
        var prefab = LevelEditor.inst.itemPrefab.FirstOrDefault(x => x.type == type);

        if (prefab == null)
            print("dont found item " + type);
        else
        {
            GameObject item = (GameObject)Instantiate(prefab.gameObject, null);
            item.GetComponent<ItemEditor>().enabled = false;
            item.transform.position = transform.position;
            item.transform.localRotation = transform.localRotation;
            DestroyImmediate(gameObject);
        }

        
    }
}
public enum TypeItem
{ 
    player,
    tree,
    sphere_stone,
    land_face,
    lane_underground,
    gold_barrel,
    lava,
    win_point,
    enemy_1
}
