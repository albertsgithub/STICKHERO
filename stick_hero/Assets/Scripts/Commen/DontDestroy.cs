//  
//NewBehaviourScript.cs  
//  
// Created by [JiangXinhou]  
//  
// Copyright jiangxinhou@outlook.com (http://blog.csdn.net/cordova)
using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {
	void Awake(){
		GameObject.DontDestroyOnLoad (this.gameObject);
	}
}
