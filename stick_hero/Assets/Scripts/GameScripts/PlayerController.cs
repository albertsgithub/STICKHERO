// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject Stick;                    //棍子渲染物体
    public GameObject VirtualPoint;             //跟随棍子末端的虚拟的点
    private LineRenderer stickRender;           //棍子渲染组件

    private float playerInitX;                  //主角的初始横坐标，固定初始位置
    private int actionResult;                   //用户操作的结果
    private float stickLength;                  //棍子长度
    private Vector3 stickStartPoint;            //棍子起点
    private Vector3 stickEndPoint;              //棍子末端点
    private float stickGrowSpeed = 5.0f;        //棍子每次伸长的速度
    private float rotateAngle = 90.0f;          //棍子放倒时的旋转单位角度
    private float playerSpeed = 1.0f;           //主角前进速度
    private float playerW;                      //主角的宽度(参考恒量，一旦初始化不再变化)
    private float bonusArea = 0.1f;             //加分区域宽度的一半

    ///音效
    public AudioClip sfx_walk;                  //主角走路
    public AudioClip sfx_fall;                  //棍子落地
    public AudioClip sfx_stick;                 //棍子增长的声音
    public AudioClip sfx_bonus;                 //加分音效
    public AudioClip sfx_hit;                   //主角摔落的音效

    ///主角状态
    public static bool isWalking;               //是否在前进
    private bool isPulling;                     //是否在拉长棍子
    private bool isDroping;                     //是否在放倒棍子
    public static bool isBacking;               //是否在整体往后撤退


    #region 脚本生命周期
    // 初始化
    void Awake() {

        //棍子
        stickRender = Stick.GetComponent<LineRenderer>();

        //存储主角初始横坐标
        playerInitX = transform.position.x;
        //主角的宽度
        playerW = GetComponent<SpriteRenderer>().bounds.size.x;

        //状态变量
        isWalking = false;
        isPulling = false;
        isDroping = false;
        isBacking = false;
    }

	void Start () {	
	}

	// 按帧更新
	void Update () {
        //检测输入
        if (GameController.gameover || !GameController.canStartTheGame)
            return;

        //鼠标左键按下或者屏幕触摸
        if(Input.GetMouseButtonDown(0)){
            //初始化棍子
            InitStickRender();
            isPulling = true;
        }

        //松开
        if (Input.GetMouseButtonUp(0)) {
            //停止交互
            GameController.canStartTheGame = false;
            //计算动作结果
            CalActionResult();
            //迅速将虚拟跟随点设置到棍子末端
            VirtualPoint.transform.position = stickEndPoint;

            isPulling = false;
            isDroping = true;
        }

    }

    //延迟刷新
    void LateUpdate() { 
        //伸长棍子
        if (isPulling) {
            StartCoroutine(PullStick());
        }

        //缩短棍子
        if (isDroping) {
            StartCoroutine(DropStick());
        }

        //主角前进
        if (isWalking) {
            StartCoroutine(PlayerWalking());
        }

        //检测主角退回到开始位置
        if(isBacking && transform.position.x <= playerInitX){
            //停止后退
            isBacking = false;
            //开启交互
            GameController.canStartTheGame = true;
        }

        //检测主角坠亡游戏结束
        if (transform.position.y < -7.0f) {
            //游戏结束
            GameController.gameover = true;
            //播放坠落音效
            PlaySfx(sfx_hit);
            //延迟一会儿销毁主角
            StartCoroutine(DelayDestroyPlayer());
        }

    }

    #endregion

    #region 自定义函数

    //重新初始化棍子
    void InitStickRender() {

        //棍子起始点初始化
        stickRender.enabled = true;
        stickStartPoint = GameController.CurrentPlatform.transform.position + new Vector3(GameController.currentPlatW / 2, GameController.platFormH / 2, 0);
        stickEndPoint = stickStartPoint;
        stickRender.SetPosition(0, stickStartPoint);
        stickRender.SetPosition(1, stickEndPoint);
        
        stickLength = 0;
    }

    //伸长棍子
    IEnumerator PullStick() {
        //棍子状态变化
        stickLength += stickGrowSpeed * Time.deltaTime;
        stickEndPoint += new Vector3(0, stickGrowSpeed * Time.deltaTime, 0);
        stickRender.SetPosition(1, stickEndPoint);
        //棍子增长声音
        PlaySfx(sfx_stick);
        //增长延迟
        yield return 0;
    }

    //放倒棍子
    IEnumerator DropStick() {
        //绕起点旋转
        VirtualPoint.transform.RotateAround(stickStartPoint, new Vector3(0, 0, 1), -Time.deltaTime*rotateAngle*3);
        //速度加快
        rotateAngle += 3;
        //棍子末端跟随虚拟点旋转
        stickRender.SetPosition(1, VirtualPoint.transform.position);

        //检测旋转结束
        if(VirtualPoint.transform.position.y <= stickStartPoint.y){
            //停止旋转
            isDroping = false;
            //精确矫正棍子末端
            stickRender.SetPosition(1,stickStartPoint + new Vector3(stickLength, 0 ,0));
            //旋转速度恢复
            rotateAngle = 90.0f;

            //如果动作成功但不加分
            if (actionResult == 1) { 
                //播放棍子落地音效
                PlaySfx(sfx_hit);

            }
            //如果落到加分区域
            else if (actionResult == 2) { 
                //播放额外加分音效
                PlaySfx(sfx_bonus);
                //额外加一分
                ++GameController.score;
            }

            //等待一会儿通知主角可以开始前进
            yield return new WaitForSeconds(0.3f);
            isWalking = true;
        }

        yield return 0;
    }

    //主角前进
    IEnumerator PlayerWalking() { 
        //主角移动
        transform.Translate(Vector3.right*playerSpeed*Time.deltaTime*3);
        //播放脚步声
        PlaySfx(sfx_walk);

        //检测主角移动到目的地：
        // 1.如果动作不成功则主角走到棍子末端后坠落
        if (actionResult == 0)
        {
            if (transform.position.x >= stickStartPoint.x + stickLength)
            {
                isWalking = false;
                //主角坠落
                GetComponent<Rigidbody>().useGravity = true;
            }
        }
        //如果动作成功则走到新平台右边
        else 
        {
            if (transform.position.x >= GameController.NewPlatform.transform.position.x + GameController.newPlatW/2 - playerW) 
            {
                isWalking = false;
                //播放动作成功音效
                //PlaySfx(sfx_hit);
                //正常得分
                ++GameController.score;
                //将主角绑定到新平台上跟随平台往后移动
                transform.parent = GameController.NewPlatform.transform;
                //隐藏棍子
                stickRender.enabled = false;
                //创建新平台
                GameController.canCreatePlatform = true;
                //等待新平台创建完成
                yield return new WaitForSeconds(0.1f);
                //所有平台完往后移动
                isBacking = true;
            }
        }

        yield return 0;
    }

    //计算动作结果
    void CalActionResult() {
        //下一个平台的横坐标
        float newPlatX = GameController.NewPlatform.transform.position.x;
        //棍子末端很坐标
        float stickEndX = stickStartPoint.x+stickLength;
        //打到新平台之外
        if (stickEndX < (newPlatX - GameController.newPlatW / 2) || stickEndX > (newPlatX + GameController.newPlatW / 2)){
            actionResult = 0;
        }
        //动作成功但不加分
        else if (stickEndX < (newPlatX - bonusArea) || stickEndX > (newPlatX + bonusArea)){
            actionResult = 1;
        }
        //动作成功并额外加一分
        else {
            actionResult = 2;
        }
    }

    //延迟销毁主角
    public IEnumerator DelayDestroyPlayer() {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
        yield return 0;
    }
    
    //播放声音片段
    public void PlaySfx(AudioClip sfx) {
        GetComponent<AudioSource>().clip = sfx;
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }

    #endregion
}
