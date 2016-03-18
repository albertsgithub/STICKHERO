// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;

public class HeroClickFunctions : MonoBehaviour {

    //英雄面板引用
    public GameObject HeroPanel;

	// 初始化
	void Awake () {	
	}

	//开始执行
	void Start () {	
	}

	// 按帧更新
	void Update () {
	}


    ////////////////////////
    //关闭英雄面板
    public void ClickedCloseButton() {
        HeroPanel.SetActive(false);
    }

}
