using System.Collections;
using UnityEngine;

/// <summary>
/// During battle, character info taken from database and saved player data.
/// </summary>
public class MissionController : MonoBehaviour
{
    public MissionUI m_missionUI;

    private MissionCharacter[] m_character;
    private MissionInventory m_inventory;

    public void Start()
    {
        CharacterId[] curCharacters;
        if (LoadSaveData.GetCurrentCharacters(out curCharacters))
        {
            m_character = new MissionCharacter[curCharacters.Length];
            for (int i = 0; i < curCharacters.Length; i++)
            {
                CharacterId id = curCharacters[i];
                CharacterSaveData csd;
                if (LoadSaveData.GetCharacterSavedData(id, out csd))
                {
                    m_character[i] = new MissionCharacter(csd.m_id, csd.m_experience, csd.m_curHealth, csd.m_curEnergy);
                }
            }
        }

        InventorySaveData isd;
        if (LoadSaveData.GetInventoryData(out isd))
        {
            m_inventory = new MissionInventory(isd.m_items);
            m_missionUI.SetUpdateItems(m_inventory.m_equippedItems);
        }
    }

//    public bool m_playerControlled;
//    public Airship m_airship;
//    public string m_name;
//    public Sprite m_icon;
//    public int m_experience;
//    public int m_curHealth;
//    public int m_curEnergy;
//    public int m_baseSpeed;
//    public int m_baseMovement;

//    public MissionCharacter(CharacterId name, Airship airship, bool playerControlled)
//    {
//        m_playerControlled = playerControlled;
//        m_airship = airship;
//
//        CharacterData databaseChar = Database.Instance.GetCharacterData(name);
//        m_name = databaseChar.m_name;
//        m_icon = databaseChar.m_icon;
//        m_baseSpeed = databaseChar.m_baseSpeed;
//        m_baseMovement = databaseChar.m_baseMovement;

//        SaveDataCharacter savedChar = LoadSaveData.GetCharacterSavedData(name);
//        m_experience = savedChar.m_experience;
//        m_curHealth = savedChar.m_curHealth;
//        m_curEnergy = savedChar.m_curEnergy;
//    }
}