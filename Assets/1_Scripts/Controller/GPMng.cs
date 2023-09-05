using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GPMng : MonoBehaviour
{
    public static GPMng inst;
    public bool IsPlaying = false;
    private void Awake()
    {
        inst = this;
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
    public void StartGame()
    {
        IsPlaying = true;
    }
}
