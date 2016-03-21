// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;

public class GameOverButtonFunctions : MonoBehaviour {

    //0.分享按钮事件
    public void ShareButtonClicked()
    {

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
        //重新加载当前scene
        Application.LoadLevel(Application.loadedLevel);
    }
}
