using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 m_Velocity;
    private Vector3 m_VelocityStandard;
    [SerializeField]
    private GameObject m_Player;

    private bool m_Starting;

    private float m_BallSpeed;

    private float m_MaxBounceAngle;

    private bool m_ResetPause;
    public bool GetResetPause
    { get { return m_ResetPause; } }

    private float m_ResetPauseTimer;

    void Start()
    {
        m_Velocity = new Vector2(0, 0);
        m_VelocityStandard = new Vector2(6, 6);
        m_BallSpeed = 10f;
        m_Starting = true;
        m_MaxBounceAngle = 5 * Mathf.PI / 12;
        m_ResetPause = false;
        m_ResetPauseTimer = 0.5f;
    }

    void FixedUpdate()
    {
        if (!m_Starting)
        {
            transform.position += m_Velocity.normalized * (m_BallSpeed + (ScoreManager.m_Instance.GetHitStreakValue() * 0.7f)) * Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
        if (m_Starting)
        {
            if (m_ResetPauseTimer >= 0f)
            {
                m_ResetPauseTimer -= Time.deltaTime;
            }
            else if (m_ResetPauseTimer <= 0f)
            {
                m_ResetPause = false;
            }

            if (!m_ResetPause)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    m_Velocity = new Vector2(-6, 6);
                    m_Starting = false;
                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    m_Velocity = new Vector2(6, 6);

                    m_Starting = false;
                }
            }
        }
    }

    public void ResetBall()
    {
        m_Velocity = new Vector2(0, 0);
        m_Starting = true;
        transform.position = new Vector3(m_Player.transform.position.x, m_Player.transform.position.y + 1.5f);
        m_ResetPause = true;
        m_ResetPauseTimer = 0.5f;
    }

    public void Bounce(int hitSide, Collision2D collision)
    {
        ChangeDirection(hitSide, collision);
    }

    private void PlayerBounce(int hitSide, Collision2D collision)
    {
        if (hitSide == 0 || hitSide == 1)
        {
            float relativeIntersectX = (collision.gameObject.transform.position.x + (collision.collider.bounds.size.x / 2))
                - (collision.gameObject.transform.position.x - transform.position.x);

            float normalizedRelativeIntersectionX = (relativeIntersectX / (collision.collider.bounds.size.x / 2));
            float bounceAngle = normalizedRelativeIntersectionX * m_MaxBounceAngle;

            m_Velocity.x = m_BallSpeed * Mathf.Cos(bounceAngle);
        }
        else if (hitSide == 2 || hitSide == 3)
        {
            float relativeIntersectY = (collision.gameObject.transform.position.y - (collision.collider.bounds.size.y / 2))
                - (collision.gameObject.transform.position.y + transform.position.y);

            float normalizedRelativeIntersectionY = (relativeIntersectY / (collision.collider.bounds.size.y / 2));
            float bounceAngle = normalizedRelativeIntersectionY * m_MaxBounceAngle;

            m_Velocity.y = m_BallSpeed * -Mathf.Sin(bounceAngle);
        }
    }

    public void ChangeDirection(int hitSide, Collision2D collision)
    {
        switch (hitSide)
        {
            case 0:
                m_Velocity.x = m_VelocityStandard.x;
                break;
            case 1:
                m_Velocity.x = -m_VelocityStandard.x;
                break;
            case 2:
                m_Velocity.y = m_VelocityStandard.y;
                break;
            case 3:
                m_Velocity.y = -m_VelocityStandard.y;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BottomWall"))
        {
            ResetBall();
            ScoreManager.m_Instance.ZeroHitStreak();
            ScoreManager.m_Instance.RemoveTime();
        }

        if (collision.collider.CompareTag("Player"))
        {
            ScoreManager.m_Instance.AddTime();
        }

        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Block"))
        {
            Vector2 collisionPoint = collision.contacts[0].normal;

            if (collisionPoint.x == 1.0f)
            {
                Bounce(0, collision);
            }
            else if (collisionPoint.x == -1.0)
            {
                Bounce(1, collision);
            }

            if (collisionPoint.y == 1.0f)
            {
                Bounce(2, collision);
            }
            else if (collisionPoint.y == -1.0f)
            {
                Bounce(3, collision);
            }
        }

        if (collision.collider.CompareTag("Player"))
        {
            Vector2 collisionPoint = collision.contacts[0].normal;

            if (collisionPoint.x >= 0.1f)
            {
                PlayerBounce(0, collision);
            }
            else if (collisionPoint.x <= -0.1f)
            {
                PlayerBounce(1, collision);
            }

            if (collisionPoint.y >= 0.1f)
            {
                PlayerBounce(2, collision);
            }
            else if (collisionPoint.y <= -0.1f)
            {
                PlayerBounce(3, collision);
            }
        }
    }
}
