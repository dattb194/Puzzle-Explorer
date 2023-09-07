using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] bool isSolid = false;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "lineFire":
                if (isSolid) break;
                isSolid = true;
                Burned();
                break;
        }
    }
    void Burned()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Renderer>().material.color = Color.black;
        Destroy(gameObject, 1);
    }
    void KillPlayer()
    {
        GPMng.inst.IsPlaying = false;
        print("Kill player");
    }
}
