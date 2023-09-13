using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<string> tagCanKill;

    public Animator anim;

    [SerializeField] EnemyState state;
    public EnemyState State
    {
        set {
            state = value;
        }
        get => state;
    }
    public virtual void Start()
    {
        State = EnemyState.idle;
    }
    private void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.tag)
        {
            case "player": 
                Attack();
                break;
            default:
                if (tagCanKill.Exists(x => x == other.gameObject.tag))
                    BeKill();
                break;
        }
    }
    public virtual void BeKill()
    {
        State = EnemyState.die;
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Renderer>().material.color = Color.black;
        Destroy(gameObject, 1);
    }
    public virtual void Update()
    {
        anim.SetInteger("state", (int)State);

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Attack();
        }
    }
    public virtual void Attack()
    {
        State = EnemyState.attack;
    }
}
public enum EnemyState
{ 
    idle = 0, attack = 1 , die = 2
}
