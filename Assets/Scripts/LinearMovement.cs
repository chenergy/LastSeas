using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy airship in the scene.
/// </summary>
public class LinearMovement : MonoBehaviour
{
    /// <summary>
    /// The animator attached to the ship objects.
    /// </summary>
    public Animator m_animator;

    /// <summary>
    /// The movement speed of the airship.
    /// </summary>
    public float m_topSpeed = 1;

    /// <summary>
    /// Internal current speed for movement.
    /// </summary>
    protected float m_curSpeed;

    /// <summary>
    /// The acceleration to top speed.
    /// </summary>
    //    public Vector3 m_acceleration = 0.1f;
    public float m_acceleration = 0.1f;

    /// <summary>
    /// This function is called every fixed framerate frame.
    /// </summary>
    public void FixedUpdate()
    {
        float displacement = m_curSpeed * Time.deltaTime + (0.5f * m_acceleration * Time.deltaTime * Time.deltaTime);
        transform.position += transform.forward * displacement;
        m_curSpeed = Mathf.Min(m_topSpeed, m_curSpeed + m_acceleration * Time.deltaTime);
    }
}

