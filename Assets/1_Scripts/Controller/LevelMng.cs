using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class LevelMng : MonoBehaviour
{
    public static LevelMng inst;
    public int ID => levelInfo.ID;
    public LevelInfo levelInfo;

    [SerializeField] int levelPlaying;
    public int LevelPlaying
    {
        get
        {
            if (PlayerPrefs.HasKey(PPKey.LevelPlaying))
                return PlayerPrefs.GetInt(PPKey.LevelPlaying);
            return 1;
        }
        set
        {
            levelPlaying = value;
            PlayerPrefs.SetInt(PPKey.LevelPlaying, value);
            if (value > LevelUnlocked)
                LevelUnlocked = value;
        }
    }
    [SerializeField] int levelUnlocked;
    public int LevelUnlocked
    {
        get
        {
            if (PlayerPrefs.HasKey(PPKey.LevelUnlocked))
                return PlayerPrefs.GetInt(PPKey.LevelUnlocked);
            return 1;
        }
        set
        {
            levelUnlocked = value;
            PlayerPrefs.SetInt(PPKey.LevelUnlocked, value);
        }
    }
    [SerializeField] int stagePlaying;
    public int StagePlaying
    {
        get
        {
            if (PlayerPrefs.HasKey(PPKey.StagePlaying))
                return PlayerPrefs.GetInt(PPKey.StagePlaying);
            return 1;
        }
        set
        {
            stagePlaying = value;
            PlayerPrefs.SetInt(PPKey.StagePlaying, value);
        }
    }
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

    [SerializeField] int goldCurrent;
    public int GoldCurrent
    {
        set
        {
            if (value < 0)
                value = 0;
            goldCurrent = value;
            PlayerPrefs.SetInt(PPKey.GoldCurrent, value);
            GoldCurrentChange?.Invoke(value);
        }
        get
        {
            return PlayerPrefs.GetInt(PPKey.GoldCurrent);
        }
    }
    [SerializeField] int diamondCurrent;
    public int DiamondCurrent
    {
        set
        {
            if (value < 0)
                value = 0;
            diamondCurrent = value;
            PlayerPrefs.SetInt(PPKey.DiamondCurrent, value);
            DiamondCurrentChange?.Invoke(value);
        }
        get
        {
            return PlayerPrefs.GetInt(PPKey.DiamondCurrent);
        }
    }

    public List<LineInfo> lineInfos;

    public UnityAction<int> GoldCurrentChange;
    public UnityAction<int> DiamondCurrentChange;
    public void LoadLevel()
    {
        levelInfo = LevelEditor.inst.levelInfo;
        MaxEnegy = levelInfo.MaxEnegy;
        Enegy = MaxEnegy;
        lineInfos.Clear();
        lineInfos = levelInfo.lineInfos;
        CameraCtrl.inst.SetData(levelInfo.cameraConfig);
    }

    private void Update()
    {
        levelPlaying = LevelPlaying;
        levelUnlocked = LevelUnlocked;
    }
}
