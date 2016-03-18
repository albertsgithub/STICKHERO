using UnityEngine;
using System.Collections;

public class AnimatedAtlas : MonoBehaviour {
		
	///*************************************************************************///
	/// 贴图动画
	///*************************************************************************///

	public int tileX; 						//X 方向贴图
	public int tileY; 						//Y 方向贴图
	public float framesPerSecond = 16.0f;	//动画速度

	private int index;
	private Vector2 size;
	private int uIndex;
	private int vIndex;
	private Vector2 offset;

	void Update (){
	    index = (int)((Time.time * framesPerSecond) % (tileX * tileY));
	    size = new Vector2(1.0f / tileX, 1.0f / tileY);
	    uIndex = index % tileX;
	    vIndex = index / tileX;
	    offset = new Vector2(uIndex * size.x, 1.0f - size.y - vIndex * size.y);
	   	GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", offset);
	    GetComponent<Renderer>().material.SetTextureScale ("_MainTex", size);
	}

}