using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIcon : PrefabHandler
{
    private LevelSO LevelData;
    public Text BestScoreDisplay;
    public Text NameDisplay;

    public override void InjectData(IData data)
    {
        LevelData = data as LevelSO;
    }

    void Start()
    {
        
        BestScoreDisplay.text = "Best Score: " + LevelData.BestScore.ToString();
        NameDisplay.text = LevelData.Name;
    }

    
}
