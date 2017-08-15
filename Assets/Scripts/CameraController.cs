using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the behaviour of the camera.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Reference to the player character.
    /// </summary>
    public Transform m_target;

    public float m_maxDistanceDelta = 1f;

    private const float MAX_HORIZONTAL_DISPLACEMENT = 10f;
    private const float MAX_VERTICAL_DISPLACEMENT = 10f;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start ()
    {
    }

    /// <summary>
    /// This function is called every fixed framerate frame.
    /// </summary>
    public void LateUpdate()
    {
        Vector3 target = m_target.transform.localPosition;
        target.x = Mathf.Clamp(target.x, -MAX_HORIZONTAL_DISPLACEMENT, MAX_HORIZONTAL_DISPLACEMENT);
        target.y = Mathf.Clamp(target.y, -MAX_VERTICAL_DISPLACEMENT, MAX_VERTICAL_DISPLACEMENT);
        float step = m_maxDistanceDelta * Time.deltaTime;
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, step);
    }
}