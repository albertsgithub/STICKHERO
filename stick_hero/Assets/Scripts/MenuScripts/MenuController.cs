//
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    //购买英雄面板
    public GameObject HerosPanel;
    //购买芒果面板
    public GameObject ShopPanel;
	//
	//public GameObject MenuPanel;

    //声音设置按钮的子图片
    //public GameObject soundsImage;

    // 0.开始游戏
    public void StartGame()
    {
		//MenuPanel.SetActive (false);
        Application.LoadLevel("Game");
    }

    // 1.开始新版游戏
    public void StartGameNew()
    {
		//MenuPanel.SetActive (false);
        Application.LoadLevel("GameNew");
    }

    // 2.游戏引导
    public void Help()
    {
    }

    // 3.游戏声音设置
    public void SoundSet() {
        //切换声音按钮的图片
        //... ...
        //切换声音模式
        //... ...
    }

    // 4.去广告设置
    public void NoADs() {
    }

    // 5.显示选择英雄面板
    public void HeroSelect()
    {
        this.HerosPanel.SetActive(true);
    }

    // 6.显示商店面板
    public void Shop()
    {
        this.ShopPanel.SetActive(true);
    }

}
