using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JoystickInputController {

	/// <summary>
	/// 聚焦项基类
	/// </summary>
	public class FocusableItem : MonoBehaviour {

		public bool FirstFocus = false;       //优先获得聚焦
		public FocusableRoot Root;            //根节点

		public BoxCollider boxCollider { get; private set; }

		//上下左右相邻的聚焦项目及其禁用状态
		public bool DisableLeft = false;
		public FocusableItem Left;
		public bool DisableRight = false;
		public FocusableItem Right;
		public bool DisableUp = false;
		public FocusableItem Up;
		public bool DisableDown = false;
		public FocusableItem Down;

		public virtual void Awake() {
			this.boxCollider = GetComponent<BoxCollider>();
		}

		public virtual void OnEnable() {
			StartCoroutine(Register());
		}
		void OnDisable() {
			if (FocusableManager.Ins != null) {
				FocusableManager.Ins.UnRegister(Root, this);
			}
		}

		/// <summary>
		/// 注册节点
		/// </summary>
		IEnumerator Register() {
			//为了防止某些时候由于界面的延迟，导致root找不到，这里延迟一会
			yield return 0;
			yield return 0;
			FindRoot();
			FocusableManager.Ins.Register(Root, this);
		}

		/// <summary>
		/// 获取聚焦项根节点
		/// </summary>
		protected virtual void FindRoot() {
			this.Root = transform.GetComponentInParent<FocusableRoot>();
		}

		/// <summary>
		/// 判断聚焦项是否可聚焦
		/// </summary>
		public virtual bool IsActive {
			get {
				return gameObject.activeInHierarchy && gameObject.activeSelf;
			}
		}

		/// <summary>
		/// 获取指定方向的相邻的聚焦项目
		/// </summary>
		public virtual FocusableItem Get(DirType dir, List<FocusableItem> list) {
			var ret = this;
			switch (dir) {
				case DirType.Left:
					if (this.DisableLeft) {
						return this;
					}
					if (this.Left != null && this.Left.IsActive) {
						return this.Left;
					}
					ret = Search(Vector3.left, list) ?? this;
					break;

				case DirType.Right:
					if (this.DisableRight) {
						return this;
					}
					if (this.Right != null && this.Right.IsActive) {
						return this.Right;
					}
					ret = Search(Vector3.right, list) ?? this;
					break;

				case DirType.Up:
					if (this.DisableUp) {
						return this;
					}
					if (this.Up != null && this.Up.IsActive) {
						return this.Up;
					}
					ret = Search(Vector3.up, list) ?? this;
					break;

				case DirType.Down:
					if (this.DisableDown) {
						return this;
					}
					if (this.Down != null && this.Down.IsActive) {
						return this.Down;
					}
					ret = Search(Vector3.down, list) ?? this;
					break;
			}
			//释放优先聚焦权
			FirstFocus = false;
			return ret;
		}

		/// <summary>
		///       计算并选取最佳相邻聚焦项目
		/// </summary>
		protected virtual FocusableItem Search(Vector3 dir, List<FocusableItem> list) {
			Vector3 myScreenPos = transform.position;
			float min = float.MaxValue;
			float weightMax = 0;
			//计算的最佳项目
			FocusableItem ret = null;
			for (int i = 0; i < list.Count; ++i) {
				FocusableItem item = list[i];
				//跳过自己本身和不活跃的
				if (item == this || !item.IsActive) {
					continue;
				}

				Vector3 navCenter = item.transform.position;
				//目标对象与起点的方向向量
				Vector3 targetDir = navCenter - myScreenPos;
				//单位向量点积
				float dot = Vector3.Dot(dir, targetDir.normalized);
				//大于一定角度度跳过
				if (dot <= 0.2f) {
					continue;
				}

				float mag = targetDir.magnitude;
				if (mag == 0f) {
					Debug.LogError("找到完全重合的点，跳过" + item.name);
					continue;
				}

				float weight = 1f / mag * 10f;
				if (dot > 0.99f) {//如果角度极为一致
								  //判断是否属于同一组物体
					if (this.transform.parent == item.transform.parent) {
						weight += 1000f;
					}
				}
				//				weight += dot / (1.01f - dot) / mag + dot * (1f) / mag;
				if (weight > weightMax) {
					weightMax = weight;
					ret = item;
				}


				//				power = (1f - dot)*mag;

				//				if (weight == float.PositiveInfinity && powerTemp == float.PositiveInfinity) {
				//					if (mag < min) {
				//						powerTemp = weight;
				//						ret = item;
				//						min = mag;
				//					}
				//				} else if (weight > powerTemp) {
				//					powerTemp = weight;
				//					ret = item;
				//					min = mag;
				//				}
			}
			return ret;
		}

		/// <summary>
		/// 被聚焦
		/// </summary>
		public virtual void OnFocused() {
		}

		public virtual void OnLostFocuse() {
		}

		/// <summary>
		/// 被点击
		/// </summary>
		public virtual void OnConfirm() {
			//这样可以使主页面被点击的按钮优先聚焦
			//gameObject.SendMessage("click")
		}
			
	}
}