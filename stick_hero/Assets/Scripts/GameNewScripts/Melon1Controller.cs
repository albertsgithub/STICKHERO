// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;

public class Melon1Controller : MonoBehaviour {


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
        //取出精灵渲染组件

        //SpriteRenderer SpriteRender = GetComponent<SpriteRenderer>();
       // Texture2D melon_open = (Texture2D)Resources.Load("watermelon_open");
        //计算新贴图的rect
        //Rect melon_new_rect = SpriteRender.sprite.textureRect;
        //melon_new_rect.width = melon_open.width;
        //melon_new_rect.height = melon_open.height;
        //Sprite melon_new = Sprite.Create(melon_open, SpriteRender.sprite.textureRect, new Vector2(0.5f, 0.5f));
        //SpriteRender.sprite = melon_new;


        //西瓜坠落
        GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        //GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("西瓜1坠落");
    }

}
