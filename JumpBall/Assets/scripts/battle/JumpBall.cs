using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBall : MonoBehaviour
{
    Rigidbody2D m_rigBody = null;
    private void Awake()
    {
        m_rigBody = GetComponent<Rigidbody2D>();
    }

    public void LoadBall()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(0, 1, 0);
    }

    public void ChangeVelocity(Vector2 vecDir)
    {
        m_rigBody.velocity = vecDir * 12;
    }

    public void OnReset()
    {
        gameObject.SetActive(false);
        m_rigBody.velocity = Vector2.zero;
        transform.position = new Vector3(0, 1, 0);
    }
}
