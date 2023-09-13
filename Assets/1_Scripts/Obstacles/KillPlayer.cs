using System.Collections;
using UnityEngine;

namespace Assets._1_Scripts.Obstacles
{
    public class KillPlayer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "player")
            {
                other.GetComponent<PlayerBehavior>().Die();
                GPMng.inst.Lose();
            }
        }
    }
}