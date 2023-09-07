using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GPMng : MonoBehaviour
{
    public static GPMng inst;
    public bool IsPlaying = false;

    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        LevelMng.inst.LoadLevel();
        DrawingPhysics.inst.Initialize();
        UIMng.inst.SetbtnsDraw();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame()
    {
        IsPlaying = true;
    }
    public void Win()
    {
        IsPlaying = false;
        UIMng.inst.Win();
    }
    public void Lose()
    {
        IsPlaying = false;
        UIMng.inst.Lose();
    }
}
