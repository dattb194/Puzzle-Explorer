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

    public Color colorBtnAvailble;
    public int StageViewing
    {
        set {
            stageViewing = value;
            btnNext.interactable = value < Resources.Load<GameConfig>("Config/Game config").maxStage;
            btnPre.interactable = value > 1;
            btnNext.GetComponent<Image>().color = value < Resources.Load<GameConfig>("Config/Game config").maxStage ? colorBtnAvailble : Color.white;
            btnPre.GetComponent<Image>().color = value > 1 ? Color.gray : Color.white;
        }
        get => stageViewing;
    }

    public Button btnPre, btnNext;
    public void SetData()
    {
        btnNext.onClick.AddListener(Next);
        btnPre.onClick.AddListener(Pre);

        gameObject.SetActive(true);
        StageViewing = (int)(LevelMng.inst.LevelUnlocked - 1) / 10 + 1;
        ShowWithStageViewing();
    }
    public void Pre()
    {
        StageViewing--;
        ShowWithStageViewing();
    }
    public void Next()
    {
        StageViewing++;
        ShowWithStageViewing();
        print("Next");
    }
    void ShowWithStageViewing()
    {
        int min = StageViewing * 10 - 9;
        txtLevel.text = $"Level {min} - {min + 9}";
        txtStageTitle.text = StageName();
        int indexCard = min;
        for (int i = 0; i < cards.Count; i++)
        {
            bool unlocked = indexCard <= LevelMng.inst.LevelUnlocked;
            cards[i].Setdata(indexCard, indexCard <= LevelMng.inst.LevelUnlocked);
            indexCard++;
        }
    }
    public string StageName()
    {
        switch (stageViewing)
        {
            case 1: return "Jungle";
            case 2: return "Desert";
            case 3: return "Volcano";
            case 4: return "Tomb";
            case 5: return "Ice Mountaint";
            
        }
        return "";
    }
}