// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameNewOverController : MonoBehaviour {

    public GameObject ScoreTextNew;              //游戏结束界面分数标签引用
    public GameObject BestTextNew;              //游戏结束界面最高分标签引用

    void Awake()
    {
        //最高分
        int best = PlayerPrefs.GetInt("gamenew_best");
        ScoreTextNew.GetComponent<Text>().text = GameNewController.score.ToString();
        BestTextNew.GetComponent<Text>().text = best.ToString();
    }
}
