using System;
using UnityEngine;
	
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
				if (Input.GetKeyDown(KeyCode.UpArrow)) {
					this.OnUp();
				} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
					this.OnLeft();
				} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
					this.OnRight();
				} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
					this.OnDown();
				} else if (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.KeypadEnter)||Input.GetMouseButtonDown(0)) {
					this.OnConfirm();
				} else if (Input.GetKeyDown(KeyCode.Escape)) {
					this.OnBack();
                } else if (Input.GetKeyDown(KeyCode.Menu)||Input.GetKeyDown(KeyCode.RightControl))
                {
                    this.OnMenu();
                }
			}

		}
			
	}