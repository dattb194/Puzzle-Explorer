using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCanDrawBrick : MonoBehaviour
{
    public static ZoneCanDrawBrick inst;
    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        Toggle(false);
    }
    public void Toggle(bool show)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(show);
        }
    }
}
