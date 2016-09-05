using UnityEngine;
using System.Collections;

/// <summary>
/// Player character in the scene.
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// The list of characters onboard the ship.
    /// </summary>
    public CharacterName[] m_charactersOnboard;

    /// <summary>
    /// The movement speed of the airship.
    /// </summary>
    public float m_topSpeed = 1;

    /// <summary>
    /// The acceleration to top speed.
    /// </summary>
    [Range(0f, 1f)]
    public float m_acceleration = 0.1f;

    /// <summary>
    /// The layer that register the ground position to move to.
    /// </summary>
    public LayerMask m_groundHitLayer;

    /// <summary>
    /// The animator attached to the ship objects.
    /// </summary>
    public Animator m_animator;

    /// <summary>
    /// Internal current speed for movement.
    /// </summary>
    private float m_curSpeed;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
	
    }
	
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_groundHitLayer))
            {
                StopAllCoroutines();
                StartCoroutine(_MoveToPointRoutine(hit.point));
            }
        }
    }

    /// <summary>
    /// Moves to the point.
    /// </summary>
    /// <returns>The coroutine.</returns>
    /// <param name="point">The point to move to.</param>
    private IEnumerator _MoveToPointRoutine(Vector3 point)
    {
        transform.forward = point - transform.position;

        float t = 0;
        float distance = (point - transform.position).magnitude;
//        Vector3 direction = (point - transform.position).normalized;
        Vector3 start = transform.position;

        while (t < distance)
        {
            m_curSpeed = Mathf.Lerp(m_curSpeed, m_topSpeed, m_acceleration);
            transform.position = Vector3.Lerp(start, point, t / distance);
            m_animator.SetFloat("Speed", m_curSpeed);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime * m_curSpeed;
        }

        transform.position = point;
        m_animator.SetFloat("Speed", 0);
    }
}

