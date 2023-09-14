using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    public virtual void Start()
    {
        if (gameObject.tag != "landFace")
            gameObject.tag = "landFace";

        AddCollider();
    }
    public virtual void AddCollider()
    {
        if (!GetComponent<Collider>())
        {
            gameObject.AddComponent<MeshCollider>();
            GetComponent<MeshCollider>().isTrigger = false;
        }
    }
}
