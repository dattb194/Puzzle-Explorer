using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnLandFace : MonoBehaviour
{
    public bool isOnFace;

    private void OnTriggerStay(Collider other)
    {
        if (isOnFace) return;
        isOnFace = true;
        //switch (other.gameObject.tag)
        //{
        //    case "landFace":
        //        isOnFace = true;
        //        break;
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.tag == "landFace")
            isOnFace = false;
    }
}
