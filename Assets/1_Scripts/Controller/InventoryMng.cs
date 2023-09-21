using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryMng : MonoBehaviour
{
    public static InventoryMng inst;

    [SerializeField] int skinEquipped;
    public int SkinEquipped
    {
        set {
            skinEquipped = value;
            PlayerPrefs.SetInt(PPKey.SkinEquipped, value);
        }
        get
        {
            int result = 1;
            if (PlayerPrefs.HasKey(PPKey.SkinEquipped))
                result = PlayerPrefs.GetInt(PPKey.SkinEquipped);
            skinEquipped = result;
            return result;
        }
    }

    [SerializeField] SkinsOwnedData skinsOwned;
    public SkinsOwnedData SkinsOwned
    {
        get
        {
            SkinsOwnedData result = new SkinsOwnedData();
            if (PlayerPrefs.HasKey(PPKey.SkinsOwned))
            {
                result = JsonUtility.FromJson<SkinsOwnedData>(PlayerPrefs.GetString(PPKey.SkinsOwned));
            }

            result.list.Add(1);
            return result;
        }
        set
        {
            skinsOwned = new SkinsOwnedData();
            skinsOwned = value;
            PlayerPrefs.SetString(PPKey.SkinsOwned, JsonUtility.ToJson(value));
        }
    }
    [SerializeField] List<Sprite> skinSprites;
    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        skinsOwned = SkinsOwned;
    }
    public bool CheckOwned(int ID)
    {
        return SkinsOwned.list.Exists(x => x == ID);
    }
    public Sprite SkinSprite(int ID)
    {
        if (skinSprites.Exists(x => x.name == ID.ToString()))
            return skinSprites.FirstOrDefault(x => x.name == ID.ToString());

        Debug.LogError("Do not found skin sprite!");
        return null;
    }
    public SkinInfo GetSkin(int ID)
    {
        SkinInfo result = new SkinInfo();
        if (GCMng.inst.listSkin.Exists(x => x.ID == ID))
        {
            var info = GCMng.inst.listSkin.FirstOrDefault(x => x.ID == ID);
            result.ID = ID;
            result.Price = info.Price;
            result.Type = info.Type;

            return result;
        }
        Debug.LogError($"Do not found Skin with ID {ID}");
        return result;
    }
}
[System.Serializable]
public class SkinsOwnedData
{
    public List<int> list = new List<int>();
}