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
    private Dictionary<UIData, BaseUI> m_dictShowingUI;
    private Dictionary<UIData, BaseUI> m_dicCacheUI;
    private List<BaseUI> m_listDestoryUI;

    public void OnInit()
    {
        m_transRoot = GameObject.Find("Root").transform;
        m_dictShowingUI = new Dictionary<UIData, BaseUI>();
        m_dicCacheUI = new Dictionary<UIData, BaseUI>();
        m_listDestoryUI = new List<BaseUI>();
    }

    public void ShowUI(UIData data, Action action = null, object[] param = null)
    {
        if (string.IsNullOrEmpty(data.strPath))
        {
            return;
        }

        BaseUI ui;
        if (!m_dicCacheUI.TryGetValue(data, out ui))
        {
            GameObject go = AsserMgr.Instance.LoadAsset(data.strPath, typeof(GameObject)) as GameObject;
            if (go == null)
            {
                Debug.LogError("资源加载报错==" + data.strPath);
                return;
            }

            RectTransform transPrefab = GameObject.Instantiate(go).GetComponent<RectTransform>();
            transPrefab.SetParent(m_transRoot);
            transPrefab.localScale = Vector3.one;
            transPrefab.anchoredPosition = Vector3.zero;
            transPrefab.anchorMin = Vector3.zero;
            transPrefab.anchorMax = Vector3.one;
            transPrefab.localEulerAngles = Vector3.zero;
            transPrefab.sizeDelta = Vector3.zero;

            ui = transPrefab.GetComponent<BaseUI>();
        }

        ui.gameObject.SetActive(true);
        ui.OnShow(action, param);
        m_dictShowingUI[data] = ui;
        if(m_dicCacheUI.TryGetValue(data, out BaseUI temp))
        {
            temp = null;
        }
    }

    public void OnUpdate(float deltaTime)
    {
        List<BaseUI> listDestory = new List<BaseUI>();
        for(int i = 0; i < m_listDestoryUI.Count; i++)
        {
            // 拷贝列表
            listDestory.Add(m_listDestoryUI[i]);
        }
        
        for (int i = 0; i < listDestory.Count; i++)
        {
            GameObject.Destroy(listDestory[i]);
        }

        listDestory.Clear();
        m_listDestoryUI.Clear();
    }

    public void CloseUI(UIData data)
    {
        BaseUI ui = m_dictShowingUI[data];        
        if (ui == null)
        {
            Debug.LogError("UI关闭报错==" + data.strPath);
        }

        ui.OnClose();
        ui.gameObject.SetActive(false);
        if (data.isCloseDestory)
        {
            m_listDestoryUI.Add(ui);
        }
        else
        {
            m_dicCacheUI[data] = ui;
        }
    }

    public void OnDispose()
    {
        m_dictShowingUI.Clear();
        m_dicCacheUI.Clear();
        m_listDestoryUI.Clear();
    }
}