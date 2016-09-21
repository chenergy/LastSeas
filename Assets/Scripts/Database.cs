//-----------------------------------------------------------------------
// <copyright file="Database.cs" company="Jonathan Chien">
//
// Copyright 2016 Jonathan Chien. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using UnityEngine;

/// <summary>
/// Database.
/// </summary>
[System.Serializable]
public class Database : ScriptableObject
{
    /// <summary>
    /// The static instance of the database.
    /// </summary>
    private static Database m_instance = null;
    public static Database Instance
    {
        get
        {
            return m_instance;
        }
    }

    /// <summary>
    /// List of characters stored in database.
    /// </summary>
    [SerializeField]
    private CharacterData[] m_characters;

    /// <summary>
    /// List of attacks stored in database.
    /// </summary>
    [SerializeField]
    private AttackData[] m_attacks;

    /// <summary>
    /// Initializes a new instance of the <see cref="Database"/> class.
    /// </summary>
    public Database()
    {
        m_instance = this;
        Debug.Log("constructor called");
    }

    /// <summary>
    /// Gets the character data.
    /// </summary>
    /// <returns>The character data.</returns>
    /// <param name="name">Name.</param>
    public CharacterData GetCharacterData(CharacterName name)
    {
        if (((int)name) < m_characters.Length)
        {
            return m_characters[(int)name];
        }

        return null;
    }

    /// <summary>
    /// Gets the attack data.
    /// </summary>
    /// <returns>The attack data.</returns>
    /// <param name="name">Name.</param>
    public AttackData GetAttackData(AttackName name)
    {
        if (((int)name) < m_attacks.Length)
        {
            return m_attacks[(int)name];
        }

        return null;
    }
}

/// <summary>
/// Character name.
/// </summary>
public enum CharacterName { PICARD, RIKER, LAFORGE, WORF };

/// <summary>
/// Attack name.
/// </summary>
public enum AttackName { BOMBARD };

/// <summary>
/// Character data.
/// </summary>
[System.Serializable]
public class CharacterData
{
    public string m_name;
    public bool m_unlocked;
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
    public string m_name;
    public GameObject m_prefab;
    public int m_damage;
}