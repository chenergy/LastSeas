using System.Collections;
using UnityEngine;

/// <summary>
/// Game manager.
/// </summary>
public class GameManager : MonoBehaviour 
{
    /// <summary>
    /// The database containing references to objects needed to load during runtime.
    /// </summary>
//    public Database m_database;

    /// <summary>
    /// The instance of the GameManager, available statically.
    /// </summary>
    private static GameManager m_instance = null;

    /// <summary>
    /// Gets the GameManager instance.
    /// </summary>
    /// <value>The instance.</value>
    public static GameManager Instance
    {
        get { return m_instance; }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    public void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
//        Debug.Log(m_database.GetCharacterData(CharacterName.BOB).m_name);
//        SavedData.LastSavedData.Load();
//        Debug.Log(SavedData.LastSavedData.GetCharacterSavedData(CharacterName.BOB).m_experience);
//        SavedData.LastSavedData.UpdateCharacterData(CharacterName.BOB, 100, 10, 10);
//        LoadSaveData.Save();
//        Debug.Log(Database.Instance.GetCharacterData(CharacterName.PICARD).m_name);

//        LoadSaveData.Save();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
    }
}
