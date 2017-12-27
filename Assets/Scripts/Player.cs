using UnityEngine;
using System.Collections;

/// <summary>
/// Airship controlled by the player in the scene.
/// </summary>
public class Player : MonoBehaviour, IDamageable
{
    public AimTarget m_aimTarget;

    /// <summary>
    /// The animator attached to the ship objects.
    /// </summary>
    public Animator m_animator;
    public Transform m_bow;
    public Transform m_port;
    public Transform m_starboard;
    public GameObject m_projectile;
    public float m_maxMoveDelta = 0.1f;
    public int m_curhealth = 10;
    public int m_totalHealth = 10;

    private float m_eulerZ;
    private MissionUI m_ui;
    private Coroutine m_speedupRoutine;
    private bool m_isSpeedup = false;

    public void Start()
    {
        m_ui = FindObjectOfType<MissionUI>();
        m_animator.SetFloat("Speed", 1f);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Set new position based on aim target.
        float step = m_maxMoveDelta * Time.deltaTime;
        Vector3 screenTargetPosition = Camera.main.WorldToScreenPoint(m_aimTarget.m_farTarget.position);
        Vector3 localTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenTargetPosition.x, screenTargetPosition.y, -Camera.main.transform.localPosition.z));
        transform.position = Vector3.Lerp(transform.position, localTargetPosition, step);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);

        // Set new rotation based on aim target.
        transform.rotation = Quaternion.LookRotation(m_aimTarget.m_farTarget.position - transform.position, Vector3.up);
        Vector3 euler = transform.localRotation.eulerAngles;
        m_eulerZ = Mathf.Lerp(m_eulerZ, -Input.GetAxis("Horizontal") * 45, Time.deltaTime * 5);
        transform.localRotation = Quaternion.Euler(euler.x, euler.y, m_eulerZ);

        // Fire weapon.
        if (Input.GetButtonDown("Fire1"))
        {
            Projectile projectile = Instantiate(m_projectile).GetComponent<Projectile>();
            projectile.transform.position = m_bow.transform.position;
            projectile.transform.forward = m_bow.transform.forward;
            projectile.SetTarget("EnemyCollider");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (m_speedupRoutine != null)
            {
                StopCoroutine(m_speedupRoutine);
            }

            m_speedupRoutine = StartCoroutine(_SpeedupRoutine());
        }
    }

    public void TakeDamage(int damage)
    {
        m_curhealth -= damage;
        if (m_curhealth <= 0)
        {
            m_curhealth = 0;
            DestroySelf();
        }

        m_ui.SetPlayerHealthFill(1.0f * m_curhealth / m_totalHealth);
    }

    public void DestroySelf()
    {
        Debug.Log("Player Destroyed");
    }

    private IEnumerator _SpeedupRoutine()
    {
        m_isSpeedup = true;
        m_maxMoveDelta *= 10;
        m_aimTarget.m_screenMoveScale *= 10;
        yield return new WaitForSeconds(0.5f);
        m_isSpeedup = false;
        m_maxMoveDelta /= 10;
        m_aimTarget.m_screenMoveScale /= 10;
        m_speedupRoutine = null;
    }
}

