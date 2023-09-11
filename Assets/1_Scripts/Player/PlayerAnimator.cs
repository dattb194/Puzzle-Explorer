using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] PlayerCtrl player;
    private void Update()
    {
        anim.SetInteger("state", (int)player.StateBehavior);
        anim.SetFloat("moveMotion", player.Behavior.moveMotion);
    }
    public void Trigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }
    public void OnEndFalling2()
    {
        player.ForceMove();
    }
}
