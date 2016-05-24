//  
//GameOverController.cs  
//  
// Created by [JiangXinhou]  
//  
// Copyright jiangxinhou@outlook.com (http://blog.csdn.net/cordova)
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : Singleton<GameOverController> {

    public Text ScoreText;              //游戏结束界面分数标签引用
    public Text BestText;              //游戏结束界面最高分标签引用

	void Start(){
		//最高分
		int best,score;
		if (Client.currentGame == 1) {
			best = PlayerPrefs.GetInt("game_best");
			score = GameController.score;
		} else {
			score = GameNewController.score;
			best = PlayerPrefs.GetInt("gamenew_best");
		}
		BestText.text = best.ToString();
		ScoreText.text = score.ToString ();
	}

	void Enable(){
	}

	//0.分享按钮事件
	public void ShareButtonClicked()
	{

	}

	//1.HOME按钮事件
	public void HomeButtonClicked()
	{
		Client.Ins.GameOverPanel.SetActive (false);
		Client.Ins.MenuBG.SetActive (true);
		Client.Ins.ScoreText.SetActive (false);

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
		Client.Ins.GameOverPanel.SetActive(false);
		//重新加载当前scene
		if(Client.currentGame == 1)
			Application.LoadLevel("Game");
		else
			Application.LoadLevel("GameNew");
	}

}
