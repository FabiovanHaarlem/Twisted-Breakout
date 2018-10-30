using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private bool m_Hit;
    private float m_Timer;
    private float m_FallSpeed;

    void Start()
    {
        m_Hit = false;
        m_Timer = 0f;
        m_FallSpeed = 2f;
    }


    void Update()
    {
        if (m_Hit)
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= 0.05f)
            {
                ObjectPool.m_Instance.ActivateForceObject(transform.position);
                ScoreManager.m_Instance.AddPoint();
                Manager.m_Instance.ShakeCamera();
                SoundManager.m_Instance.BlockDestroyed();
                
                Deactivate();
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y - (m_FallSpeed + (ScoreManager.m_Instance.GetHitStreakValue() * 0.1f)) * Time.deltaTime, transform.position.z);
    }

    public void Activate(Vector3 startPosition)
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        m_Hit = false;
        m_Timer = 0f;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            m_Hit = true;
        }

        if (collision.collider.CompareTag("MiddleWall"))
        {
            Deactivate();
        }
    }
}
