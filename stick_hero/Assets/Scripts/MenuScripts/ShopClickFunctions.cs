// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;

public class ShopClickFunctions : MonoBehaviour {

    //商店面板引用
    public GameObject ShopPanel;

	// 初始化
	void Awake () {
	}

	//开始执行
	void Start () {	
	}

	// 按帧更新
	void Update () {
	}

    /////////////////////////
    public void Clicked8YButton() { 
    }

    public void Clicked12YButton()
    {
    }

    public void Clicked30YButton()
    {
    }

    public void Clicked45YButton()
    {
    }

    //关闭面板按钮
    public void ClickedCloseButton()
    {
        ShopPanel.SetActive(false);
    }

    public void ClickedInAppButton()
    {
    }

}
