//  
//Client.cs  
//  
// Created by [JiangXinhou]  
//  
// Copyright jiangxinhou@outlook.com (http://blog.csdn.net/cordova)
using UnityEngine;
using System.Collections;

public class Client : Singleton<Client> {

	public GameObject ScoreText;
	public GameObject GameOverPanel;
	public GameObject MenuBG;

	public static bool currentGameOver;
	public static int currentGame;

	void Start(){
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		currentGameOver = false;
		currentGame = 1;
	}

}
