using System.Collections;
using UnityEngine;

public class LineElement : MonoBehaviour
{
    public virtual void Active()
    {
        GetComponent<Collider>().enabled = true;
    }
}