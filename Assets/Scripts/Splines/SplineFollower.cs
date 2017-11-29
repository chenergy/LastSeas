using UnityEngine;
using System.Collections;

/// <summary>
/// Follows a given spline path.
/// </summary>
public class SplineFollower : MonoBehaviour
{
    /// <summary>
    /// The spline to follow.
    /// </summary>
    public BaseSpline m_spline;

    /// <summary>
    /// The follower speed along the spline.
    /// </summary>
    public float m_speed = 1f;

    /// <summary>
    /// Lock rotation on the x axis.
    /// </summary>
    public bool m_lockXRotation;

    /// <summary>
    /// Lock rotation on the y axis.
    /// </summary>
    public bool m_lockYRotation;

    /// <summary>
    /// Lock rotation on the z axis.
    /// </summary>
    public bool m_lockZRotation;

    /// <summary>
    /// Whether to print the distance traveled since last frame.
    /// </summary>
    public bool m_debugDeltaDistance = false;

    /// <summary>
    /// Spline follower event delegate definition.
    /// </summary>
    public delegate void SplineFollowerEvent(float tValue);

    public event SplineFollowerEvent OnStart;

    public event SplineFollowerEvent OnEnd;

    /// <summary>
    /// Occurs when t value is updated.
    /// </summary>
    public event SplineFollowerEvent OnUpdate;

    /// <summary>
    /// The current t value along the spline.
    /// </summary>
    private float m_tValue;

//    /// <summary>
//    /// The delta t per frame to move uniformly.
//    /// </summary>
//    private float m_deltaT;
//
//    /// <summary>
//    /// The number of divisions in the spline used to calculate length.
//    /// </summary>
//    private int m_divisions = 1000;

    /// <summary>
    /// The current position of the transform.
    /// </summary>
    private Vector3 m_lastPos;

    /// <summary>
    /// Whether the follower is currently following the spline.
    /// </summary>
    private bool m_followingEnabled = true;

    private bool m_isFollowing = false;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
//        float length = 0;
//        for (int i = 0; i < m_divisions; i++)
//        {
//            length += Vector3.Distance(m_spline.GetPoint((i + 1) * 1.0f / m_divisions), m_spline.GetPoint(i * 1.0f / m_divisions));
//        }
//
//        m_deltaT = 1.0f / length;

        m_lastPos = m_spline.GetPoint(0);
        SetPositionOnSpline(0);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        if (!m_followingEnabled)
        {
            return;
        }

        if (m_spline == null)
        {
            return;
        }

        Vector3 lastPos = Vector3.zero;

        if (m_tValue < 1.0f)
        {
            if (!m_isFollowing)
            {
                if (OnStart != null)
                {
                    OnStart(m_tValue);
                }

                m_isFollowing = true;
            }

//            m_tValue += Time.deltaTime * m_deltaT * m_speed;
            float increment = Time.deltaTime * m_speed;
            increment = increment / m_spline.GetDerivative(m_tValue);
            m_tValue += increment;
            m_tValue = Mathf.Clamp(m_tValue, 0.0f, 1.0f);
            lastPos = m_lastPos;
            SetPositionOnSpline(m_tValue);

            if (OnUpdate != null)
            {
                OnUpdate(m_tValue);
            }
        }
        else
        {
            if (OnEnd != null)
            {
                OnEnd(m_tValue);
            }

            FindNextSpline();
        }

        if (m_debugDeltaDistance)
        {
            Debug.Log(Vector3.Distance(lastPos, m_lastPos));
        }
    }

    /// <summary>
    /// Sets whether the follower should start following.
    /// </summary>
    /// <param name="enabled">If set to <c>true</c> enabled.</param>
    public void SetFollowingEnabled(bool enabled)
    {
        m_followingEnabled = enabled;
    }

    /// <summary>
    /// Sets the position of the follower on spline.
    /// </summary>
    /// <param name="t">T value along the spline.</param>
    private void SetPositionOnSpline(float t)
    {
        // Set the new position.
        Vector3 nextPos = m_spline.GetPoint(m_tValue);
        transform.position = m_lastPos;

        // Set the new rotation.
        Vector3 newForward = (nextPos - m_lastPos);
        Vector3 oldRotation = transform.rotation.eulerAngles;
        Vector3 newRotation = Quaternion.LookRotation(newForward).eulerAngles;
        newRotation = new Vector3(m_lockZRotation ? oldRotation.z : newRotation.z,
            m_lockYRotation ? oldRotation.y : newRotation.y,
            m_lockXRotation ? oldRotation.x : newRotation.x);
        transform.rotation = Quaternion.Euler(newRotation);
        m_lastPos = nextPos;
    }

    private void FindNextSpline()
    {
        m_isFollowing = false;
        m_spline = null;
    }
}