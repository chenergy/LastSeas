using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    public int m_damage = 1;
    public float m_speed = 5f;

    private Collider m_collider;

    public void Awake()
    {
        m_collider = GetComponent<Collider>();
    }

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
        Move();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(m_damage);
        }

        DestroySelf();
    }

    public void Move()
    {
        transform.position += transform.forward * m_speed * Time.deltaTime;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void IgnoreCollision(Collider other)
    {
        Physics.IgnoreCollision(m_collider, other);
    }
}

