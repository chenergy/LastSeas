using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public abstract class SplineEvent : MonoBehaviour
{
    public Spline3D m_spline;

    public SplineFollower m_follower;

    public UnityEvent m_event;

    [Range(0f, 1f)]
    public float m_tValueToTrigger = 0;

    private bool m_triggered = false;

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
        if (m_triggered)
        {
            return;
        }

        if (m_follower.TValue > m_tValueToTrigger)
        {
            m_triggered = true;
            OnTValuePassed();
        }
    }

    public void OnDrawGizmos()
    {
        try 
        {
            Gizmos.DrawSphere(m_spline.GetPoint(m_tValueToTrigger), 1f);
        }
        catch (NullReferenceException e) { }
    }

    public abstract void OnTValuePassed();
}

