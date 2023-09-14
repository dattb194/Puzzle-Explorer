using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBox : Land
{
    public override void AddCollider()
    {
        if (!GetComponent<Collider>())
        {
            gameObject.AddComponent<BoxCollider>();
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
