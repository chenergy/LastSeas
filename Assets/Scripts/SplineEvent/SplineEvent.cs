using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

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

