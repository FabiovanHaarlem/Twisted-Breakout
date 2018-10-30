using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float m_Speed;
    [SerializeField]
    private Ball m_Ball;
	
	void Start ()
    {
        m_Speed = 20f;
	}
	
	
	void Update ()
    {
        if (!m_Ball.GetResetPause)
        {
            Vector3 velocity = new Vector3(Input.GetAxisRaw("Horizontal") * (m_Speed + (ScoreManager.m_Instance.GetHitStreakValue() * 0.1f)) * Time.deltaTime, 0, 0);
            transform.Translate(velocity);

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -13f, 13), transform.position.y, 0);
        }
	}
}
