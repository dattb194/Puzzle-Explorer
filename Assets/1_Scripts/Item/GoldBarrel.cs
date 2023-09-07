using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBarrel : MonoBehaviour
{
    bool isCollected = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            if (isCollected) return;
            isCollected = true;
            OnCollected();
        }
    }
    private void OnCollected()
    {
        print("Collected gold barrel");
        Destroy(gameObject);
    }
}
