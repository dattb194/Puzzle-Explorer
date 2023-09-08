using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMng : MonoBehaviour
{
    public static LevelMng inst;
    public int ID;
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
        levelInfo = LevelEditor.inst.levelsBase.GetByID(ID);
        MaxEnegy = levelInfo.MaxEnegy;
        Enegy = MaxEnegy;
        lineInfos.Clear();
        lineInfos = levelInfo.lineInfos;
        LevelEditor.inst.Load(ID);
        CameraCtrl.inst.SetData(levelInfo.cameraConfig);
    }
}
