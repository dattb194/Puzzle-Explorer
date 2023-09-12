using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMng : MonoBehaviour
{
    public static LevelMng inst;
    public int ID => levelInfo.ID;
    public LevelInfo levelInfo;
    private void Awake()
    {
        inst = this;
    }
    [SerializeField] int enegy;
    public int Enegy
    {
        set
        {
            enegy = value;

        }
        get => enegy;
    }
    public int MaxEnegy = 1000;

    public List<LineInfo> lineInfos;

    public void LoadLevel()
    {
        print(111111);
        levelInfo = LevelEditor.inst.levelInfo;
        MaxEnegy = levelInfo.MaxEnegy;
        Enegy = MaxEnegy;
        lineInfos.Clear();
        lineInfos = levelInfo.lineInfos;
        //LevelEditor.inst.Load(ID);
        CameraCtrl.inst.SetData(levelInfo.cameraConfig);
    }
}
