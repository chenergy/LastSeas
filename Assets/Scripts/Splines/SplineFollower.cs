using UnityEngine;
using System.Collections;

/// <summary>
/// Follows a given spline path.
/// </summary>
public class SplineFollower : MonoBehaviour
{
    /// <summary>
    /// The follower speed along the spline.
    /// </summary>
    public float m_speed = 1f;

    /// <summary>
    /// The spline to follow.
    /// </summary>
    public Spline3D m_spline;

    /// <summary>
    /// The current position of the transform.
    /// </summary>
    private Vector3 m_lastPos;

    /// <summary>
    /// Start this instance.
    /// </summary>
    public void Start()
    {
        m_lastPos = m_spline.GetPoint(0);
        StartCoroutine(_FollowPathRoutine());
    }

    /// <summary>
    /// Follows the spline path until complete.
    /// </summary>
    /// <returns>The coroutine.</returns>
    private IEnumerator _FollowPathRoutine()
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * m_speed;
            t = Mathf.Clamp(t, 0.0f, 1.0f);
            Vector3 nextPos = m_spline.GetPoint(t);
            transform.position = m_lastPos;
            transform.forward = (nextPos - m_lastPos);
            m_lastPos = nextPos;

            yield return null;
        }
    }
}