using UnityEngine;
using System.Collections;

public class Turret : BaseEnemy
{
    public Transform m_base;
    public Transform m_cannon;
    public Transform m_barrel;
    public float m_baseRotationSpeed = 1f;
    public float m_cannonRotationSpeed = 1f;
    public GameObject m_projectile;
    public float m_firingInterval = 5f;

    private Transform m_target;
    private float m_timer = 0;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
//        SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
    }
	
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        if (m_target == null)
        {
            return;
        }

        Vector3 target = m_target.position + m_target.forward * 2f;
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
        Quaternion baseRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        Quaternion cannonRotation = Quaternion.Inverse(transform.rotation) * Quaternion.Euler(targetRotation.eulerAngles.x, 0, 0);
        m_base.rotation = Quaternion.RotateTowards(m_base.rotation, baseRotation, m_baseRotationSpeed * Time.deltaTime);
        m_cannon.localRotation = Quaternion.RotateTowards(m_cannon.localRotation, cannonRotation, m_cannonRotationSpeed * Time.deltaTime);

        m_timer += Time.deltaTime;

        if (m_timer > m_firingInterval)
        {
            m_timer = 0;
        }
        else
        {
            return;
        }

        if ((Quaternion.Angle(m_base.rotation, baseRotation) + Quaternion.Angle(m_cannon.localRotation, cannonRotation)) < 5)
        {
            Fire();
        }
    }

    public void SetTarget(Transform target)
    {
        m_target = target;
    }

    public void ClearTarget()
    {
        m_target = null;
    }

    public void Fire()
    {
        Projectile projectile = Instantiate(m_projectile).GetComponent<Projectile>();
        projectile.SetTarget("PlayerCollider");
        projectile.transform.position = m_barrel.transform.position;
        projectile.transform.forward = m_barrel.transform.forward;
    }
}

