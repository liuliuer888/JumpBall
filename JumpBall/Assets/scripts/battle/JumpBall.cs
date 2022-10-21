﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBall : ICacheable
{
    private string m_strPath = "prefab/battle/ball.prefab";
    private Transform m_transPrefab = null;
    public JumpBall()
    {
        Transform transParent = GameObject.Find("ball_trans").transform;
        GameObject obj = AsserMgr.Instance.LoadAsset(m_strPath, typeof(GameObject)) as GameObject;
        m_transPrefab = GameObject.Instantiate(obj).transform;
        m_transPrefab.SetParent(transParent);
        m_transPrefab.localScale = Vector3.one;
        m_transPrefab.position = Vector3.zero;
        m_transPrefab.gameObject.SetActive(true);
    }

    public void FreeToCache()
    {
        m_transPrefab.localScale = Vector3.one;
        m_transPrefab.position = Vector3.zero;
        m_transPrefab.gameObject.SetActive(false);
    }
}