using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Purchasing;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] PlayerStateBehavior stateBehavior;
    [SerializeField] GameObject posClimb;
    public PlayerStateBehavior StateBehavior
    {
        set
        {
            stateBehavior = value;

            switch (value)
            {
                case PlayerStateBehavior.move:
                    rig.isKinematic = false;
                    break;

            }
        }
        get => stateBehavior;
    }
    // Start is called before the first frame update

    PlayerBehavior behavior;
    public PlayerBehavior Behavior => behavior;

    [HideInInspector] public List<Transform> ropesClimbed = null;
    
    [SerializeField] PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;
    Rigidbody rig;
    public Rigidbody Rig => rig;

    [SerializeField] CheckOnLandFace checkOnLandFace;
    public CheckOnLandFace CheckOnLandFace => CheckOnLandFace;

    void Start()
    {
        StateBehavior = PlayerStateBehavior.standby;
        behavior = GetComponent<PlayerBehavior>();
        rig = GetComponent<Rigidbody>();
        ropesClimbed = new List<Transform>();
        playerMovement = new PlayerMovement(this);

        if (rig == null)
            rig = gameObject.AddComponent<Rigidbody>();

    }
    public void ForceMove()
    {
        print("ForceMove");
        behavior.moveMotion = 0;
        StateBehavior = PlayerStateBehavior.move;
    }

    private void Update()
    {
        if (!GPMng.inst) return;
        if (!GPMng.inst.IsPlaying) return;

        behavior.Moving();
        playerMovement.Update();


        switch (StateBehavior)
        {
            case PlayerStateBehavior.standby:
                if (checkOnLandFace)
                    ForceMove();
                break;

            case PlayerStateBehavior.idle:
                if (!playerMovement.isNotMove)
                    StateBehavior = PlayerStateBehavior.move;
                break;

            case PlayerStateBehavior.move:

                if (checkOnLandFace.isOnFace == false)
                    StateBehavior = PlayerStateBehavior.falling;
                if (playerMovement.isNotMove)
                    StateBehavior = PlayerStateBehavior.idle;
                break;

            case PlayerStateBehavior.falling:
                if (checkOnLandFace.isOnFace)
                {
                    StateBehavior = PlayerStateBehavior.falling_2;
                }
                break;
            case PlayerStateBehavior.climb:
                behavior.Climbing();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "rope":
                if (StateBehavior == PlayerStateBehavior.climb) break;

                var root = other.transform.parent;
                var rope = other.transform;

                if (ropesClimbed.Exists(x => x == root)) break;

                if (rope != root.GetChild(0) &&
                    rope != root.GetChild(root.childCount - 2))
                {
                    break;
                }

                behavior.ForceClimb(rope, posClimb.transform);

                break;
        }
    }
}
public enum PlayerStateBehavior
{
    idle = 0,standby = 1, move = 2, climb = 3, falling_1 = 4, falling_2 = 5, win = 6, lose = 7, falling = 8
}
