using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMgr : TSingleton<BallMgr>, IInitializeable
{
    List<JumpBall> m_listCache = null;
    private string m_strPath = "prefab/battle/ball.prefab";

    public void OnInit()
    {
        m_listCache = new List<JumpBall>();
    }

    public void CreateBall()
    {
        JumpBall ball = null;
        if (m_listCache.Count <= 0)
        {
            Transform transParent = GameObject.Find("ball_trans").transform;
            GameObject obj = AsserMgr.Instance.LoadAsset(m_strPath, typeof(GameObject)) as GameObject;
            Transform transPrefab = GameObject.Instantiate(obj).transform;
            transPrefab.SetParent(transParent);
            transPrefab.GetComponent<HitTrigger>().OnHit = OnEnterCollider;
            ball = transPrefab.GetComponent<JumpBall>();
        }
        else
        {
            ball = m_listCache[0];
            m_listCache.Remove(ball);
        }

        ball.LoadBall();
    }

    private void OnEnterCollider(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)LayName.Wall_Bottom)
        {
            JumpBall ball = collision.otherRigidbody.GetComponent<JumpBall>();
            ball.OnReset();
            m_listCache.Add(ball);
        }
    }

    public void ChangeVelocity()
    {

    }

    public void OnDispose()
    {
    }
}
