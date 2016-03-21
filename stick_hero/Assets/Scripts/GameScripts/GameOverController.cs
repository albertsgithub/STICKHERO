// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public Text ScoreText;              //游戏结束界面分数标签引用
    public Text BestText;              //游戏结束界面最高分标签引用

    void Awake() { 
        //最高分
        int best = PlayerPrefs.GetInt("game_best");
        ScoreText.text = GameController.score.ToString();
        BestText.text = best.ToString();
    }

}
