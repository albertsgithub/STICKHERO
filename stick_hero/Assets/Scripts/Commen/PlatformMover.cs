// 
// Code created by [Jiang Xinhou]
//
// Copyright (C) 2014 Nanjing Xiaoxi Network Technology Co., Ltd. (http://www.mogoomobile.com)
using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour {

    private float speed = 5.0f;  //平台移动速度

    private void Update()
    {
        if (PlayerController.isBacking || PlayerNewController.isBacking)
        {
            //后退
            transform.position -= new Vector3(Time.deltaTime * speed, 0f, 0f);
        }
        //超出太远则销毁平台
        if (transform.position.x < -10.0f)
        {
            Destroy(gameObject);
        }
    }
}
