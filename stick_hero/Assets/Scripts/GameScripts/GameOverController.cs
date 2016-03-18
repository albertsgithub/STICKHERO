// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public GameObject ScoreText;              //游戏结束界面分数标签引用
    public GameObject BestText;              //游戏结束界面最高分标签引用

    void Awake() { 
        //最高分
        int best = PlayerPrefs.GetInt("game_best");
        ScoreText.GetComponent<Text>().text = GameController.score.ToString();
        BestText.GetComponent<Text>().text = best.ToString();
    }

	//0.分享按钮事件
    public void ShareButtonClicked() { 

    }

    //1.HOME按钮事件
    public void HomeButtonClicked()
    {
        Application.LoadLevel("Menu");
    }

    //2.排名按钮事件
    public void RankButtonClicked()
    {

    }

    //3.评论按钮事件
    public void MarkButtonClicked()
    {

    }

    //4.重新开始游戏按钮事件
    public void RestartButtonClicked()
    {
        Application.LoadLevel("Game");
    }
}
