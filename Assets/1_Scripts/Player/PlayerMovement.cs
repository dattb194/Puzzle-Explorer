using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerMovement
{
    public Transform trans;
    public float biendo = .2f;
    public MonoBehaviour monoBehaviour;
    public PlayerMovement(MonoBehaviour monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
        this.trans = monoBehaviour.transform;
        previousPosition = trans.position;
        isNotMove = false;
    }

    private Vector3 previousPosition;

    public bool isNotMove;

    float timeCheck = 0;
    public void Update()
    {

        if (timeCheck > 0)
        {
            timeCheck -= Time.deltaTime;
        }
        else
        {
            Check();
            previousPosition = trans.position;
        }
    }
    public void Check()
    {
        timeCheck = 1;
        if (Vector3.Distance(trans.position, previousPosition) <= biendo)
            isNotMove = true;
        else
            isNotMove = false;

    }
}
