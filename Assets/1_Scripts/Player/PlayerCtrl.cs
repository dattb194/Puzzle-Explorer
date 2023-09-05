using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerCtrl : MonoBehaviour
{
    public PlayerStateBehavior stateBehavior;
    // Start is called before the first frame update
    public float direction = 1;
    Rigidbody rig;

    public Transform rope;
    void Start()
    {
        stateBehavior = PlayerStateBehavior.standby;
        rig = GetComponent<Rigidbody>();
        if (rig == null)
            rig = gameObject.AddComponent<Rigidbody>();

    }
    private void Update()
    {
        Standby();
        AutoMove();
        AutoClimb();
    }
    public void AutoMove()
    {
        if (stateBehavior != PlayerStateBehavior.move) return;
        if (rig.isKinematic)
            rig.isKinematic = true;

        transform.position += new Vector3(direction * Time.deltaTime, 0, 0);
    }
    public Climb climb;
    public void Standby()
    {
        if (stateBehavior == PlayerStateBehavior.standby && GPMng.inst.IsPlaying == true)
        {
            stateBehavior = PlayerStateBehavior.move;
        }
    }
    public void AutoClimb()
    {
        if (stateBehavior != PlayerStateBehavior.climb) return;

        if (climb != null) return;

        var points = new Vector3[rope.childCount];
        for (int i = 0; i < rope.childCount; i++)
        {
            points[i] = rope.GetChild(i).position;
        }
        climb = new Climb(points, transform);
        climb.MoveToNextPoint(() =>
        {
            stateBehavior = PlayerStateBehavior.move;
            climb = null;
            rope = null;
        });
        rig.isKinematic = true;
    }
    public List<Transform> roped = new List<Transform>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "rope" && stateBehavior != PlayerStateBehavior.climb)
        {
            if (roped.Exists(x => x == other.gameObject.transform.parent)) return;

            print(1111111111111111);
            rope = other.gameObject.transform.parent;
            stateBehavior = PlayerStateBehavior.climb;

            roped.Add(other.gameObject.transform.parent);
        }
    }
}
public enum PlayerStateBehavior
{
    standby, move, climb
}
