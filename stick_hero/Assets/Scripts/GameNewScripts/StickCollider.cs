// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;

public class StickCollider : MonoBehaviour {

    public AudioClip sfx_hitplatform;               //碰撞音效

    //虚拟点的引用，需要使用它的旋转参数
    public GameObject virtualPoint;
    //主角的引用，需要再合适的时候让之跌落
    public GameObject Player;
    //棍子宽度
    private float lineWidth = 0.05f;
    //棍子的boxcollider2d组件
    BoxCollider2D stickCollider;

	//初始化
	void Awake () {
        //获取棍子的collider2d组件
        stickCollider = GetComponent<BoxCollider2D>();
	}

	//开始执行
	void Start () {	
	}

	// 按帧更新
	void Update () {
        //棍子的起始点
        Vector3 startPos = PlayerNewController.stickStartPoint;
        Vector3 endPos = PlayerNewController.stickEndPoint;
        //碰撞器的尺寸
        float height = Mathf.Sqrt(Mathf.Pow(startPos.x - endPos.x, 2.0f) + Mathf.Pow(startPos.y - endPos.y, 2.0f));
        float width = 0.05f;
        Vector2 size = new Vector2(width,height);

        //更新棍子的collider:
        //棍子碰撞器的位置
        stickCollider.transform.position = startPos + (endPos - startPos) / 2;
        //碰撞器的方向
        stickCollider.transform.rotation = virtualPoint.transform.rotation;
        //碰撞器的尺寸
        stickCollider.size = size;
	}

    //碰撞检测
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "platform_new(Clone)" && other.gameObject == GameNewController.NewPlatform)
        { 
            //播放棍子碰撞平台音效
            PlaySfx(sfx_hitplatform);
            //停止主角和棍子的旋转
            PlayerNewController.isDroping = false;
            //主角跌落游戏结束
            Player.GetComponent<Rigidbody>().useGravity = true;
            GameNewController.gameover = true;
        }
    }

    //播放声音片段
    public void PlaySfx(AudioClip sfx)
    {
        GetComponent<AudioSource>().clip = sfx;
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }
}
