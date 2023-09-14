using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    PlayerCtrl playerCtrl;
    [SerializeField] PlayerAnimator playerAnim;
    Transform posClimb;

    public float moveMotion = 0;
    public float giatoc = 1f;
    public float direction = 1;

    public Climb climb;
    private void Start()
    {
        playerCtrl = GetComponent<PlayerCtrl>();
    }
    public void Falling()
    {

    }

    float posXPre;
    float timeCheckPosX;
    bool climbFoward;
    public void Climbing()
    {
        transform.position = posClimb.GetChild(0).position;

        transform.GetChild(0).localScale = new Vector3(1, 1, climbFoward ? 1 : -1);

        if (timeCheckPosX > 0)
        {
            timeCheckPosX -= Time.deltaTime;
        }
        else
        {
            timeCheckPosX = .3f;
            print($"transform.position.x: {transform.position.x}///posXPre: {posXPre}");
            if (transform.position.x >= posXPre)
                climbFoward = true;
            else
                climbFoward = false;

            posXPre = transform.position.x;
        }
    }
    public void Moving()
    {
        if (moveMotion < 1)
        {
            moveMotion += Time.deltaTime * giatoc * direction;
        }
        else
            moveMotion = 0;

        transform.position += new Vector3(direction * Time.deltaTime, 0, 0);
        playerCtrl.PlayerMovement.Update();
    }

    public float timeCheckJump = 1;
    public void CheckJump()
    {
        if (!playerCtrl.PlayerMovement.isNotMove)
        {
            timeCheckJump = 0;
            return;
        }

        if (timeCheckJump < 1)
        {
            timeCheckJump += Time.deltaTime;
            return;
        }

        transform.position += new Vector3(direction / 10, direction / 20, 0);
        timeCheckJump = 0;
    }
    public void ForceClimb(Transform rope, Transform _posClimb)
    {
        if (posClimb == null)
        {
            posClimb = Instantiate(_posClimb);
        }
        posClimb.position = rope.position;
        posXPre = transform.position.x;
        print("StartClimb");
        playerCtrl.StateBehavior = PlayerStateBehavior.climb;
        playerCtrl.ropesClimbed.Add(rope.parent);

        climb = new Climb(rope, posClimb);
        climb.DoClimbWithRope(() =>
        {
            StopClimb();
        });
        playerCtrl.Rig.isKinematic = true;
    }
    public void StopClimb()
    {
        print("playerCtrl.StateBehavior = PlayerStateBehavior.falling");
        playerCtrl.StateBehavior = PlayerStateBehavior.falling;
        climb.rope.gameObject.SetActive(false);
        playerCtrl.Rig.isKinematic = false;
        transform.GetChild(0).transform.localScale = Vector3.one;
    }
    public void Die()
    {
        playerAnim.Die();
        playerCtrl.StateBehavior = PlayerStateBehavior.lose;
    }
}
