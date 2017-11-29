using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Database.
/// </summary>
//[System.Serializable]
//public class Database : ScriptableObject
public class Database : MonoBehaviour
{
    /// <summary>
    /// The static instance of the database.
    /// </summary>
    public static Database Instance { get; private set; }

    /// <summary>
    /// List of character data stored in database.
    /// </summary>
    public CharacterData[] m_characters;

    /// <summary>
    /// List of attack data stored in database.
    /// </summary>
    public AttackData[] m_attacks;

    /// <summary>
    /// List of item data stored in database.
    /// </summary>
    public ItemData[] m_items;

    /// <summary>
    /// List of airship data stored in database.
    /// </summary>
    public AirshipData m_airships;

    /// <summary>
    /// The dictionary of character data.
    /// </summary>
    private Dictionary<CharacterId, CharacterData> m_characterDict;

    /// <summary>
    /// The dictionary of attack data.
    /// </summary>
    private Dictionary<AttackId, AttackData> m_attackDict;

    /// <summary>
    /// The dictionary of item data.
    /// </summary>
    private Dictionary<ItemId, ItemData> m_itemDict;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        m_characterDict = new Dictionary<CharacterId, CharacterData>();
        m_attackDict = new Dictionary<AttackId, AttackData>();
        m_itemDict = new Dictionary<ItemId, ItemData>();

        foreach (CharacterData c in m_characters)
        {
            m_characterDict.Add(c.m_id, c);
        }

        foreach (AttackData a in m_attacks)
        {
            m_attackDict.Add(a.m_id, a);
        }

        foreach (ItemData i in m_items)
        {
            m_itemDict.Add(i.m_id, i);
        }
    }

    /// <summary>
    /// Gets the character data.
    /// </summary>
    /// <returns>The character data.</returns>
    /// <param name="id">Identifier.</param>
    public CharacterData GetCharacterData(CharacterId id)
    {
        if (m_characterDict.ContainsKey(id))
        {
            return m_characterDict[id];
        }

        return null;
    }

    /// <summary>
    /// Gets the attack data.
    /// </summary>
    /// <returns>The attack data.</returns>
    /// <param name="id">Identifier.</param>
    public AttackData GetAttackData(AttackId id)
    {
        if (m_attackDict.ContainsKey(id))
        {
            return m_attackDict[id];
        }

        return null;
    }

    /// <summary>
    /// Gets the item data.
    /// </summary>
    /// <returns>The item data.</returns>
    /// <param name="id">Identifier.</param>
    public ItemData GetItemData(ItemId id)
    {
        if (m_itemDict.ContainsKey(id))
        {
            return m_itemDict[id];
        }

        return null;
    }
}

/// <summary>
/// Character data.
/// </summary>
[System.Serializable]
public class CharacterData
{
    public CharacterId m_id;
    public string m_displayName;
    public Sprite m_icon;
    public int m_baseHealth;
    public int m_baseEnergy;
    public int m_baseAttack;
    public int m_baseDefense;
    public int m_baseSpeed;
    public int m_baseMovement;
}

/// <summary>
/// Attack data.
/// </summary>
[System.Serializable]
public class AttackData
{
    public AttackId m_id;
    public string m_displayName;
    public GameObject m_prefab;
    public int m_damage;
}

/// <summary>
/// Item data.
/// </summary>
[System.Serializable]
public class ItemData
{
    public ItemId m_id;
    public string m_displayName;
    public Sprite m_icon;
}

/// <summary>
/// Airship data.
/// </summary>
[System.Serializable]
public class AirshipData
{
    public AirshipId m_id;
    public string m_displayName;
    public Sprite m_icon;
}

