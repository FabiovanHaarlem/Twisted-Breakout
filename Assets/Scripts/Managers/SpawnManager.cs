using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float m_SpawnTimer;

    private void Start()
    {
        m_SpawnTimer = 0f;
    }

    private void Update()
    {
        m_SpawnTimer += Time.deltaTime;

        if (m_SpawnTimer >= (0.8f - (ScoreManager.m_Instance.GetHitStreakValue() * 0.01f)))
        {
            SpawnBlock();
            m_SpawnTimer = 0f;
        }
    }

    private void SpawnBlock()
    {
        Vector3 startingPosition = new Vector3(Random.Range(transform.position.x - 10f, transform.position.x + 10), transform.position.y, transform.position.z);
        ObjectPool.m_Instance.ActivateBlock(startingPosition);
    }

}
