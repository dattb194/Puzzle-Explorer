using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<string> tagCanKill;

    private void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.tag)
        {
            case "player":
                KillPlayer();
                break;
            default:
                if (tagCanKill.Exists(x => x == other.gameObject.tag))
                    BeKill();
                break;
        }
    }
    void BeKill()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Renderer>().material.color = Color.black;
        Destroy(gameObject, 1);
    }
    void KillPlayer()
    {
        GPMng.inst.Lose();
        print("Kill player");
    }
}
