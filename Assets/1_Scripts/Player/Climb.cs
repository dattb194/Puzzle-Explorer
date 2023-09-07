using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Climb
{
    public Transform player;
    public Vector3[] points;
    public float duration = 2;

    public Transform rope;

    public bool isActive = false;

    public Climb(Vector3[] points, Transform player, float duration = 2)
    {
        this.points = points;
        this.player = player;
        this.duration = duration;
    }
    public Climb(Transform rope, Transform player, float duration = 2)
    {
        this.rope = rope;
        this.player = player;
        this.duration = duration;
    }

    public void DoClimbWithPoints(UnityAction done = null)
    {
        player.transform.DOPath(points, duration).SetEase(Ease.Linear).SetAutoKill(true).onComplete += () =>
        {
            done?.Invoke();
        };
    }
    public void DoClimbWithRope(UnityAction done = null)
    {
        Debug.Log(rope.name);
        var root = rope.parent;
        points = new Vector3[root.childCount - 1];

        if (rope == root.GetChild(0))
        {
            Debug.Log(44444);
            for (int i = 0; i < root.childCount - 1; i++)
            {
                points[i] = root.GetChild(i).position;
            }
        }

        if (rope == root.GetChild(root.childCount - 2))
        {
            Debug.Log(5555555);
            for (int i = root.childCount - 2; i >= 0; i--)
            {
                points[root.childCount - 2 - i] = root.GetChild(i).position;
            }
        }
        DoClimbWithPoints(done);
    }
}