using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Climb
{
    public Transform trans;
    public Vector3[] points; // The array of points to move to
    public float duration = 2; // The duration of each movement
    public Ease easeType = Ease.Linear; // The easing type of the movement
    private int currentIndex; // The index of the current point
    public Climb(Vector3[] points, Transform trans)
    {
        this.points = points;
        this.trans = trans;
        currentIndex = 0;
        MoveToNextPoint();
    }


    public void MoveToNextPoint(UnityAction done = null)
    {
        trans.DOPath(points, duration).SetEase(easeType).SetAutoKill(true).onComplete += () =>
        {
            done?.Invoke();
            trans.position = points[points.Length - 1];
        };

        //    if (currentIndex < points.Length) // Check if there are more points to move to
        //    {
        //        trans.DOMove(points[currentIndex], duration) // Move the player to the current point with the given duration and easing type
        //            .SetEase(easeType)
        //            .OnComplete(() => // Set a callback function to be executed when the movement is completed
        //            {
        //                if (currentIndex >= points.Length - 1)
        //                {
        //                    done?.Invoke();
        //                    Debug.LogError("done?.Invoke();");
        //                }
        //                else
        //                {
        //                    currentIndex++; // Increment the index and move to the next point
        //                    MoveToNextPoint(); // Recursively call this function
        //                }
        //            });
        //    }
        //}
    }
}