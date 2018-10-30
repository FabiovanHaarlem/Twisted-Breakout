using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ScoreEntry
{
    public string m_Name;
    public int m_Score;
    public int m_HitScore;
}

[System.Serializable]
public class ScoreData
{
    public ScoreEntry[] m_Saves;
}

