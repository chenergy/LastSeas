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
    /// Static reference to any loaded saved data.
    /// </summary>
    private static SaveFile LoadedSaveFile { get; set; }

    public static bool GetCurrentCharacters(out CharacterId[] data)
    {
        data = null;
        if (LoadedSaveFile == null)
        {
            Debug.Log("No loaded save file.");
            return false;
        }

        data = LoadedSaveFile.m_curCharacters;
        return true;
    }

    /// <summary>
    /// Gets the character saved data.
    /// </summary>
    /// <returns>The character saved data.</returns>
    /// <param name="name">Name of the character.</param>
    public static bool GetCharacterSavedData(CharacterId name, out CharacterSaveData data)
    {
        data = null;
        if (LoadedSaveFile == null)
        {
            Debug.Log("No loaded save file.");
            return false;
        }

        for (int i = 0; i < LoadedSaveFile.m_savedCharacterData.Length; i++)
        {
            CharacterSaveData character = LoadedSaveFile.m_savedCharacterData[i];
            if (character.m_id == name)
            {
                data = character;
                return true;
            }
        }

        Debug.LogError(string.Format("Could not find character with name {0}", name.ToString()));
        return false;
    }

    public static bool GetInventoryData(out InventorySaveData data)
    {
        data = null;
        if (LoadedSaveFile == null)
        {
            Debug.Log("No loaded save file.");
            return false;
        }

        data = LoadedSaveFile.m_inventory;
        return true;
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
        CharacterSaveData character;
        if (GetCharacterSavedData(name, out character))
        {
            Debug.LogError(string.Format("Could not find character with name {0}", name.ToString()));
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
        Debug.Log("Saving.");
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
        Debug.Log("Loading.");
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
    /// <summary>
    /// List of saved character data.
    /// </summary>
    public CharacterSaveData[] m_savedCharacterData;

    /// <summary>
    /// List of items in inventory.
    /// </summary>
    public InventorySaveData m_inventory;

    /// <summary>
    /// The DateTime of the last saved data.
    /// </summary>
    public DateTime m_dateLastSaved;

    /// <summary>
    /// The amount of currency assigned to the player.
    /// </summary>
    public int m_currency;

    public CharacterId[] m_curCharacters;
    public ItemId[] m_curItems;
    public AirshipId m_curShip;

    public SaveFile()
    {
        m_inventory = new InventorySaveData();

        m_savedCharacterData = new CharacterSaveData[4];
        m_savedCharacterData[0] = new CharacterSaveData(CharacterId.PICARD);
        m_savedCharacterData[1] = new CharacterSaveData(CharacterId.LAFORGE);
        m_savedCharacterData[2] = new CharacterSaveData(CharacterId.RIKER);
        m_savedCharacterData[3] = new CharacterSaveData(CharacterId.WORF);

        m_currency = 0;
        m_dateLastSaved = DateTime.Today;

        m_curCharacters = new CharacterId[]
        {
            CharacterId.PICARD,
            CharacterId.LAFORGE,
            CharacterId.RIKER,
            CharacterId.WORF
        };
        m_curItems = new ItemId[2];
        m_curShip = AirshipId.BASIC;
    }
}

/// <summary>
/// Character saved data.
/// </summary>
[System.Serializable]
public class CharacterSaveData
{
    public CharacterId m_id;
    public int m_experience;
    public int m_curHealth;
    public int m_curEnergy;

    public CharacterSaveData()
    {
    }

    public CharacterSaveData(CharacterId id)
    {
        m_id = id;
    }
}

/// <summary>
/// Inventory save data.
/// </summary>
[System.Serializable]
public class InventorySaveData
{
    public int[] m_items;

    public InventorySaveData()
    {
        m_items = new int[2];
    }
}

