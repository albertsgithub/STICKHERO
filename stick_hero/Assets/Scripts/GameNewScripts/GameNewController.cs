// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameNewController : MonoBehaviour {

    public GameObject Player;                             //主角引用
    [System.NonSerialized]
    public static GameObject CurrentPlatform;             //当前平台
    [System.NonSerialized]
    public static GameObject NewPlatform;                 //当前新平台
    public GameObject GameoverPlanel;                     //游戏结束面板
    public GameObject PlatformPrefab;                     //平台预设模板
    public GameObject Melon1Prefab;                       //西瓜1预设模板
    public GameObject Melon2Prefab;                       //西瓜2预设模板
    private GameObject Melon1;                            //西瓜1
    private GameObject Melon2;                            //西瓜2
    public GameObject ScoreText;                          //游戏分数显示标签

    public static bool gameover;                          //游戏结束标志
    public static bool canCreatePlatform;                 //允许创建平台
    public static bool canStartTheGame;                   //允许游戏交互

    private Vector3 platInitPosition;                     //初始平台的位置（恒量，作为参考点保持不变）
    private float minDistance = 2.0f;                     //新平台离当前平台的最近距离
    private float maxDistance = 3.0f;                     //新平台离当前平台的最远距离
    private float minHeight = -1.0f;                      //新平台的最小相对高度
    private float maxHeight = 1.0f;                       //新平台的最大相对高度
    private float targetDistance;                         //新平台目标距离
    private float targetHeight;                           //新平台目标相对高度
    private Vector3 newPlatPosition;                      //新平台位置
    private Vector3 nearstPoint;                          //新平台上离棍子起点最近的点
    Vector3 stickStartPosition;                           //棍子的起点位置

    public static float platFormW;                        //平台的宽度(常量)
    public static float platFormH;                        //平台的高度（常量）

    public static int platformCreated;                    //已经创建平台个数
    public static int score;                              //当前游戏分数
    private float stickLengthAdd = 1.0f;                  //棍子最大长度增大矫正
    public static float successLength;                    //动作成功的最大棍子长度（棍子不能打到平台） 
    
    /*西瓜参数*/
    private float originalR;                              //西瓜模板本来的半径
    private float minRScale = 0.3f;                       //西瓜的最小半径缩放
    private float maxRScale = 0.8f;                       //西瓜的最大半径缩放
    private float firstRScale;                            //第一个西瓜的半径缩放值
    private float secondRScale;                           //第二个西瓜的半径缩放值
    public static float firstR;                           //第一个西瓜的半径
    public static float secondR;                          //第二个西瓜的半径
    private Vector3 firstPosition;                        //第一个西瓜的位置
    private Vector3 secondPosition;                       //第二个西瓜的位置
    public static int melonNum;                           //西瓜的个数

    #region 脚本生命周期
    // 初始化
    void Awake() {

        //游戏帧率设置
        Application.targetFrameRate = 60;
        //初始化当前平台
        CurrentPlatform = GameObject.FindGameObjectWithTag("gamenew_platform_start");
        //平台初始位置
        platInitPosition = CurrentPlatform.transform.position;
        //平台宽度
        platFormW = CurrentPlatform.GetComponent<SpriteRenderer>().bounds.size.x;
        //平台高度
        platFormH = CurrentPlatform.GetComponent<SpriteRenderer>().bounds.size.y;

        //变量初始值
        gameover = false;
        canCreatePlatform = false;
        platformCreated = 0;
        score = 0;
        canStartTheGame = false;

        //初始化平台目标参数
        targetDistance = Random.Range(minDistance, maxDistance);
        targetHeight = Random.Range(minHeight, maxHeight);

        //西瓜变量初始化
        originalR = Melon1Prefab.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        firstR = 0;
        secondR = 0;
        firstPosition = Vector3.zero;
        secondPosition = Vector3.zero;
    }

	void Start () {

        //创建第一个新平台
        //创建一个西瓜
        melonNum = 1;
        //创建平台
        CreatePlatform();
        //随机创建西瓜
        CreateRandomMelon();

        //开始交互
        canStartTheGame = true;
	}

	// 按帧更新
	void Update () {

        if (!gameover)
        {
            //更新显示当前分数
            ScoreText.GetComponent<Text>().text = score.ToString();
        }

        //新平台产生源
        if (canCreatePlatform)
        {
            //及时停止创建
            canCreatePlatform = false;
            //当前平台更新
            CurrentPlatform = NewPlatform;
            //生成随机新平台距离
            targetDistance = Random.Range(minDistance, maxDistance);
            //生成随机新平台相对高度
            targetHeight = Random.Range(minHeight, maxHeight);

            //开始创建平台
            //随机生成一个或者两个西瓜
            if (Random.Range(0, 10) > 3)
                melonNum = 2;
            else
                melonNum = 1;

            //创建平台
            CreatePlatform();
            //随机创建西瓜
            CreateRandomMelon();
            //所有平台完往后移动
            PlayerNewController.isBacking = true;

        }
	}

    void LateUpdate()
    {
        //检测游戏结束
        if (gameover)
        {
            StartCoroutine(processGameover());
        }
    }

    #endregion


    #region 自定义函数

    //创建一个新平台
    public void CreatePlatform()
    {
        //新平台位置
        newPlatPosition = CurrentPlatform.transform.position + new Vector3(targetDistance + platFormW, 0, 0);
        newPlatPosition.y = platInitPosition.y + targetHeight;
        
        /***计算棍子最大长度***/
        //主角位置
        Vector3 playerPos = Player.transform.position;
        //棍子起点位置
        stickStartPosition = playerPos + new Vector3(PlayerNewController.playerW / 2, 0, 0);
        //新平台左上角位置
        Vector3 leftTop = newPlatPosition + new Vector3(-platFormW / 2, platFormH / 2, 0);
        //如果新平台比棍子起点低则棍子的最短长度为：起点到平台左上角的距离
        if ((newPlatPosition.y + platFormH / 2) < playerPos.y)
        {
            //最近的点为左上角
            nearstPoint = leftTop;
            //动作成功棍子的最大长度
            successLength = Mathf.Sqrt(Mathf.Pow(nearstPoint.x - stickStartPosition.x, 2.0f) + Mathf.Pow(nearstPoint.y - stickStartPosition.y, 2.0f));
            //棍子伸缩的最大长度
            PlayerNewController.stickMaxLength = successLength + stickLengthAdd;
        }
        //否则最短的最大棍子长度为起点到新平台的距离
        else {
            //最近的点为棍子起点到新平台水平的第一个交点
            nearstPoint = stickStartPosition;
            nearstPoint.x = leftTop.x;
            //动作成功棍子的最大长度
            successLength = nearstPoint.x - playerPos.x;
            //棍子伸缩的最大长度
            PlayerNewController.stickMaxLength = successLength + stickLengthAdd;
        }

        //克隆一个平台prefab
        NewPlatform = Instantiate(PlatformPrefab, newPlatPosition, Quaternion.identity) as GameObject;
        //新平台宽度缩放
        NewPlatform.transform.localScale = new Vector3(0.2f, 3, 1);
    }

    //随机搜索一个或者两个西瓜的位置点和西瓜随机半径
    public void CreateRandomMelon(){

        //1.搜索第一个点:
        //第一个西瓜的半径
        firstRScale = Random.Range(minRScale, maxRScale);
        firstR = originalR * firstRScale;
        //可行域圆轨迹半径
        float firstCircleR = successLength;// +firstR;
        //可行域圆轨迹垂径的一半,也是随机y坐标相对于棍子起点的浮动范围
        float firstYRange = Mathf.Sqrt(firstCircleR * firstCircleR - 4*firstR*firstR);
        //生成随机y坐标
        firstPosition.y = stickStartPosition.y + Random.Range(-firstYRange,firstYRange);
        //根据y坐标求出可行圆轨迹上的x坐标值(这个x只是最终随机点x坐标的随机范围的最大值)：（x-a）^2 + (y-b)^2 = r^2
        float maxRandomX = Mathf.Sqrt(firstCircleR * firstCircleR - Mathf.Pow((firstPosition.y - stickStartPosition.y), 2.0f)) + stickStartPosition.x;
        //x坐标的随机浮动范围
        float firstXRange = maxRandomX - stickStartPosition.x - firstR * 2;
        //最终随机横坐标
        firstPosition.x = stickStartPosition.x + firstR * 2 + Random.Range(0, firstXRange);
        //创建第一个西瓜
        Melon1 = Instantiate(Melon1Prefab, firstPosition, Quaternion.identity) as GameObject;
        Melon1.transform.localScale = new Vector3(firstRScale, firstRScale, 1);

        //2.如果两个西瓜则搜索第二个点
        if(melonNum == 2){
            //先不考虑和第一个西瓜重合的情况
            //第二个西瓜的半径
            secondRScale = Random.Range(minRScale,maxRScale);
            secondR = originalR * secondRScale;
            //可行域圆轨迹半径
            float secondCircleR = successLength;// +secondR;
            //可行域圆轨迹垂径的一半,也是随机y坐标相对于棍子起点的浮动范围
            float secondYRange = Mathf.Sqrt(secondCircleR * secondCircleR - 4 * secondR * secondR);
            //生成随机y坐标
            secondPosition.y = stickStartPosition.y + Random.Range(-secondYRange, secondYRange);
            //根据y坐标求出可行圆轨迹上的x坐标值(这个x只是最终随机点x坐标的随机范围的最大值)：（x-a）^2 + (y-b)^2 = r^2
            float maxRandom2X = Mathf.Sqrt(secondCircleR * secondCircleR - Mathf.Pow((secondPosition.y - stickStartPosition.y), 2.0f)) + stickStartPosition.x;
            //x坐标的随机浮动范围
            float firstX2Range = maxRandom2X - stickStartPosition.x - secondR * 2;
            //最终随机横坐标
            secondPosition.x = stickStartPosition.x + secondR * 2 + Random.Range(0, firstX2Range);
            //创建第二个西瓜
            Melon2 = Instantiate(Melon2Prefab, secondPosition, Quaternion.identity) as GameObject;
            Melon2.transform.localScale = new Vector3(secondRScale, secondRScale, 1);

        }
    }

    //游戏结束处理
    private IEnumerator processGameover()
    {
        //结算最高成绩：
        int best = PlayerPrefs.GetInt("gamenew_best");
        if (score > best) {
            best = score;
            PlayerPrefs.SetInt("gamenew_best", best);
        }

        //等待震动动画结束
        yield return new WaitForSeconds(1.0f);
        // 显示gameover面板
        GameoverPlanel.SetActive(true);
        yield return 0;
    }

    #endregion

}
