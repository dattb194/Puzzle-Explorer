using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageDisplay : MonoBehaviour
{
    public Text txtStageTitle;
    public Text txtLevel;

    public List<StageCard> cards;

    [SerializeField] int stageViewing;
    public int StageViewing
    {
        set {
            stageViewing = value;
            int maxStage = 6;
            btnNext.interactable = value < maxStage;
            btnPre.interactable = value > 1;
        }
    }

    public Button btnPre, btnNext;
    public void SetData()
    {
        gameObject.SetActive(true);
        stageViewing = (int)(LevelMng.inst.LevelUnlocked - 1) / 10 + 1;
        ShowWithStageViewing();
    }
    public void Pre()
    {
        stageViewing--;
        ShowWithStageViewing();
    }
    public void Next()
    {
        stageViewing++;
        ShowWithStageViewing();
    }
    void ShowWithStageViewing()
    {
        int min = stageViewing * 10 - 9;
        txtLevel.text = $"Level {min} - {min + 9}";
        int indexCard = min;
        for (int i = 0; i < cards.Count; i++)
        {
            bool unlocked = indexCard <= LevelMng.inst.LevelUnlocked;
            cards[i].Setdata(indexCard, indexCard <= LevelMng.inst.LevelUnlocked);
            indexCard++;
        }
    }
}