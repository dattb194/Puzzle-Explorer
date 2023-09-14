using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckyBonus : MonoBehaviour
{
    Transform textRoot;
    Transform arrowRoot;
    [SerializeField] int mValue;
    [SerializeField] Text txtGold;

    public int goldBonus;
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

            goldBonus = Resources.Load<GameConfig>("Config/Game config").goldBonus * hs;
            txtGold.text = "" + goldBonus;

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
        if (coutTime >= .5f)
        {
            MValue++;
            coutTime = 0;
        }
        else
            coutTime += Time.deltaTime;
    }
}
