using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPanel : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = LevelMng.inst.GoldCurrent.ToString();
        LevelMng.inst.GoldCurrentChange += value => transform.GetChild(0).GetComponent<Text>().text = value.ToString();
    }
}
