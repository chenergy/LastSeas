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

    /// <summary>
    /// The t value that triggers the event.
    /// </summary>
//    [Range(0f, 1f)]
//    public float m_tValue;

    public UnityEvent m_onStart;
    public UnityEvent m_onEnd;

    /// <summary>
    /// The event to trigger.
    /// </summary>
    public List<SplineEvent> m_events;

//    private bool m_triggered = false;
//    private bool m_started = false;

    /// <summary>
    /// The attached collider.
    /// </summary>
//    private Collider m_collider;

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
//        m_collider = GetComponent<Collider>();
//        transform.position = m_spline.GetPoint(m_tValue);
    }
	
//    /// <summary>
//    /// Update is called once per frame.
//    /// </summary>
//    public void Update()
//    {
//        if (m_triggered)
//        {
//            return;
//        }
//
//        if (m_follower.m_spline == m_spline)
//        {
//            
//        }
//    }

    private void OnSplineStart(float t)
    {
        if (m_follower.m_spline != m_spline)
        {
            return;
        }

        m_onStart.Invoke();

        Debug.Log("start");
    }

    private void OnSplineEnd(float t)
    {
        if (m_follower.m_spline != m_spline)
        {
            return;
        }

        m_onEnd.Invoke();

        Debug.Log("end");
    }

    private void OnSplineUpdate(float t)
    {
        if (m_follower.m_spline != m_spline)
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
        Gizmos.DrawWireCube(m_follower.m_spline.GetPoint(0f), Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_follower.m_spline.GetPoint(1f), Vector3.one);
        Gizmos.color = Color.white;

        for (int i = 0; i < m_events.Count; i++)
        {
            Gizmos.DrawWireCube(m_spline.GetPoint(m_events[i].m_tValue), Vector3.one);
        }

        Gizmos.color = c;
    }

    /// <summary>
    /// Raises the trigger enter event.
    /// </summary>
    /// <param name="other">Other collider.</param>
//    public void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Player")
//        {
//            m_collider.enabled = false;
//            m_event.Invoke();
//        }
//    }
}

[System.Serializable]
public class SplineEvent : IComparable<SplineEvent>
{
    [Range(0f, 1f)]
    public float m_tValue;
    public UnityEvent m_event;

    public int CompareTo(SplineEvent other)
    {
        if (other == null)
        {
            return 1;
        }

//        SplineEventt other = obj as SplineEventt;
        if (other != null)
        {
            return m_tValue.CompareTo(other.m_tValue);
        }
        else
        {
            throw new ArgumentException("Object is not a SplineEvent");
        }
    }
}

