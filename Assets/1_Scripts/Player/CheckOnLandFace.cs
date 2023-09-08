using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnLandFace : MonoBehaviour
{
    public bool isOnFace;

    private void OnTriggerStay(Collider other)
    {
        if (isOnFace) return;
        if (other.gameObject.tag == "landFace")
            isOnFace = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "landFace")
            isOnFace = false;
    }
}
