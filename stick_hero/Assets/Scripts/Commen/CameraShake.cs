//  
//CameraShake.cs  
//  
// Created by [JiangXinhou]  
//  
// Copyright jiangxinhou@outlook.com (http://blog.csdn.net/cordova)
using System;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originPosition;          //原位置
    private float shake_decay;               //摇动幅度
    private float shake_scale;               //衰减幅度

    //变量初始化
    private void Awake()
    {
        originPosition = transform.position;
        shake_scale = 0.1f;
        shake_decay = 0.005f;
    }


    private void Update()
    {
		if ( Client.currentGameOver && (this.shake_scale > 0f))
        {
            //停止背景音乐
            GetComponent<AudioSource>().Stop();
            //震动
            transform.position = originPosition + ((Vector3)(UnityEngine.Random.insideUnitSphere * shake_scale));
            //幅度衰减
            shake_scale -= shake_decay;

            //摇晃幅度小于零
			if (shake_scale < 0)
				Client.currentGameOver = false;
        }
    }

}
