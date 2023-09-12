using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public static CameraCtrl inst;
    private void Awake()
    {
        inst = this;
    }
    public void SetData(CameraConfig cameraConfig)
    {
        GetComponent<CameraFollow>().followPlayer = cameraConfig.followPlayer;
    }
}