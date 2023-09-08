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
        }
        get => stateBehavior;
    }
    // Start is called before the first frame update
    public float direction = 1;
    Rigidbody rig;

    public List<Transform> ropesClimbed = null;
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
            StateBehavior = PlayerStateBehavior.move;
            rope.gameObject.SetActive(false);
            //ropeDeteched = null;
        });
        rig.isKinematic = true;
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
        if (playerMovement.trans == null) playerMovement = new PlayerMovement(transform);
        playerMovement.Update();
    }
}
public enum PlayerStateBehavior
{
    standby, move, climb
}
