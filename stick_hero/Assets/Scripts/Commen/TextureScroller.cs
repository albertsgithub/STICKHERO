using UnityEngine;
using System.Collections;

public class TextureScroller : MonoBehaviour {

	///***********************************************************************
	/// 背景移动
	///***********************************************************************

	private float offset;
	private float speed = 0.1f;
	
	void Update () {

		if(PlayerController.isWalking || PlayerNewController.isBacking) {
            offset += speed * Time.deltaTime * (GetComponent<Renderer>().material.mainTextureScale.x / 1.5f);
			GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", new Vector2(offset, 0));
		}
	}
}
