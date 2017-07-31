using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract airship class containing base behaviours.
/// </summary>
public abstract class Airship : MonoBehaviour
{
    /// <summary>
    /// The list of characters onboard the ship.
    /// </summary>
    public CharacterId[] m_charactersOnboard;

    /// <summary>
    /// The movement speed of the airship.
    /// </summary>
    public float m_topSpeed = 1;

    /// <summary>
    /// The acceleration to top speed.
    /// </summary>
//    public Vector3 m_acceleration = 0.1f;
    public float m_acceleration = 0.1f;

    /// <summary>
    /// The animator attached to the ship objects.
    /// </summary>
    public Animator m_animator;

    /// <summary>
    /// Internal current speed for movement.
    /// </summary>
    protected float m_curSpeed;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
    }

    ///// <summary>
    ///// This function is called every fixed framerate frame.
    ///// </summary>
    //public virtual void FixedUpdate()
    //{
    //    float displacement = m_curSpeed * Time.deltaTime + (0.5f * m_acceleration * Time.deltaTime * Time.deltaTime);
    //    transform.position += transform.forward * displacement;
    //    m_curSpeed = Mathf.Min(m_topSpeed, m_curSpeed + m_acceleration * Time.deltaTime);
    //}

    /// <summary>
    /// Moves to the point.
    /// </summary>
    /// <returns>The coroutine.</returns>
    /// <param name="point">The point to move to.</param>
//    protected IEnumerator _MoveToPointRoutine(Vector3 point)
//    {
//        transform.forward = point - transform.position;
//
//        float t = 0;
//        float distance = (point - transform.position).magnitude;
//        Vector3 start = transform.position;
//
//        while (t < distance)
//        {
//            m_curSpeed = Mathf.Lerp(m_curSpeed, m_topSpeed, m_acceleration);
//            transform.position = Vector3.Lerp(start, point, t / distance);
//            m_animator.SetFloat("Speed", m_curSpeed);
//
//            t += Time.deltaTime * m_curSpeed;
//            yield return new WaitForEndOfFrame();
//        }
//
//        transform.position = point;
//        m_curSpeed = 0;
//        m_animator.SetFloat("Speed", 0);
//    }
}

