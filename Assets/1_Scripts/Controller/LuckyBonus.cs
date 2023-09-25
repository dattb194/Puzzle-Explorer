using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LuckyBonus : MonoBehaviour
{
    [SerializeField] Text txtGold;
    Transform textRoot;
    Transform arrowRoot;
    [SerializeField] int mValue;

    [SerializeField] int goldBonus;

    bool getBonusSuccess = false;
    public int GoldBonus
    {
        set
        {
            goldBonus = value;
        }
        get => goldBonus;
    }
    public UnityAction<int> OnChoiseSuccess;
    public int MValue
    {
        set
        {
            if (value > 6)
                value = 0;

            int hs = 2;
            switch (value)
            {
                case 0:
                case 6: hs = 2; break;
                case 1:
                case 5: hs = 3; break;
                case 2:
                case 4: hs = 4; break;
                case 3: hs = 5; break;
            }

            GoldBonus = Resources.Load<GameConfig>("Config/Game config").goldBonus * hs;
            txtGold.text = "" + GoldBonus;

            mValue = value;
            for (int i = 0; i < textRoot.childCount; i++)
            {
                textRoot.GetChild(i).transform.localScale = value == i ? Vector3.one * 1.4f : Vector3.one * 1f;
                arrowRoot.GetChild(i).gameObject.SetActive(value == i ? true : false);
            }
        }
        get => mValue;
    }
    float coutTime = 0;
    private void Start()
    {
        textRoot = transform.GetChild(0);
        arrowRoot = transform.GetChild(1);
        MValue = 2;
    }
    private void Update()
    {
        if (getBonusSuccess) return;
        if (coutTime >= .5f)
        {
            MValue++;
            coutTime = 0;
        }
        else
            coutTime += Time.deltaTime;
    }
    public void GetReward()
    {
        getBonusSuccess = true;
        LevelMng.inst.GoldCurrent += GoldBonus;
        OnChoiseSuccess?.Invoke(GoldBonus);
        Invoke("ReloadScene", 2);
    }
    public void NoThank()
    {
        getBonusSuccess = true;
        LevelMng.inst.GoldCurrent += Resources.Load<GameConfig>("Config/Game config").goldBonus;
        OnChoiseSuccess?.Invoke(Resources.Load<GameConfig>("Config/Game config").goldBonus);
        Invoke("ReloadScene", 2);
    }

    void ReloadScene()
    {
        GPMng.inst.ReloadScene();
    }
}
