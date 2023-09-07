using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    bool isSolid = false;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "player":
                KillPlayer();
                break;
            case "lineWater":
                if (isSolid) break;
                isSolid = true;
                Solidify();
                break;
        }
    }
    void Solidify()
    {
        GetComponent<Collider>().isTrigger = false;
        GetComponent<Renderer>().material.color = Color.cyan;
    }
    void KillPlayer()
    {
        GPMng.inst.Lose();
        print("Kill player");
    }
}
