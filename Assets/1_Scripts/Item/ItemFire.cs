using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFire : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "lineWater")
            Destroy(gameObject);
    }
}
