using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFire : LineElement
{
    public override void Active()
    {
        base.Active();
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
