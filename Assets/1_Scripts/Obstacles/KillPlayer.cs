using System.Collections;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public bool isDisabled = false;
    private void OnTriggerEnter(Collider other)
    {
        if (isDisabled) return;

        if (other.gameObject.tag == "player")
        {
            other.GetComponent<PlayerBehavior>().Die();
            GPMng.inst.Lose();
        }
    }
}