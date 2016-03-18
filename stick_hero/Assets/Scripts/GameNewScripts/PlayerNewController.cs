// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;

public class PlayerNewController : MonoBehaviour {

    public GameObject Stick;                            //游戏界面棍子物体的引用
    private LineRenderer StickRender;                   //棍子物体的渲染组件
    public GameObject VirtualPoint;                     //跟随棍子末端的虚拟的点
    private Vector3 virtualStartPoint;                  //跟随点的旋转起点

    private Vector3 playerInitPos;                      //主角的初始位置，作为参考点一旦初始化保持不变
    private Vector3 targetPosition;                     //主角要跳到的目的位置（动态变化）
    private Vector3 stickStartPoint;                    //棍子起点
    private Vector3 stickEndPoint;                      //棍子末点
    private float stickLength;                          //棍子长度
    public static float stickMaxLength;                 //棍子的最大长度（根据新平台的位置动态变化）
    private float stickUnit = 2.0f;                     //棍子伸缩速度
    private float rotateAngle = 180.0f;                 //棍子放倒时的旋转角速度(每次都会变小)
    private float rotateSpeed = 180.0f;                 //棍子放倒时的旋转角速度(恒量)
    private float stickRotation;                        //棍子已经旋转的角度

    public static float playerW;                        //主角宽度
    public static float playerH;                        //主角高度

    ///主角状态
    public static bool isJumping;                       //是否在跳跃
    private bool isPulling;                             //是否在拉长棍子
    private bool isShrinking;                           //是否在缩短棍子
    private bool isDroping;                             //是否在放倒棍子
    public static bool isBacking;                       //是否在整体往后撤退

    ///音效
    public AudioClip sfx_jump;                          //主角走路
    public AudioClip sfx_drop;                          //棍子挥舞音效
    public AudioClip sfx_fall;                          //棍子落地
    public AudioClip sfx_stick;                         //棍子增长的声音
    public AudioClip sfx_bonus;                         //加分音效
    public AudioClip sfx_hit;                           //主角摔落的音效

    #region 函数生命周期
    // 初始化
    void Awake() {

        //主角初始位置
        playerInitPos = transform.position;
        //主角尺寸
        playerH = GetComponent<SpriteRenderer>().bounds.size.y;
        playerW = GetComponent<SpriteRenderer>().bounds.size.x;

        //棍子初始化
        StickRender = Stick.GetComponent<LineRenderer>();
        InitStickRender();

        //初始化状态
        isBacking = false;
        isDroping = false;
        isJumping = false;
        isShrinking = false;
        isPulling = true;

        stickRotation = 0;

    }
	void Start () {
	}

	// 按帧更新
	void Update () {

        //检测输入
        if (!GameNewController.canStartTheGame || GameNewController.gameover)
            return;
        //鼠标左键按下或者屏幕触摸放倒棍子并停止交互
        if(Input.GetMouseButtonDown(0)){
            //停止交互
            GameNewController.canStartTheGame = false;
            //播放挥舞棍子的音效
            //... ...

            //更新状态
            isPulling = false;
            isShrinking = false;
            isDroping = true;
            //迅速将虚拟点设置到棍子末端
            virtualStartPoint = stickEndPoint;
            VirtualPoint.transform.position = virtualStartPoint;
            //开启棍子的碰撞检测
            //... ...
        }
    }

    //延迟更新
    void LateUpdate() {

        //伸缩棍子
        if (isPulling || isShrinking)
        {
            StartCoroutine(PullStick());
        }

        //整体旋转
        if (isDroping) {
            StartCoroutine(DropStick());
        }

        //主角跳到指定位置
        if (isJumping)
        {
            StartCoroutine(PlayerJumpTo(targetPosition));
        }

        //检测主角退回到开始位置
        if (isBacking && transform.position.x <= playerInitPos.x)
        {
            //停止后退
            isBacking = false;
            //开启交互
            GameNewController.canStartTheGame = true;
            //初始化棍子
            InitStickRender();
            //开始伸缩棍子
            isPulling = true;
        }

        //检测主角坠亡游戏结束
        if (transform.position.y < -7.0f)
        {
            GameNewController.gameover = true;
            Destroy(gameObject);
        }

    }

