//  
//MenuController.cs  
//  
// Created by [JiangXinhou]  
//  
// Copyright jiangxinhou@outlook.com (http://blog.csdn.net/cordova)
using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    //购买英雄面板
    public GameObject HerosPanel;
    //购买芒果面板
    public GameObject ShopPanel;

    // 0.开始游戏
    public void StartGame()
    {
		//Client.Ins.MenuBG.SetActive (false);
        Application.LoadLevel("Game");
    }

    // 1.开始新版游戏
    public void StartGameNew()
    {
		Client.Ins.MenuBG.SetActive (false);
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
        HerosPanel.SetActive(true);
		FocusableManager._needUpdate = true;
    }
	public void CloseHeroPanel(){
		HerosPanel.SetActive (false);
	}

    // 6.显示商店面板
    public void Shop()
    {
        ShopPanel.SetActive(true);
		FocusableManager._needUpdate = true;
    }
	public void CloseShop(){
		ShopPanel.SetActive (false);
	}

}
