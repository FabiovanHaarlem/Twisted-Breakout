using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Camera;
    public static Manager m_Instance;

    private Vector3 m_CameraStartPosition;
    
    private float m_ShakeTimer;
    private float m_ShakeAmount;

    private void Start()
    {
        m_Instance = this;
        m_CameraStartPosition = m_Camera.transform.position;
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (m_ShakeTimer >= 0)
        {
            Vector2 shakePosition = Random.insideUnitCircle * m_ShakeAmount;

            m_Camera.transform.position = new Vector3(m_CameraStartPosition.x + shakePosition.x, m_CameraStartPosition.y + shakePosition.y, -10);

            m_ShakeTimer -= Time.deltaTime;
        }
        else
        {
            m_Camera.transform.position = m_CameraStartPosition;
        }
    }

    public void ShakeCamera()
    {
        float shakeStrength = 0.1f;
        float shakeDuration = 0.6f;
        m_ShakeAmount = shakeStrength;
        m_ShakeTimer = shakeDuration;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
