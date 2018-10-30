using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObject : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_Pieces;
    [SerializeField]
    private List<GameObject> m_PiecesLocations;

    private Quaternion m_DefaultRotation;

    private float m_DeactivateTimer;

    private void Start()
    {
        m_DefaultRotation = new Quaternion();
        m_DeactivateTimer = 5f;

        for (int i = 0; i < m_Pieces.Count; i++)
        {
            m_Pieces[i].transform.position = m_PiecesLocations[i].transform.position;
        }
    }

    private void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            m_DeactivateTimer -= Time.deltaTime;
        }

        if (m_DeactivateTimer <= 0f)
        {
            Deactivate();
        }
    }

    public void Activate(Vector3 blockPosition)
    {
        this.gameObject.SetActive(true);
        transform.position = blockPosition;
    }

    public void Deactivate()
    {
        RePositionPieces();
        m_DeactivateTimer = 3f;
        this.gameObject.SetActive(false);
    }

    private void RePositionPieces()
    {
        for (int i = 0; i < m_Pieces.Count; i++)
        {
            m_Pieces[i].transform.position = m_PiecesLocations[i].transform.position;
            m_Pieces[i].transform.rotation = m_DefaultRotation;
        }
    }
}
