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
        float step = m_maxMoveDelta * Time.deltaTime;
        Vector3 screenTargetPosition = Camera.main.WorldToScreenPoint(m_aimTarget.m_farTarget.position);
        Vector3 localTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenTargetPosition.x, screenTargetPosition.y, -Camera.main.transform.localPosition.z));
        transform.position = Vector3.Lerp(transform.position, localTargetPosition, step);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);

        transform.rotation = Quaternion.LookRotation(m_aimTarget.m_farTarget.position - transform.position, Vector3.up);
        Vector3 euler = transform.localRotation.eulerAngles;
        m_eulerZ = Mathf.Lerp(m_eulerZ, -Input.GetAxis("Horizontal") * 45, Time.deltaTime * 5);
        transform.localRotation = Quaternion.Euler(euler.x, euler.y, m_eulerZ);

        if (Input.GetButtonDown("Fire1"))
        {
            Projectile projectile = Instantiate(m_projectile).GetComponent<Projectile>();
            projectile.transform.position = m_bow.transform.position;
            projectile.transform.forward = m_bow.transform.forward;
            projectile.SetTargetTag("Enemy");
//            GameObject gobj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//            gobj.transform.position = m_bow.transform.position;
//            gobj.GetComponent<Collider>().isTrigger = true;
//            Rigidbody rb = gobj.AddComponent<Rigidbody>();
//            rb.useGravity = false;
//            rb.velocity = m_bow.transform.forward * 50;
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
}

