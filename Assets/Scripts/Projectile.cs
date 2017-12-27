using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    public int m_damage = 1;
    public float m_speed = 5f;

    private Collider m_collider;
    private LayerMask m_targetLayers;

    public void Awake()
    {
        m_collider = GetComponent<Collider>();
        Destroy(gameObject, 10f);
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
        int otherLayer = (1 << other.gameObject.layer);
        if ((otherLayer & m_targetLayers.value) == otherLayer)
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

    public void SetTarget(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        m_targetLayers = m_targetLayers | (1 << layer);
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