    #endregion

    #region 自定义函数

    //重新初始化棍子
    void InitStickRender()
    {

        //棍子起始点初始化
        StickRender.enabled = true;
        stickStartPoint = transform.position + new Vector3(playerW / 2, 0, 0);
        stickEndPoint = stickStartPoint;
        StickRender.SetPosition(0, stickStartPoint);
        StickRender.SetPosition(1, stickEndPoint);

        stickLength = 0;
    }

    //伸缩棍子
    IEnumerator PullStick()
    {
        //棍子状态变化
        if (isPulling)
        {
            stickLength += stickUnit*Time.deltaTime;
            stickEndPoint += new Vector3(0, stickUnit * Time.deltaTime, 0);
            StickRender.SetPosition(1, stickEndPoint);
            //检测伸长到最大长度
            if (stickLength >= stickMaxLength)
            {
                isShrinking = true;
                isPulling = false;
            }
        }
        else 
        {
            stickLength -= stickUnit * Time.deltaTime;
            stickEndPoint -= new Vector3(0, stickUnit * Time.deltaTime, 0);
            StickRender.SetPosition(1, stickEndPoint);
            //检测缩短到0
            if(stickLength <= 0){
                isShrinking = false;
                isPulling = true;
            }
        }
        yield return 0;
    }

    //放倒棍子
    IEnumerator DropStick()
    {
        //虚拟点绕起点旋转
        //单次旋转角度
        float rotateUnit = Time.deltaTime * rotateAngle * 3;
        stickRotation += rotateUnit;
        VirtualPoint.transform.RotateAround(stickStartPoint, new Vector3(0, 0, 1), -rotateUnit);
        //主角旋转
        transform.RotateAround(transform.position + new Vector3(0, -playerH / 2, 0), new Vector3(0, 0, 1), -Time.deltaTime * rotateAngle * 1f);
        //速度减慢
        rotateAngle -= 0.5f;
        //棍子末端跟随虚拟点旋转
        StickRender.SetPosition(1, VirtualPoint.transform.position);

        //检测旋转结束
        if (stickRotation > 160) {
            //角度计数器归零
            stickRotation = 0;
            //旋转速度恢复
            rotateAngle = rotateSpeed;
            //停止旋转
            isDroping = false;
            //隐藏棍子
            StickRender.enabled = false;

            //根据动作结果判断主角掉落还是继续跳跃到下一个平台:
            if (stickLength > GameNewController.successLength)
            {
                //主角跌落
                GetComponent<Rigidbody>().useGravity = true;
                //旋转
                transform.RotateAround(transform.position, new Vector3(0, 0, 1), -Time.deltaTime * rotateAngle * 3);
            }
            //动作成功跳到下一个平台
            else {
                //跳跃目的点
                Vector3 targetJumpPos = GameNewController.NewPlatform.transform.position + new Vector3(0, GameNewController.platFormH / 2 + playerH / 2, 0);
                StartCoroutine(PlayerJumpTo(targetJumpPos));
            }
        }
        yield return 0;
    }

    //主角跳到指定位置
    IEnumerator PlayerJumpTo(Vector3 targetPos) {
        //跳到下一个平台上
        transform.position = targetPos;
        //去掉旋转
        transform.rotation = Quaternion.identity;
        //正常得分
        ++GameNewController.score;
        //创建新平台
        GameNewController.canCreatePlatform = true;
        //等待平台创建完成
        yield return new WaitForSeconds(0.2f);
        //所有平台完往后移动
        isBacking = true;
        yield return 0;
    }

    #endregion
}
