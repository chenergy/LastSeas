using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public class SplineEvent : MonoBehaviour
{
    public Spline3D m_spline;
    public SplineFollower m_follower;

    [Header("Start Event")]
    [Range(0f, 1f)]
    public float m_startTValue = 0;
    public UnityEvent m_startEvent;

    [Header("End Event")]
    [Range(0f, 1f)]
    public float m_endTValue = 1f;
    public UnityEvent m_endEvent;

    private bool triggered = false;
    private bool completed = false;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
    }
	
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Skip if completed.
        if (completed)
        {
            return;
        }

        if (!triggered)
        {
            // Wait for staring t value.
            if (m_follower.TValue > m_startTValue)
            {
                triggered = true;
                OnStart();
            }
        }
        else
        {
            // Wait for ending t value.
            if (m_follower.TValue > m_endTValue)
            {
                completed = true;
                OnEnd();
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        try 
        {
            Color c = Gizmos.color;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(m_spline.GetPoint(m_startTValue), Vector3.one);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(m_spline.GetPoint(m_endTValue), Vector3.one);
            Gizmos.color = c;
        }
        catch (NullReferenceException e) { }
    }

    public void OnStart()
    {
        m_startEvent.Invoke();
    }

    public void OnEnd()
    {
        m_endEvent.Invoke();
    }
}

