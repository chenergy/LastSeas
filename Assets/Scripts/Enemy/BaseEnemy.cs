using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour, IDamageable
{
    public int m_health = 1;

    public void TakeDamage(int damage)
    {
        m_health -= damage;
        if (m_health <= 0)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

