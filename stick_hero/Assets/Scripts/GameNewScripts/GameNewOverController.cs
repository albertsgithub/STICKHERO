//  
//GameNewOverController.cs  
//  
// Created by [JiangXinhou]  
//  
// Copyright jiangxinhou@outlook.com (http://blog.csdn.net/cordova)
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
        ScoreTextNew.GetComponent<Text>().text = GameNewController.Ins.score.ToString();
        BestTextNew.GetComponent<Text>().text = best.ToString();
    }
}
