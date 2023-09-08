using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TesLevel : MonoBehaviour
{
    public void Start()
    {
        if (GPMng.inst != null) return;
        PlayerPrefs.SetString("LevelTest", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("0Gameplay");
    }
}
