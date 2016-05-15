//  
//Singleton.cs  
//  
// Created by [JiangXinhou]  
//  
// Copyright jiangxinhou@outlook.com (http://blog.csdn.net/cordova)
using UnityEngine;
using System.Collections;

/*
 * 泛型单例类，只允许有一个实例
 */
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	//静态单一实例
	public static T _instance;

	//获取实例的接口
	public static T Ins
	{
		get
		{
			return _instance;
		}
	}

	//初始化单例
	public virtual void Awake()
	{
		_instance = (T)this;
	}

	//销毁实例
	public virtual void OnDestroy()
	{
		_instance = null;
	}
}