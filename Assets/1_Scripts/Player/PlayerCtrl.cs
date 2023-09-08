using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] PlayerStateBehavior stateBehavior;
    public PlayerStateBehavior StateBehavior
    {
        set
        {
            stateBehavior = value;
            {
                GetComponent<PlayerAnimator>().Trigger(((int)value).ToString());
            }
        }
        get => stateBehavior;
    }
    // Start is called before the first frame update
    public float direction = 1;
    Rigidbody rig;

    public List<Transform> ropesClimbed = null;
    [SerializeField] CheckOnLandFace checkOnLandFace;
    void Start()
    {
        StateBehavior = PlayerStateBehavior.standby;
        rig = GetComponent<Rigidbody>();
        ropesClimbed = new List<Transform>();
        if (rig == null)
            rig = gameObject.AddComponent<Rigidbody>();

    }
    private void Update()
    {
        if (!GPMng.inst) return;
        if (!GPMng.inst.IsPlaying) return;
        Standby();
        AutoMove();
        CheckIdle();
        Falling();
    }
    public void AutoMove()
    {
        if (StateBehavior != PlayerStateBehavior.move) return;
        if (rig.isKinematic)
            rig.isKinematic = false;

        transform.position += new Vector3(direction * Time.deltaTime, 0, 0);
    }
    public void Standby()
    {
        if (StateBehavior != PlayerStateBehavior.standby) return;
        StateBehavior = PlayerStateBehavior.move;
    }
    public void Climb(Transform rope)
    {
        //Transform rope = ropeDeteched;
        StateBehavior = PlayerStateBehavior.climb;

        ropesClimbed.Add(rope.parent);

        var climb = new Climb(rope, transform);
        climb.DoClimbWithRope(() =>
        {
            StateBehavior = PlayerStateBehavior.falling;
            rope.gameObject.SetActive(false); 
            rig.isKinematic = false;
            Invoke(nameof(EndFalling), 1);
            //ropeDeteched = null;
        });
        rig.isKinematic = true;
    }
    void Falling()
    {
        if (StateBehavior != PlayerStateBehavior.falling) return;
        if (checkOnLandFace.isOnFace)
        {
            StateBehavior = PlayerStateBehavior.falling_2;
        }
    }
    void EndFalling()
    {
        StateBehavior = PlayerStateBehavior.move;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "rope":
                if (StateBehavior == PlayerStateBehavior.climb) break;

                var root = other.transform.parent;
                var trans = other.transform;

                if (ropesClimbed.Exists(x => x == root)) break;
                //if (ropeDeteched != null) break;
                //ropeDeteched = trans;

                if (trans != root.GetChild(0) &&
                    trans != root.GetChild(root.childCount - 2))
                {
                    //ropeDeteched = null;
                    break;
                }

                Climb(trans);
                break;
        }
    }

    [SerializeField] PlayerMovement playerMovement;
    void CheckIdle()
    {
        if (playerMovement.trans == null)
            playerMovement = new PlayerMovement(transform);
        playerMovement.Update();

        if (playerMovement.isNotMove)
            StateBehavior = PlayerStateBehavior.standby;
    }
}
public enum PlayerStateBehavior
{
    standby = 1, move = 2, climb = 3, falling_1 = 4, falling_2 = 5, win = 6, lose = 7, falling = 7
}
