using UnityEngine;

public class MoveBoxMgr : TSingleton<MoveBoxMgr>, IInitializeable, IUpdateable
{
    private Transform m_transBox = null;
    private float m_fSpeed = 0.005f;
    private Vector3 m_vecDir = Vector3.zero;
    private Vector3 m_vecBallDir = Vector3.up;

    public void OnInit()
    {
        m_transBox = GameObject.Find("box_trans").transform;
        m_transBox.position = Vector3.zero;
    }

    public void OnUpdate(float fDeltaTime)
    {
        if (m_vecDir == Vector3.zero)
        {
            return;
        }

        m_transBox.position += m_vecDir * m_fSpeed;
    }

    private void OnEnterCollider(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)LayName.JUMP_BALL)
        {
            Vector3 closestPoint = collision.collider.ClosestPoint(m_transBox.position);//获取碰撞位置
            //m_vecBallDir = closestPoint.normalized;
            //collision.transform.GetComponent<JumpBall>().ChangeVelocity(m_vecDir);
        }
    }

    public void SetHitCollider()
    {
        m_transBox.GetComponentInChildren<HitTrigger>().OnHit = OnEnterCollider;
    }

    public void SetDragDir(Vector3 vecDir)
    {
        m_vecDir = vecDir;
    }

    public Vector3 GetBallColliderDir()
    {
        return m_vecBallDir;
    }

    public void OnDispose()
    {
    }
}
