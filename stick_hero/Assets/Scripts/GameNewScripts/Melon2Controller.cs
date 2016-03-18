// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;

public class Melon2Controller : MonoBehaviour {

	// 按帧更新
	void Update () {
        
        //落出屏幕销毁
        if (transform.position.y < -8.0f)
            Destroy(gameObject);
	}

    //碰撞触发器
    void OnTriggerEnter2D(Collider2D other)
    {
        //播放西瓜破碎音效
        //... ...
        //播放西瓜破碎动画
        //... ...

        //西瓜坠落
        GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        //GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("西瓜2坠落");
    }

}
