using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBall : MonoBehaviour
{
    Rigidbody2D m_rigBody = null;
    private const float m_fSpeed = 12;
    private void Awake()
    {
        m_rigBody = GetComponent<Rigidbody2D>();
    }

    public void LoadBall()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(0, 1, 0);
        m_rigBody.velocity = Vector3.down * m_fSpeed;
    }

    public void ChangeVelocity(Vector2 vecDir)
    {
        m_rigBody.velocity = vecDir * m_fSpeed;
    }

    public void OnReset()
    {
        gameObject.SetActive(false);
        m_rigBody.velocity = Vector2.zero;
        transform.position = new Vector3(0, 1, 0);
    }
}
