using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondPanel : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = LevelMng.inst.DiamondCurrent.ToString();
        LevelMng.inst.DiamondCurrentChange += value => transform.GetChild(0).GetComponent<Text>().text = value.ToString();
    }
}
