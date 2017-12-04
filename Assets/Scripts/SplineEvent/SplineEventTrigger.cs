using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Spline event trigger.
/// </summary>
public class SplineEventTrigger : MonoBehaviour
{
    /// <summary>
    /// The spline to trigger the event.
    /// </summary>
    public BaseSpline m_spline;

    public SplineFollower m_follower;

    public UnityEvent m_onStart;
    public UnityEvent m_onEnd;

    /// <summary>
    /// The event to trigger.
    /// </summary>
    public List<SplineEvent> m_events;

    /// <summary>
    /// Raises the enable event.
    /// </summary>
    public void OnEnable()
    {
        m_follower.OnStart += OnSplineStart;
        m_follower.OnUpdate += OnSplineUpdate;
        m_follower.OnEnd += OnSplineEnd;
    }

    /// <summary>
    /// Raises the disable event.
    /// </summary>
    public void OnDisable()
    {
        m_follower.OnStart -= OnSplineStart;
        m_follower.OnUpdate -= OnSplineUpdate;
        m_follower.OnEnd -= OnSplineEnd;
    }

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
        m_events.Sort();
    }

    private void OnSplineStart(float t)
    {
        if (m_follower.CurSpline != m_spline)
        {
            return;
        }

        m_onStart.Invoke();

        Debug.Log("start");
    }

    private void OnSplineEnd(float t)
    {
        if (m_follower.CurSpline != m_spline)
        {
            return;
        }

        m_onEnd.Invoke();

        Debug.Log("end");
    }

    private void OnSplineUpdate(float t)
    {
        if (m_follower.CurSpline != m_spline)
        {
            return;
        }

        if (m_events.Count == 0)
        {
            return;
        }

        SplineEvent first = m_events[0];
        if (t >= first.m_tValue)
        {
            first.m_event.Invoke();
            m_events.RemoveAt(0);
            Debug.Log("t = " + t.ToString());
        }
    }

    /// <summary>
    /// Raises the draw gizmos event.
    /// </summary>
    public void OnDrawGizmos()
    {
        if ((m_spline == null) || (m_follower == null) || (m_events.Count == 0))
        {
            return;
        }

        Color c = Gizmos.color;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(m_spline.GetPoint(0f), Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_spline.GetPoint(1f), Vector3.one);
        Gizmos.color = Color.white;

        for (int i = 0; i < m_events.Count; i++)
        {
            Gizmos.DrawWireCube(m_spline.GetPoint(m_events[i].m_tValue), Vector3.one);
        }

        Gizmos.color = c;
    }
}

