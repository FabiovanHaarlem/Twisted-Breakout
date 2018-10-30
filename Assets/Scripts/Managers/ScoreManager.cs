using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private float m_Score;
    [SerializeField]
    private Text m_ScoreText;
    [SerializeField]
    private Text m_HitStreakText;
    [SerializeField]
    private Text m_TimeLeftText;

    [SerializeField]
    private Text m_YourHighestScoreText;
    [SerializeField]
    private Text m_YourHighestHitStreakText;

    [SerializeField]
    private Text m_HighScores;
    [SerializeField]
    private Text[] m_HighScoreTexts;

    [SerializeField]
    private GameObject m_EndMenuHolder;
    public static ScoreManager m_Instance;
    [SerializeField]
    private SaveSystem m_SaveSystem;

    [SerializeField]
    private Transform m_HitEffectPosition;

    private float m_TextStartingZ;

    private float m_GameTimer;
    private int m_HitStreak;

    private int m_HighestHitStreak;

    private float m_ShowHitStreak;
	
	void Start ()
    {
        m_EndMenuHolder.SetActive(false);
        m_Instance = this;
        m_Score = 0;
        m_GameTimer = 30f;
        m_ScoreText.text = "Crush Points: " + Mathf.Round(m_Score);
        m_HitStreakText.text = m_HitStreak + "X";
        m_TimeLeftText.text = Mathf.Round(m_GameTimer).ToString();
        m_ShowHitStreak = 0f;
        m_HitStreakText.gameObject.SetActive(false);
    }

    private void Update()
    {
        m_GameTimer -= Time.deltaTime;

        m_TimeLeftText.text = Mathf.Round(m_GameTimer).ToString();


        if (m_GameTimer <= 0f)
        {
            ActivateEndScreen();
        }

        if (m_ShowHitStreak >= 0f)
        {
            m_ShowHitStreak -= Time.deltaTime;
        }
        else
        {
            m_HitStreakText.gameObject.SetActive(false);
        }

        //MoveHitStreakText();
    }

    //private void MoveHitStreakText()
    //{

    //    //if (m_PingPongTime >= 0f)
    //    //{
    //    //    m_PingPongTime -= Time.deltaTime;
    //    //}
    //    //else if (m_PingPongTime <= 0f)
    //    //{
    //    //    m_PingPongTime = 1f;
    //    //}

    //    //m_TextRectTransform.rotation = new Quaternion(m_TextRectTransform.rotation.x, m_TextRectTransform.rotation.y, m_TextStartingZ, m_TextRectTransform.rotation.w);
    //}

    private void ActivateEndScreen()
    {
        m_EndMenuHolder.SetActive(true);
        m_YourHighestScoreText.text = "Crush Points: " + Mathf.Round(m_Score);
        m_YourHighestHitStreakText.text = "Highest Hit Combo: " + m_HighestHitStreak + "X";
        Time.timeScale = 0f;
    }

    public void AddPoint()
    {
        m_HitStreakText.gameObject.SetActive(true);
        ObjectPool.m_Instance.ActivateForceObjectHit(m_HitEffectPosition.position);
        m_ShowHitStreak = 0.3f;
        m_HitStreak += 1;
        if (m_HitStreak > m_HighestHitStreak)
        {
            m_HighestHitStreak = m_HitStreak;
        }

        m_HitStreakText.text = m_HitStreak + "X";
        m_Score += 1 + (m_HitStreak / 2f);
        m_ScoreText.text = "Crush Points: " + Mathf.Round(m_Score);
        m_GameTimer += 1f + (m_HitStreak / 2f);
    }

    public int GetHitStreakValue()
    {
        return m_HitStreak;
    }

    public void AddTime()
    {
        m_GameTimer += 1f;
    }

    public void RemoveTime()
    {
        m_GameTimer -= 15f;
    }

    public void ZeroHitStreak()
    {
        m_HitStreak = 0;
        m_HitStreakText.text = m_HitStreak + "X";
    }

    public void SaveScore(string name)
    {
        m_SaveSystem.SaveScore(name, Mathf.RoundToInt(m_Score), m_HighestHitStreak);
        LoadScores();
    }

    private void LoadScores()
    {
        ScoreData scoreEntry = m_SaveSystem.LoadScore();

        for (int i = 0; i < scoreEntry.m_Saves.Length; i++)
        {
            m_HighScoreTexts[i].gameObject.SetActive(true);
            m_HighScoreTexts[i].text = (i + 1) + "st  " + scoreEntry.m_Saves[i].m_Name + "  Crush Points: " + scoreEntry.m_Saves[i].m_Score + "  Highest Hit Combo: " + scoreEntry.m_Saves[i].m_HitScore + "X";
        }
    }
}
