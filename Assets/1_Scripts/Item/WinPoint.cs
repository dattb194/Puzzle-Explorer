using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    bool isTrigged = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            if (isTrigged) return;
            isTrigged = true;
            Win();
        }
    }
    private void Win()
    {
        print("Winnnnnnnnnn");
        GPMng.inst.Win();
    }
}
