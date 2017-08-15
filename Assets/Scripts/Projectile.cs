using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    public int m_damage = 1;
    public float m_speed = 5f;

    private Collider m_collider;
    private string m_targetTag;

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
        if (other.tag == m_targetTag)
        {
            IDamageable target = other.GetComponent<IDamageable>();
            target.TakeDamage(m_damage);
            DestroySelf();
        }
    }

    public void IgnoreCollision(Collider other)
    {
        Physics.IgnoreCollision(m_collider, other);
    }

    public void SetTargetTag(string tag)
    {
        m_targetTag = tag;
    }

    private void Move()
    {
        transform.position += transform.forward * m_speed * Time.deltaTime;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}

