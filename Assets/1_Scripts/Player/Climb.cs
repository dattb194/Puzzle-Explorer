using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Climb
{
    public Transform posClimb;
    public Vector3[] points;
    public float duration = 2;

    public Transform rope;

    public bool isActive = false;

    public Climb(Vector3[] points, Transform posClimb, float duration = 1)
    {
        this.posClimb = posClimb;
        this.points = points;
        this.duration = duration;
    }
    public Climb(Transform rope,  Transform posClimb,float duration = 1)
    {
        this.rope = rope;
        this.posClimb = posClimb;
        this.duration = duration;
    }

    public void DoClimbWithPoints(UnityAction done = null)
    {
        duration = 1.5f;
        posClimb.transform.DOPath(points, duration).SetEase(Ease.Linear).SetSpeedBased().SetAutoKill(true).onComplete += () =>
        {
            done?.Invoke();
        };
    }
    public void DoClimbWithRope(UnityAction done = null)
    {
        var root = rope.parent;
        points = new Vector3[root.childCount - 1];

        if (rope == root.GetChild(0))
        {
            for (int i = 0; i < root.childCount - 1; i++)
            {
                points[i] = root.GetChild(i).position;
            }
        }

        if (rope == root.GetChild(root.childCount - 2))
        {
            for (int i = root.childCount - 2; i >= 0; i--)
            {
                points[root.childCount - 2 - i] = root.GetChild(i).position;
            }
        }
        DoClimbWithPoints(done);
    }
}