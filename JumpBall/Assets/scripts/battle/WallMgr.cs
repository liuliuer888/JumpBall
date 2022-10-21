using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMgr : TSingleton<WallMgr>, IInitializeable, IUpdateable
{
    private Transform m_transWall4 = null;
    public void OnInit()
    {
        m_transWall4 = GameObject.Find("wall4").transform;
    }

    // Update is called once per frame
    public void OnUpdate(float fDeltaTime)
    {

    }

    public void OnDispose()
    {

    }
}
