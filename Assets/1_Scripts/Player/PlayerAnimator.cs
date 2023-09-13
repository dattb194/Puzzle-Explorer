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
    public void Die()
    {
        anim.SetTrigger("die");
    }
    public void Win()
    {
        anim.SetTrigger("win");
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
