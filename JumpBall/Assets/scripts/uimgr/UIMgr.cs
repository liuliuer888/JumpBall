using System;
using System.Collections.Generic;
using UnityEngine;
/**
 * 文件名：UIMgr.cs
 * 文件描述：UI管理
 * 作者：liuzhitao
 * 创建时间：2022/10/20 17:08:44
 * 修改记录：
 */

public class UIMgr : TSingleton<UIMgr>, IInitializeable, IUpdateable
{
    private Transform m_transRoot;
    private List<BaseUI> m_listShowingUI;
    private List<BaseUI> m_listDestoryUI;
    private Dictionary<UIData, BaseUI> m_dicCacheUI;

    public void OnInit()
    {
        m_transRoot = GameObject.Find("Root").transform;
        m_listShowingUI = new List<BaseUI>();
        m_listDestoryUI = new List<BaseUI>();
        m_dicCacheUI = new Dictionary<UIData, BaseUI>();
    }

    public void ShowUI(UIData data, Action action, object[] param)
    {
        if (string.IsNullOrEmpty(data.strPath))
        {
            return;
        }

        BaseUI ui = m_dicCacheUI[data];
        if (ui == null)
        {

        }
    }

    public void OnUpdate(float deltaTime)
    {

    }

    public void OnDispose()
    {

    }
}