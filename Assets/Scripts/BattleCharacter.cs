using System.Collections;
using UnityEngine;

/// <summary>
/// During battle, character info taken from database and saved player data.
/// </summary>
public class BattleCharacter
{
    public bool m_playerControlled;
    public Airship m_airship;
    public string m_name;
    public Sprite m_icon;
    public int m_experience;
    public int m_curHealth;
    public int m_curEnergy;
    public int m_baseSpeed;
    public int m_baseMovement;

    public BattleCharacter(CharacterId name, Airship airship, bool playerControlled)
    {
        m_playerControlled = playerControlled;
        m_airship = airship;

        CharacterData databaseChar = Database.Instance.GetCharacterData(name);
        m_name = databaseChar.m_name;
        m_icon = databaseChar.m_icon;
        m_baseSpeed = databaseChar.m_baseSpeed;
        m_baseMovement = databaseChar.m_baseMovement;

//        SaveDataCharacter savedChar = LoadSaveData.GetCharacterSavedData(name);
//        m_experience = savedChar.m_experience;
//        m_curHealth = savedChar.m_curHealth;
//        m_curEnergy = savedChar.m_curEnergy;
    }
}