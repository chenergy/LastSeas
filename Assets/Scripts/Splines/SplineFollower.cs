using UnityEngine;
using System.Collections;

/// <summary>
/// Follows a given spline path.
/// </summary>
public class SplineFollower : MonoBehaviour
{
    /// <summary>
    /// The spline controller.
    /// </summary>
    public SplineController m_splineController;

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

    /// <summary>
    /// Spline follower events.
    /// </summary>
    public event SplineFollowerEvent OnStart;
    public event SplineFollowerEvent OnEnd;
    public event SplineFollowerEvent OnUpdate;

    /// <summary>
    /// The spline to follow.
    /// </summary>
    public BaseSpline CurSpline { get; private set; }

    /// <summary>
    /// The current t value along the spline.
    /// </summary>
    private float m_tValue;

    /// <summary>
    /// The current position of the transform.
    /// </summary>
    private Vector3 m_lastPos;

    /// <summary>
    /// Whether the follower is allowed to follow a spline.
    /// </summary>
    private bool m_followingEnabled = true;

    /// <summary>
    /// Whether the follower is currently following the spline.
    /// </summary>
    private bool m_isFollowing = false;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    public void Awake()
    {
        CurSpline = m_splineController.m_treeRoot.m_spline;
    }

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
        m_lastPos = CurSpline.GetPoint(0);
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

        if (CurSpline == null)
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

            float increment = Time.deltaTime * m_speed;
            increment = increment / CurSpline.GetDerivative(m_tValue);
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

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(m_splineController.m_treeRoot.m_spline.GetPoint(m_tValue), 1f);
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
        Vector3 nextPos = CurSpline.GetPoint(m_tValue);
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
        m_tValue = 0;
//        CurSpline = null;
        CurSpline = m_splineController.FindNextSpline();
    }
}