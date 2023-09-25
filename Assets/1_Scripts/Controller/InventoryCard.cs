using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCard : MonoBehaviour
{
    [SerializeField] int ID;
    [SerializeField] int state = -1;// 0: chua mo khoa//price, 1: mo khoa nhung khong chon//owner, 2: dang su dung//equipped
    public int State
    {
        get => state;
        set
        {
            state = value;
            for (int i = 0; i < objs.Count; i++)
            {
                objs[i].SetActive(i == value);
            }
        }
    }

    public List<GameObject> objs;
    [SerializeField] Image imgAvatar;
    [SerializeField] Image imgTypeMoney;
    [SerializeField] Text txtPrice;
    [SerializeField] Sprite[] spr;

    [SerializeField] Button btnPrice;
    [SerializeField] Button btnEquipment;

    public void SetData(int ID)
    {
        this.ID = ID;
        imgAvatar.sprite = InventoryMng.inst.SkinSprite(ID);
        txtPrice.text = InventoryMng.inst.GetSkin(ID).Price.ToString();
        imgTypeMoney.sprite = InventoryMng.inst.GetSkin(ID).Type == "Gold" ? spr[0] : spr[1];

        btnPrice.onClick.AddListener(Price);
        btnEquipment.onClick.AddListener(Equippment);

        Refresh();

    }
    public void Refresh()
    {
        if (ID == InventoryMng.inst.SkinEquipped)
            State = 2;
        else
        {
            if (InventoryMng.inst.CheckOwned(ID))
            {
                State = 1;
                btnEquipment.interactable = InventoryMng.inst.SkinEquipped != ID;
            }
            else
            {
                State = 0;
                int have = (InventoryMng.inst.GetSkin(ID).Type == "Gold") ? LevelMng.inst.GoldCurrent : LevelMng.inst.DiamondCurrent;
                btnPrice.interactable = (have >= InventoryMng.inst.GetSkin(ID).Price);
            }
        }
    }

    public void Price()
    {
        if (InventoryMng.inst.GetSkin(ID).Type == "Gold")
            LevelMng.inst.GoldCurrent -= InventoryMng.inst.GetSkin(ID).Price;
        else
            LevelMng.inst.DiamondCurrent -= InventoryMng.inst.GetSkin(ID).Price;

        var _skinOwned = InventoryMng.inst.SkinsOwned;
        _skinOwned.list.Add(ID);
        InventoryMng.inst.SkinsOwned = _skinOwned;

        Equippment();
    }
    public void Equippment()
    {
        InventoryMng.inst.SkinEquipped = ID;
        FindFirstObjectByType<InventoryDisplay>().RefreshCards();
    }
}
