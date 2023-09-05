using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public float direction = 1;
    Rigidbody rig;
    IEnumerator Start()
    {
        rig = GetComponent<Rigidbody>();
        if (rig == null)
            rig = gameObject.AddComponent<Rigidbody>();

        while (!GPMng.inst.IsPlaying) yield return null;
        while (GPMng.inst.IsPlaying)
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
            rig.AddForce(transform.right * direction);
            yield return null;
        }
    }

}
