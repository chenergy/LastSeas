using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy airship in the scene.
/// </summary>
public class Enemy : Airship
{
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

