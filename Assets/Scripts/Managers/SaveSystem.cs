using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private bool m_SetPlayerScore;

    private void Start()
    {
        m_SetPlayerScore = false;

        if (!File.Exists(Application.persistentDataPath + "/HighScores.json"))
        {
            ScoreData scoreData = new ScoreData();
            scoreData.m_Saves = new ScoreEntry[3];

            string json = JsonUtility.ToJson(scoreData);
            File.WriteAllText(Application.persistentDataPath + "/HighScores.json", json);
        }
    }

    public void SaveScore(string name, int score, int highestHitStreak)
    {
        ScoreData scoreData = new ScoreData();
        scoreData.m_Saves = new ScoreEntry[3];

        ScoreData loadedData = LoadScore();
        scoreData = loadedData;

        if (score >= scoreData.m_Saves[0].m_Score)
        {
            scoreData.m_Saves[2].m_Name = scoreData.m_Saves[1].m_Name;
            scoreData.m_Saves[2].m_Score = scoreData.m_Saves[1].m_Score;
            scoreData.m_Saves[2].m_HitScore = scoreData.m_Saves[1].m_HitScore;

            scoreData.m_Saves[1].m_Name = scoreData.m_Saves[0].m_Name;
            scoreData.m_Saves[1].m_Score = scoreData.m_Saves[0].m_Score;
            scoreData.m_Saves[1].m_HitScore = scoreData.m_Saves[0].m_HitScore;

            scoreData.m_Saves[0].m_Name = name;
            scoreData.m_Saves[0].m_Score = score;
            scoreData.m_Saves[0].m_HitScore = highestHitStreak;
        }
        else if (score >= scoreData.m_Saves[1].m_Score)
        {
            scoreData.m_Saves[2].m_Name = scoreData.m_Saves[1].m_Name;
            scoreData.m_Saves[2].m_Score = scoreData.m_Saves[1].m_Score;
            scoreData.m_Saves[2].m_HitScore = scoreData.m_Saves[1].m_HitScore;

            scoreData.m_Saves[1].m_Name = name;
            scoreData.m_Saves[1].m_Score = score;
            scoreData.m_Saves[0].m_HitScore = highestHitStreak;
        }
        else if (score >= scoreData.m_Saves[2].m_Score)
        {
            scoreData.m_Saves[2].m_Name = name;
            scoreData.m_Saves[2].m_Score = score;
            scoreData.m_Saves[0].m_HitScore = highestHitStreak;
        }

        string json = JsonUtility.ToJson(scoreData);
        File.WriteAllText(Application.persistentDataPath + "/HighScores.json", json);
    }

    public ScoreData LoadScore()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/HighScores.json");
        ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);

        return scoreData;
    }

}
