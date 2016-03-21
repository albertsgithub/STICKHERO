// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;

public class Melon2Controller : MonoBehaviour {

    public AudioClip sfx_melonbroken;               //西瓜破碎音效

	// 按帧更新
	void Update () {
        
        //落出屏幕销毁
        if (transform.position.y < -8.0f)
            Destroy(gameObject);
	}

    //碰撞触发器
    void OnTriggerEnter2D(Collider2D other)
    {
        //如果不是棍子碰撞则忽略，防止西瓜碰撞的情况
        if (other.gameObject.name != "Stick")
            return;
        //播放西瓜破碎音效
        PlaySfx(sfx_melonbroken);
        //播放西瓜破碎动画
        //... ...

        //西瓜坠落
        GetComponent<Rigidbody2D>().gravityScale = 1.0f;

        //告诉游戏控制器打掉一个西瓜
        GameNewController.melonNum -= 1;
    }

    //播放声音片段
    public void PlaySfx(AudioClip sfx)
    {
        GetComponent<AudioSource>().clip = sfx;
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }

}
