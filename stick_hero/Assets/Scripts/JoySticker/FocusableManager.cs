//  
//FocusableManager.cs  
//  
// Created by [JiangXinhou]  
//  
// Copyright jiangxinhou@outlook.com (http://blog.csdn.net/cordova)
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 聚焦项容器数据结构
/// </summary>
[Serializable]
public class FocusableContainer
{
    public FocusableRoot Root;                                      // 根节点
    public List<FocusableItem> Items = new List<FocusableItem>();   // 根节点下的items
}

/// <summary>
/// 聚焦管理器
/// </summary>
public class FocusableManager : Singleton<FocusableManager>
{

    public FocusableItem CurrentFocusableItem;                                       //当前聚焦的聚焦项  
    public bool Enable;                                                              //聚焦功能是否启用
    public bool ShowDebug;                                                           //是否打印显示聚焦信息
    public static bool _needUpdate = false;                                          //更新聚焦状态
    public FocusEffect FocusEffect;                                                  //光标显示特效
    public List<FocusableContainer> Containers;                                      //聚焦项目容器列表，每个容器容纳多个聚焦项

    #region 生命周期
    void Start()
    {
        if (!this.Enable)
        {
            return;
        }

        //注册点击事件

        //选中点击确认
        JoystickController.Ins.OnConfirm += () =>
        {
            if (CurrentFocusableItem != null)
            {
                this.CurrentFocusableItem.OnConfirm();
            }
        };
        //返回
        JoystickController.Ins.OnBack += () =>
        {
            Client.Ins.MenuBG.SetActive(true);
            Client.Ins.ScoreText.SetActive(false);
            Application.LoadLevel("Menu");
        };
        //菜单键按下
        JoystickController.Ins.OnMenu += () =>
        {
        };
        //左
        JoystickController.Ins.OnLeft += () =>
        {
            if (CurrentFocusableItem != null)
            {
                //只筛选和当前聚焦按钮在同一个父节点下的物体
                var list = this.Containers.Find(container => container.Root == this.CurrentFocusableItem.Root).Items;
                //聚焦到合适的按钮
                this.Focus(CurrentFocusableItem.Get(DirType.Left, list));
            }
        };
        //右
        JoystickController.Ins.OnRight += () =>
        {
            if (CurrentFocusableItem != null)
            {
                var list = this.Containers.Find(container => container.Root == CurrentFocusableItem.Root).Items;
                this.Focus(CurrentFocusableItem.Get(DirType.Right, list));
            }
        };
        //上
        JoystickController.Ins.OnUp += () =>
        {
            if (CurrentFocusableItem != null)
            {
                //只筛选和当前聚焦按钮在同一个父节点下的物体
                var list = this.Containers.Find(container => container.Root == this.CurrentFocusableItem.Root).Items;
                //聚焦到合适的按钮
                this.Focus(CurrentFocusableItem.Get(DirType.Up, list));
            }
        };
        //下
        JoystickController.Ins.OnDown += () =>
        {
            if (CurrentFocusableItem != null)
            {
                //只筛选和当前聚焦按钮在同一个父节点下的物体
                var list = this.Containers.Find(container => container.Root == this.CurrentFocusableItem.Root).Items;
                //聚焦到合适的按钮
                this.Focus(CurrentFocusableItem.Get(DirType.Down, list));
            }
        };


    }
    void OnGUI()
    {
        if (this.ShowDebug)
        {
            if (CurrentFocusableItem != null)
            {
                if (GUILayout.Button("CurrentFocus:" + CurrentFocusableItem.transform.position))
                {
                }
            }
        }
    }
    void LateUpdate()
    {
        if (_needUpdate)
        {
            _needUpdate = false;
            FocusableItem result = null;

            foreach (var o in this.Containers)
            {
                if (o.Items.Count > 0)
                {
                    //找到聚焦点
                    result = o.Items[0];//默认选取第一个
                    foreach (var item in o.Items)
                    {
                        if (item.FirstFocus)
                        {//如果设置了优先选取则选中优先选取
                            result = item;
                            break;
                        }
                    }
                    break;
                }
            }

            Focus(result);
        }
    }
    #endregion

    /// <summary>
    /// 聚焦到指定项目
    /// </summary>
    public void Focus(FocusableItem item)
    {
        if (CurrentFocusableItem == item)
        {
            return;
        }

        //通知聚焦项失去聚焦
        if (this.CurrentFocusableItem != null)
        {
            this.CurrentFocusableItem.OnLostFocuse();
        }

        CurrentFocusableItem = item;

        //通知聚焦项已经被聚焦
        if (CurrentFocusableItem != null)
        {
            CurrentFocusableItem.OnFocused();
        }

        //设置手指的更新显示
        if (item == null)
        {
            this.FocusEffect.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.FocusEffect.Focus(item);
        }
    }

    /// <summary>
    /// 更新聚焦列表
    /// </summary>
    private void OnUpdateList()
    {
        _needUpdate = true;
    }
    IEnumerator UpdateList()
    {
        yield return 0;
        OnUpdateList();
    }

    /// <summary>
    /// 注册聚焦项
    /// </summary>
    public void Register(FocusableRoot root, FocusableItem item)
    {
        if (!this.Enable)
        {
            return;
        }
        var c = this.Containers.Find(container => container.Root == root);
        c.Items.Add(item);
        StartCoroutine(UpdateList());
    }

    /// <summary>
    /// 注销聚焦项
    /// </summary>
    public void UnRegister(FocusableRoot root, FocusableItem item)
    {
        if (!this.Enable)
        {
            return;
        }
        var c = this.Containers.Find(container => container.Root == root);
        if (c != null)
        {
            c.Items.Remove(item);
        }
        StartCoroutine(UpdateList());
    }

}