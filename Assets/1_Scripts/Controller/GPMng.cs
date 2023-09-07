using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GPMng : MonoBehaviour
{
    public static GPMng inst;
    public bool IsPlaying = false;
    [SerializeField] int enegy;

    public GameObject winDialog;
    public GameObject loseDialog;
    public int Enegy
    {
        set
        {
            enegy = value;
            enegyDisplay.fillAmount = (float)value / maxEnegy;
        }
        get => enegy;
    }
    [SerializeField] int maxEnegy = 1000;

    public Image enegyDisplay;
    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        Enegy = maxEnegy;
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
        winDialog.SetActive(true);
    }
    public void Lose()
    {
        IsPlaying = false;
        loseDialog.SetActive(true);
    }
}
