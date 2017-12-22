//  
//GameController.cs  
//  
// Created by [JiangXinhou]  
//  
// Copyright jiangxinhou@outlook.com (http://blog.csdn.net/cordova)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [System.NonSerialized]
    public static GameObject CurrentPlatform;             //当前平台
    [System.NonSerialized]
    public static GameObject NewPlatform;                 //当前新平台
    public GameObject PlatformPrefab;                     //平台预设模板

    public static bool gameover;                          //游戏结束标志
    public static bool canCreatePlatform;                 //允许创建平台
	public static bool canStartTheGame = false;                   //允许游戏交互

    private float minDistance = 1.0f;                     //新平台离当前平台的最近距离
    private float maxDistance = 3.0f;                     //新平台离当前平台的最远距离
    private float minScale = 0.5f;                        //新平台的最小宽度缩放
    private float maxScale = 2.0f;                        //新平台的最大宽度缩放
    private float targetDistance;                         //新平台目标距离
    private float targetScale;                            //新平台目标缩放值

    public static float currentPlatW;                     //当前平台的宽度
    public static float newPlatW;                         //新平台的宽度
    public static float platFormH;                        //平台高度
    
    public static int platformCreated;                    //已经创建平台个数
    public  int score;                              //当前游戏分数
    
	void Start()
    {
		Client.Ins.MenuBG.SetActive(false);
		Client.Ins.ScoreText.GetComponent<Text>().text = "0";
		Client.Ins.ScoreText.SetActive (true);

		//初始化当前平台
		CurrentPlatform = GameObject.FindGameObjectWithTag("game_platform_start");
		//平台宽度
		currentPlatW = CurrentPlatform.GetComponent<MeshRenderer>().bounds.size.x;
		//平台高度
		platFormH = CurrentPlatform.GetComponent<MeshRenderer>().bounds.size.y;

		//变量初始值
		gameover = false;
		canCreatePlatform = false;
		platformCreated = 0;
		score = 0;

		//初始化平台目标参数
		targetDistance = minDistance;
		targetScale = maxScale;

        //创建第一个新平台
        StartCoroutine(CreatePlatform());
        
        //开始交互
        canStartTheGame = true;
    }

    /// <summary>
    /// 刷新
    /// </summary>
    private void Update()
    {
        if (!gameover)
        {
            //更新显示当前分数
			Client.Ins.ScoreText.GetComponent<Text>().text = score.ToString();
        }

        //新平台产生源
        if (canCreatePlatform)
        {
            //当前平台更新
            CurrentPlatform = NewPlatform;
            //当前平台宽度更新
            currentPlatW = CurrentPlatform.GetComponent<MeshRenderer>().bounds.size.x;

            //生成随机新平台距离
            targetDistance = Random.Range(minDistance, maxDistance);
            //生成随机新平台宽度缩放值
            targetScale = Random.Range(minScale, maxScale);
            //开始创建
            StartCoroutine(CreatePlatform());

            //递增游戏难度：
            //减小宽度上限
            if (maxScale > (2f * minScale))
            {
                maxScale -= 0.01f;
            }
            //增大距离下限
            if (minDistance < (maxDistance / 2f))
            {
                minDistance += 0.02f;
            }

        }
    }

    void LateUpdate()
    {
        //检测游戏结束
        if (gameover)
        {
			gameover = false;
			Client.currentGameOver = true;
            StartCoroutine(processGameover());
        }
    }

    //创建一个新平台
    public IEnumerator CreatePlatform()
    {
        
        //新平台位置
        Vector3 NewPlatformPos = CurrentPlatform.transform.position + new Vector3(currentPlatW / 2.0f + targetDistance + newPlatW / 2.0f, 0, 0);
        //克隆一个平台prefab
        NewPlatform = Instantiate(PlatformPrefab, NewPlatformPos, Quaternion.identity) as GameObject;
        //旋转到可见
        NewPlatform.transform.Rotate(new Vector3(90f,180f,0));
        //新平台宽度缩放
        NewPlatform.transform.localScale = new Vector3(targetScale*0.05f, 1, 1);
        //恢复子元素加分区域的缩放
        NewPlatform.transform.Find("BonusArea").localScale = new Vector3(0.2f,1f,0.01f);
        //新平台宽度
        newPlatW = NewPlatform.GetComponent<MeshRenderer>().bounds.size.x;
        //及时停止创建
        canCreatePlatform = false;
        yield return 0;
    }

    //游戏结束处理
    private IEnumerator processGameover()
    {
		Client.GameScore = score;
        //结算最高成绩：
        int best = PlayerPrefs.GetInt("game_best");
        if (score > best)
        {
			PlayerPrefs.SetInt("game_best", score);
        }

        //等待震动动画结束
        yield return new WaitForSeconds(2.0f);
        // 显示gameover面板
		Client.currentGameOver = false;
		Client.Ins.GameOverPanel.SetActive(true);
		FocusableManager._needUpdate = true;

        yield return 0;
    }
}
