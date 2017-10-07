using UnityEngine;
using System.Collections;

public class MissionCharacter
{
    private CharacterId m_id;
    private int m_experience;
    private int m_curHealth;
    private int m_curEnergy;

    // Use this for initialization
    public MissionCharacter(CharacterId id, int experience, int health, int energy)
    {
        m_id = id;
        m_experience = experience;
        m_curHealth = health;
        m_curEnergy = energy;
    }
}

