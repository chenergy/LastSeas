using UnityEngine;
using System.Collections;

/// <summary>
/// Follows a given spline path.
/// </summary>
public class SplineFollower : MonoBehaviour
{
    public bool m_following = true;

    /// <summary>
    /// The spline to follow.
    /// </summary>
    public Spline3D m_spline;

    /// <summary>
    /// The follower speed along the spline.
    /// </summary>
    public float m_speed = 1f;

    public bool m_lockXRotation;
    public bool m_lockYRotation;
    public bool m_lockZRotation;

    public float TValue { get; private set; }

    private float m_deltaT;
    private int m_divisions = 1000;

    /// <summary>
    /// The current position of the transform.
    /// </summary>
    private Vector3 m_lastPos;

    /// <summary>
    /// Start this instance.
    /// </summary>
    public void Start()
    {
        float length = 0;
        for (int i = 0; i < m_divisions; i++)
        {
            length += Vector3.Distance(m_spline.GetPoint((i + 1) * 1.0f / m_divisions), m_spline.GetPoint(i * 1.0f / m_divisions));
        }

        m_deltaT = 1.0f / length;

        m_lastPos = m_spline.GetPoint(0);
//        StartCoroutine(_FollowPathRoutine());
    }

    public void Update()
    {
        if (!m_following)
        {
            return;
        }

        if (TValue < 1.0f)
        {
            TValue += Time.deltaTime * m_deltaT * m_speed;
            TValue = Mathf.Clamp(TValue, 0.0f, 1.0f);
            Vector3 nextPos = m_spline.GetPoint(TValue);
            transform.position = m_lastPos;

            Vector3 newForward = (nextPos - m_lastPos);
            //            transform.forward = new Vector3(m_lockXRotation ? transform.forward.x : newForward.x,
            //                m_lockYRotation ? transform.forward.y : newForward.y,
            //                m_lockZRotation ? transform.forward.z : newForward.z);
            Vector3 oldRotation = transform.rotation.eulerAngles;
            Vector3 newRotation = Quaternion.LookRotation(newForward).eulerAngles;
            newRotation = new Vector3(m_lockZRotation ? oldRotation.z : newRotation.z,
                m_lockYRotation ? oldRotation.y : newRotation.y,
                m_lockXRotation ? oldRotation.x : newRotation.x);
            transform.rotation = Quaternion.Euler(newRotation);
            m_lastPos = nextPos;
        }
    }

//    /// <summary>
//    /// Follows the spline path until complete.
//    /// </summary>
//    /// <returns>The coroutine.</returns>
//    private IEnumerator _FollowPathRoutine()
//    {
//        float t = 0.0f;
//        while (t < 1.0f)
//        {
//            t += Time.deltaTime * m_deltaT * m_speed;
//            t = Mathf.Clamp(t, 0.0f, 1.0f);
//            Vector3 nextPos = m_spline.GetPoint(t);
//            transform.position = m_lastPos;
//
//            Vector3 newForward = (nextPos - m_lastPos);
////            transform.forward = new Vector3(m_lockXRotation ? transform.forward.x : newForward.x,
////                m_lockYRotation ? transform.forward.y : newForward.y,
////                m_lockZRotation ? transform.forward.z : newForward.z);
//            Vector3 oldRotation = transform.rotation.eulerAngles;
//            Vector3 newRotation = Quaternion.LookRotation(newForward).eulerAngles;
//            newRotation = new Vector3(m_lockZRotation ? oldRotation.z : newRotation.z,
//                m_lockYRotation ? oldRotation.y : newRotation.y,
//                m_lockXRotation ? oldRotation.x : newRotation.x);
//            transform.rotation = Quaternion.Euler(newRotation);
//            m_lastPos = nextPos;
//
//            yield return null;
//        }
//    }
}