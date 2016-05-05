using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void OpenIOS(){
		Debug.Log (_OpenWebView ("http://www.blog.csdn.net/cordova"));
	}

	#if UNITY_IOS&&!UNITY_EDITOR
	[DllImportAttribute("__Internal")]
	public static extern int OpenWebView (string url);
	#endif
	public static int _OpenWebView(string url){
		#if UNITY_IOS&&!UNITY_EDITOR
		//return 0;
		return OpenWebView (url);

		#else
		return 1;
		#endif
	}
}
