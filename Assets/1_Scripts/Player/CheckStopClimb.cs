using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStopClimb : MonoBehaviour
{
    public PlayerCtrl playerCtrl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "landFace")
        {
            print(222222222222);
            if (playerCtrl.StateBehavior == PlayerStateBehavior.climb)
            {
                print(3333333333333);
                playerCtrl.Behavior.climb.ForceStop(()=>
                {
                    playerCtrl.Behavior.StopClimb();
                });
            }
        }
    }
}
