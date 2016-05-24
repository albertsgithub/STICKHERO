//  
//JoystickController.cs  
//  
// Created by [JiangXinhou]  
//  
// Copyright jiangxinhou@outlook.com (http://blog.csdn.net/cordova)
using System;
using UnityEngine;
using MojingSample.CrossPlatformInput;
	
	/// <summary>
	/// 摇杆和tv控制器的模拟
	/// </summary>
	public class JoystickController : Singleton<JoystickController> {

		public bool SimulateJoystick = true;

		//上下左右确认返回菜单事件代理
		public Action OnLeft = delegate { };
		public Action OnRight = delegate { };
		public Action OnUp = delegate { };
		public Action OnDown = delegate { };
		public Action OnConfirm = delegate { };
		public Action OnBack = delegate { };
        public Action OnMenu = delegate { };

		//输入检测
		void Update() {
			if (this.SimulateJoystick) {
			if (Input.GetKeyDown(KeyCode.UpArrow)||CrossPlatformInputManager.GetButtonDown("UP")) {
					this.OnUp();
			} else if (Input.GetKeyDown(KeyCode.LeftArrow)||CrossPlatformInputManager.GetButtonDown("LEFT")) {
					this.OnLeft();
			} else if (Input.GetKeyDown(KeyCode.RightArrow)||CrossPlatformInputManager.GetButtonDown("RIGHT")) {
					this.OnRight();
			} else if (Input.GetKeyDown(KeyCode.DownArrow)||CrossPlatformInputManager.GetButtonDown("DOWN")) {
					this.OnDown();
			} else if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0)||CrossPlatformInputManager.GetButtonDown("OK")) {
					this.OnConfirm();
			} else if (Input.GetKeyDown(KeyCode.Escape)||CrossPlatformInputManager.GetButtonDown("C")) {
					this.OnBack();
			} else if (Input.GetKeyDown(KeyCode.Menu)||Input.GetKeyDown(KeyCode.RightControl)||CrossPlatformInputManager.GetButtonDown("MENU"))
                {
                    this.OnMenu();
                }
			}

		}
			
	}