using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Callback : MonoBehaviour {

	void Start () {
#if !UNITY_EDITOR && UNITY_ANDROID
		AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
		javaObject.CallStatic ("mjMerchantVerification");
#endif
	}

	public void MJLoginCallback(string msg) {
		Debug.Log (msg);
		gameObject.GetComponent<Text> ().text = getMsg(msg);
	}

	public void MjPayCallback(string msg) {
		Debug.Log (msg);
		gameObject.GetComponent<Text> ().text = getMsg(msg);
	}

	public void MjVerification(string msg) {
		Debug.Log (msg);
		gameObject.GetComponent<Text> ().text = getMsg(msg);
	}

	public void MjModouCallback(string msg) {
		Debug.Log (msg);
		LitJson.JsonData jarr = LitJson.JsonMapper.ToObject(msg);
		if (jarr.IsObject) {
			if (jarr["code"].Equals("13000")) {
				gameObject.GetComponent<Text> ().text = "魔豆数："+ jarr["modou"] +" 礼券数："+ jarr["gift"];
			} else {
				gameObject.GetComponent<Text> ().text = jarr["msg"]+"";
			}
		}
	}

	private string getMsg(string msg) {
		string message = "";
		if (msg == "00000") {
			message = "网络异常";
		} else if (msg == "10000") {
			message = "登录成功";
		} else if (msg == "10001") {
			message = "用户未登录";
		} else if (msg == "11000") {
			message = "商户验证成功";
		} else if (msg == "11001") {
			message = "商户验证失败";
		} else if (msg == "11002") {
			message = "商户验证参数错误或请求过期";
		} else if (msg == "11003") {
			message = "商户未验证";
		} else if (msg == "12000") {
			message = "支付成功";
		} else if (msg == "12001") {
			message = "支付失败";
		} else if (msg == "12002") {
			message = "魔豆不足";
		}

		return message;
	}
}
