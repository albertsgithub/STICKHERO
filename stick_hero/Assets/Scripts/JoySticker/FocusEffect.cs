//  
//FocusEffect.cs  
//  
// Created by [JiangXinhou]  
//  
// Copyright jiangxinhou@outlook.com (http://blog.csdn.net/cordova)
using UnityEngine;
using System.Collections;
	
	public class FocusEffect : MonoBehaviour {

		public float radius = 0.02f;
		private Vector3 oldScale;
		float radian = 0;
		public float speed = 0.15f;

		public GameObject Icon;
		public FocusableItem item; 

		// Use this for initialization
		void Start () {
			oldScale = Icon.transform.localScale;
		}

		// Update is called once per frame
		void Update () {
			radian += speed;
			float scaleAdd = Mathf.Sin (radian) * radius;
			Icon.transform.localScale = oldScale + new Vector3 (scaleAdd,scaleAdd,scaleAdd);

			if (this.item != null) {
				//this.transform.position = this.item.transform.position;
				var targetPos = this.item.transform.position;
				this.transform.position = Vector3.Lerp(this.transform.position, targetPos,
					15 * Time.unscaledDeltaTime);
			}
		}

		public void Focus(FocusableItem item){
			//保证光标是可见的
			if (!gameObject.activeSelf) {
				gameObject.SetActive(true);
			}

			this.item = item;
		}
	}	
