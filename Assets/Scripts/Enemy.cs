using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int m_health = 1;

    public void TakeDamage(int health)
    {
        m_health -= health;
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

