using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeviceDebug : MonoBehaviour
{
    public static DeviceDebug instance;
    [SerializeField] Text txt;
    static string mesage;
    private void Awake()
    {
        instance = this;
    }
    public static void Log(string mes)
    {
        mesage += mes + System.Environment.NewLine;
    }

    private void Update()
    {
        txt.text = mesage;
    }
}
