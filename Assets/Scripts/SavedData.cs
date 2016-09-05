//-----------------------------------------------------------------------
// <copyright file="SavedData.cs" company="Jonathan Chien">
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
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// Saved data.
/// </summary>
[System.Serializable]
public class SavedData
{
    /// <summary>
    /// List of saved character data.
    /// </summary>
    public CharacterSavedData[] m_savedCharacterData;

    /// <summary>
    /// The DateTime of the last saved data.
    /// </summary>
    public DateTime m_dateLastSaved;

    /// <summary>
    /// The amount of currency assigned to the player.
    /// </summary>
    public int m_currency;

    /// <summary>
    /// Static reference to any loaded saved data.
    /// </summary>
    private static SavedData m_savedData = new SavedData();
    public static SavedData LastSavedData
    {
        get { return m_savedData; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SavedData"/> class.
    /// </summary>
    public SavedData()
    {
        m_savedCharacterData = new CharacterSavedData[4];
        for (int i = 0; i < m_savedCharacterData.Length; i++)
        {
            m_savedCharacterData[i] = new CharacterSavedData();
        }
    }

    /// <summary>
    /// Gets the character saved data.
    /// </summary>
    /// <returns>The character saved data.</returns>
    /// <param name="name">Name of the character.</param>
    public CharacterSavedData GetCharacterSavedData(CharacterName name)
    {
        if (((int)name) < m_savedCharacterData.Length)
        {
            return m_savedCharacterData[(int)name];
        }

        return null;
    }

    /// <summary>
    /// Updates the character data of given name with parameters.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="experience">Experience.</param>
    /// <param name="curHealth">Current health.</param>
    /// <param name="curEnergy">Current energy.</param>
    public void UpdateCharacterData(CharacterName name, int experience, int curHealth, int curEnergy)
    {
        if (((int)name) < m_savedCharacterData.Length)
        {
            m_savedCharacterData[(int)name].m_experience = experience;
            m_savedCharacterData[(int)name].m_curHealth = curHealth;
            m_savedCharacterData[(int)name].m_curEnergy = curEnergy;
        }
    }

    /// <summary>
    /// Save this instance.
    /// </summary>
    public static void Save()
    {
        m_savedData.m_dateLastSaved = DateTime.Now;
        XmlSerializer serializer = new XmlSerializer(typeof(SavedData));
        FileStream file = File.Create(Application.persistentDataPath + "/savedData.gd");
        serializer.Serialize(file, m_savedData);
        file.Close();
        Debug.Log("Saved: " + Application.persistentDataPath);
    }

    /// <summary>
    /// Load this instance.
    /// </summary>
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedData.gd"))
        {
            XmlSerializer bf = new XmlSerializer(typeof(SavedData));
            FileStream file = File.Open(Application.persistentDataPath + "/savedData.gd", FileMode.Open);
            m_savedData = (SavedData)bf.Deserialize(file);
            file.Close();
            Debug.Log("Loaded: " + Application.persistentDataPath);
        }
        else
        {
            Save();
            Load();
        }
    }
}

/// <summary>
/// Character saved data.
/// </summary>
[System.Serializable]
public class CharacterSavedData
{
    public int m_experience;
    public int m_curHealth;
    public int m_curEnergy;
}