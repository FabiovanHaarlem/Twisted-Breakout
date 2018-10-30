using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<ForceObject> m_ForceObjects;
    private List<ForceObject> m_ForceObjectsHit;
    private List<Block> m_Blocks;

    private ForceObject m_LastUsedForceObject;
    private ForceObject m_LastUsedForceObjectHit;

    public static ObjectPool m_Instance;

	void Start ()
    {
        m_Instance = this;

        m_ForceObjects = new List<ForceObject>();
        m_ForceObjectsHit = new List<ForceObject>();
        m_Blocks = new List<Block>();

        for (int i = 0; i < 10; i++)
        {
            GameObject forceObject = Instantiate(Resources.Load<GameObject>("Prefabs/ForceObjectHit"));
            ForceObject forceObjectScript = forceObject.GetComponent<ForceObject>();
            forceObjectScript.Deactivate();
            m_ForceObjectsHit.Add(forceObjectScript);
        }

        for (int i = 0; i < 30; i++)
        {
            GameObject forceObject = Instantiate(Resources.Load<GameObject>("Prefabs/ForceObject"));
            ForceObject forceObjectScript = forceObject.GetComponent<ForceObject>();
            forceObjectScript.Deactivate();
            m_ForceObjects.Add(forceObjectScript);
        }

        for (int i = 0; i < 30; i++)
        {
            GameObject block = Instantiate(Resources.Load<GameObject>("Prefabs/Block"));
            Block blockScript = block.GetComponent<Block>();
            blockScript.Deactivate();
            m_Blocks.Add(blockScript);
        }

        m_LastUsedForceObject = m_ForceObjects[0];
        m_LastUsedForceObjectHit = m_ForceObjectsHit[0];
    }
	
    public void ActivateForceObject(Vector3 blockPosition)
    {
        for (int i = 0; i < m_ForceObjects.Count; i++)
        {
            if (!m_ForceObjects[i].gameObject.activeInHierarchy)
            {
                if (m_ForceObjects[i].GetInstanceID() != m_LastUsedForceObject.GetInstanceID())
                {
                    m_ForceObjects[i].Deactivate();
                    m_ForceObjects[i].Activate(blockPosition);
                    m_LastUsedForceObject = m_ForceObjects[i];
                    break;
                }
            }
        }
    }

    public void ActivateForceObjectHit(Vector3 blockPosition)
    {
        for (int i = 0; i < m_ForceObjectsHit.Count; i++)
        {
            if (!m_ForceObjectsHit[i].gameObject.activeInHierarchy)
            {
                if (m_ForceObjectsHit[i].GetInstanceID() != m_LastUsedForceObjectHit.GetInstanceID())
                {
                    m_ForceObjectsHit[i].Deactivate();
                    m_ForceObjectsHit[i].Activate(blockPosition);
                    m_LastUsedForceObjectHit = m_ForceObjectsHit[i];
                    break;
                }
            }
        }
    }


    public void ActivateBlock(Vector3 startPosition)
    {
        for (int i = 0; i < m_Blocks.Count; i++)
        {
            if (!m_Blocks[i].gameObject.activeInHierarchy)
            {
                m_Blocks[i].Activate(startPosition);
                break;
            }
        }
    }
}
