using UnityEngine;
using DG.Tweening;

[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    public Vector2 ratio;
    public Transform player;

    public float x, y;

    public bool followPlayer;
    void LateUpdate()
    {
        if (!followPlayer) return;

        if (player == null)
        {
            if(FindObjectOfType<PlayerCtrl>())
                player = FindObjectOfType<PlayerCtrl>().transform;
            return;
        }


        x = player.transform.position.x - ratio.x * Screen.width;
        y = player.transform.position.y - ratio.y * Screen.height;

        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        //transform.DOMove(pos, .1f);
    }
}
