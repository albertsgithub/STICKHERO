// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : Singleton<GameOverController> {

    public Text ScoreText;              //游戏结束界面分数标签引用
    public Text BestText;              //游戏结束界面最高分标签引用

	void Start(){
		gameObject.transform.localScale = Vector3.zero;

		//最高分
		int best;
		if (Application.loadedLevel == 2) {
			best = PlayerPrefs.GetInt("game_best");
			ScoreText.text = GameController.score.ToString ();
		} else {
			ScoreText.text = GameNewController.score.ToString ();
			best = PlayerPrefs.GetInt("gamenew_best");
		}

		BestText.text = best.ToString();
	}

	//0.分享按钮事件
	public void ShareButtonClicked()
	{

	}

	//1.HOME按钮事件
	public void HomeButtonClicked()
	{
		GameObject MenuBG = GameObject.FindWithTag ("MenuBG");

		MenuBG.transform.localScale = Vector3.one;
		gameObject.transform.localScale = Vector3.zero;

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
		gameObject.transform.localScale = Vector3.zero;
		//重新加载当前scene
		Application.LoadLevel(Application.loadedLevel);
	}

}
