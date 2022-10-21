using System;
using UnityEngine;
/**
 * 文件名：BaseUI.cs
 * 文件描述：UI基类
 * 作者：liuzhitao
 * 创建时间：2022/10/20 17:08:44
 * 修改记录：
 */

public class UIData
{
    public string strPath;
    public bool isCloseDestory;
}

public class BaseUI : MonoBehaviour
{
    void Awake()
    {
        OnInit();
        RegisterMessage();
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            OnUpdate(Time.deltaTime);
        }
    }

    public void OpenUI(UIData info)
    {
        UIMgr.Instance.ShowUI(info);
    }

    public void Close(UIData info)
    {
        UIMgr.Instance.CloseUI(info);
    }

    public virtual void OnInit()
    {
    }

    public virtual void OnShow(Action action, object[] param)
    {
    }

    public virtual void RegisterMessage()
    {
    }

    public virtual void OnUpdate(float deltaTime)
    {
    }

    public virtual void OnClose()
    {
        // todo 可以做移除监听
    }
}
