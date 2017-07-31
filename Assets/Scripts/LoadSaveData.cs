using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// Saved data.
/// </summary>
//[System.Serializable]
public class LoadSaveData
{
    /// <summary>
    /// List of saved character data.
    /// </summary>
//    public CharacterSavedData[] m_savedCharacterData;

    /// <summary>
    /// The DateTime of the last saved data.
    /// </summary>
//    public DateTime m_dateLastSaved;

    /// <summary>
    /// The amount of currency assigned to the player.
    /// </summary>
//    public int m_currency;

    /// <summary>
    /// Static reference to any loaded saved data.
    /// </summary>
//    private static LoadSaveData m_savedData = new LoadSaveData();
//    public static LoadSaveData LastSavedData
//    {
//        get { return m_savedData; }
//    }
    public static SaveFile LoadedSaveFile { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SavedData"/> class.
    /// </summary>
//    public LoadSaveData()
//    {
//        m_savedCharacterData = new CharacterSavedData[4];
//        for (int i = 0; i < m_savedCharacterData.Length; i++)
//        {
//            m_savedCharacterData[i] = new CharacterSavedData();
//        }
//    }

    /// <summary>
    /// Gets the character saved data.
    /// </summary>
    /// <returns>The character saved data.</returns>
    /// <param name="name">Name of the character.</param>
    public static SaveDataCharacter GetCharacterSavedData(CharacterId name)
    {
//        if (((int)name) < m_savedCharacterData.Length)
//        {
//            return m_savedCharacterData[(int)name];
//        }
        if (LoadedSaveFile != null)
        {
            Debug.Log("No loaded save file.");
            return null;
        }

        for (int i = 0; i < LoadedSaveFile.m_savedCharacterData.Length; i++)
        {
            SaveDataCharacter character = LoadedSaveFile.m_savedCharacterData[i];
            if (character.m_id == name)
            {
                return character;
            }
        }

        Debug.Log("Could not find character with name");
        return null;
    }

    /// <summary>
    /// Updates the character data of given name with parameters.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="experience">Experience.</param>
    /// <param name="curHealth">Current health.</param>
    /// <param name="curEnergy">Current energy.</param>
    public static void UpdateCharacterData(CharacterId name, int experience, int curHealth, int curEnergy)
    {
        
//        if (((int)name) < m_savedCharacterData.Length)
        SaveDataCharacter character = GetCharacterSavedData(name);
        if (character == null)
        {
            return;
        }

        character.m_experience = experience;
        character.m_curHealth = curHealth;
        character.m_curEnergy = curEnergy;
    }

    /// <summary>
    /// Save this instance.
    /// </summary>
    public static void Save()
    {
        if (LoadedSaveFile == null)
        {
            Debug.Log("No save file. Creating new.");
            LoadedSaveFile = new SaveFile();
        }

        LoadedSaveFile.m_dateLastSaved = DateTime.Now;
        XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));
        FileStream file = File.Create(Application.persistentDataPath + "/savedData.gd");
        serializer.Serialize(file, LoadedSaveFile);
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
            XmlSerializer bf = new XmlSerializer(typeof(SaveFile));
            FileStream file = File.Open(Application.persistentDataPath + "/savedData.gd", FileMode.Open);
            LoadedSaveFile = (SaveFile)bf.Deserialize(file);
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

[System.Serializable]
public class SaveFile
{
    public SaveFile()
    {
        m_savedCharacterData = new SaveDataCharacter[4];
        m_savedCharacterData[0] = new SaveDataCharacter(CharacterId.PICARD);
        m_savedCharacterData[1] = new SaveDataCharacter(CharacterId.LAFORGE);
        m_savedCharacterData[2] = new SaveDataCharacter(CharacterId.RIKER);
        m_savedCharacterData[3] = new SaveDataCharacter(CharacterId.WORF);
    }

    /// <summary>
    /// List of saved character data.
    /// </summary>
    public SaveDataCharacter[] m_savedCharacterData;

    /// <summary>
    /// The DateTime of the last saved data.
    /// </summary>
    public DateTime m_dateLastSaved;

    /// <summary>
    /// The amount of currency assigned to the player.
    /// </summary>
    public int m_currency;
}

/// <summary>
/// Character saved data.
/// </summary>
[System.Serializable]
public class SaveDataCharacter
{
    public SaveDataCharacter(CharacterId id)
    {
        m_id = id;
    }

    public CharacterId m_id;
    public int m_experience;
    public int m_curHealth;
    public int m_curEnergy;
}