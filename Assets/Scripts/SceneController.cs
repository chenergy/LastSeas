using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// The static instance of the scene controller.
    /// </summary>
    public static SceneController Instance { get; private set; }

    /// <summary>
    /// The player object.
    /// </summary>
    public Player m_player;

    /// <summary>
    /// The player spline follower.
    /// </summary>
    public SplineFollower m_playerSplineFollower;

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
    }
}

