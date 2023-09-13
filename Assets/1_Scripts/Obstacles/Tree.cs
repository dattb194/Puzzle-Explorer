using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "lineFire":
                Burned();
                break;
        }
    }
    void Burned()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Collider>().isTrigger = true;
        transform.GetChild(0).GetComponent<Renderer>().material.color = Color.black;
        Destroy(gameObject, 1);
    }
}
