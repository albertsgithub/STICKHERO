using UnityEngine;
using System.Collections;

public class TextureRandomizer : MonoBehaviour {

	public Texture2D[] availableTextures;			//可供选择的背景

	void Start () {
        //没有背景则不生成
		if(availableTextures == null)
			return;

        //随机背景:
        //背景个数
        int textureNum = availableTextures.Length;
        //随机下标
        int randomBackgroundIndex = Random.RandomRange(0, textureNum-1);
        //设置随机背景贴图
		GetComponent<Renderer>().material.mainTexture = availableTextures[randomBackgroundIndex];
	}

}
