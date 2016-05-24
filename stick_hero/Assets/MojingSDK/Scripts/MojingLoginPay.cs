using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class MojingLoginPay : MonoBehaviour {
    void Start () {
		//绑定事件
		ArrayList btnsName = new ArrayList();
		btnsName.Add ("SingleLogin");
		btnsName.Add ("DoubleLogin");
		btnsName.Add ("MjAppAutoLogin");
		btnsName.Add ("Register");
		btnsName.Add ("Pay");
		btnsName.Add ("Modou");

		foreach (string btnName in btnsName ) {
			GameObject btnObj = GameObject.Find(btnName);
			Button btn = btnObj.GetComponent<Button>();
			btn.onClick.AddListener(delegate() {
                this.OnClick(btnObj);
            });
		}
	}

    //单屏登录
    public static void SingleLogin()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
		javaObject.CallStatic ("mjCallSingleLogin");
#endif
        Debug.Log("SingleLogin");
    }
    //双屏登录
    public static void DoubleLogin()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
        javaObject.CallStatic("mjCallDoubleLogin");
#endif
        Debug.Log("DoubleLogin");
    }
    //同步App登录状态
    public static void MjAppAutoLogin()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
        javaObject.CallStatic("mjAppAutoLogin");
#endif
    }
    //注册
    public static void Register()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
        javaObject.CallStatic("mjCallRegister");
#endif
    }
    //消费1个魔豆
    public static void Pay()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
        javaObject.CallStatic("mjPayModou", "1.00", "北京区服");
#endif
    }
    //获取用户魔豆数
    public static void Modou()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
        javaObject.CallStatic("mjGetModou");
#endif
    }
    //ButtonClickedEvent
    void OnClick(GameObject btnObj) {
		switch (btnObj.name) {
			case "SingleLogin": //单屏登录
                SingleLogin();
			break;

			case "DoubleLogin": //双屏登录
                DoubleLogin();
			break;

			case "MjAppAutoLogin": //同步App登录状态
                MjAppAutoLogin();
			break;

			case "Register": //注册
                Register();
			break;

			case "Pay": //购买
                Pay();
			break;

			case "Modou": //获取魔豆
                Modou();
			break;
		}

	}

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_IOS
    void OnGUI()
    {
        GUI.skin.label.fontSize = 30;
        GUI.color = Color.red;
        GUILayout.Label("iOS/Windows can not support Login & Pay Model now~", GUILayout.Width(1500));
    }
#endif
}
