using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMgr : TSingleton<BallMgr>, IInitializeable
{
    public void OnInit()
    {
    }

    public void CreateBall()
    {
        JumpBall ball = ObjectCache.GetObject<JumpBall>(true);
    }

    public Vector3 GetRandomPosition()
    {
        return Vector3.zero;
    }

    public void OnDispose()
    {
    }
}
