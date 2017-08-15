using UnityEngine;
using System.Collections;

/// <summary>
/// Player aim target.
/// </summary>
public class AimTarget : MonoBehaviour
{
    public Transform m_nearTarget;
    public Transform m_farTarget;
    public float m_screenMoveScale = 0.05f;
    public float m_screenReturnScale = 0.01f;

    public Vector2 TargetViewportPosition { get; private set; }

    public const float FAR_TARGET_MAX_DISTANCE = 100f;
    public const float FAR_TARGET_SCALE = 1f;
    public const float NEAR_TARGET_MAX_DISTANCE = 50f;
    public const float NEAR_TARGET_SCALE = 0.5f;

    private bool m_hInvert = false;
    private bool m_vInvert = false;
    private Player m_player;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
        TargetViewportPosition = new Vector2(0.5f, 0.5f);
        m_player = FindObjectOfType<Player>();
    }
	
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        int hMult = (m_hInvert) ? -1 : 1;

        float vAxis = Input.GetAxis("Vertical");
        int vMult = (m_vInvert) ? -1 : 1;

        if ((hAxis != 0) || (vAxis != 0))
        {
            // Move to player input point.
            TargetViewportPosition += new Vector2(hAxis * hMult, vAxis * vMult) * m_screenMoveScale;
        }
        else
        {
            // Return to center point.
            Vector2 target = new Vector2 (0.5f, 0.5f);
            TargetViewportPosition = Vector2.MoveTowards(TargetViewportPosition, target, m_screenReturnScale);
        }

        TargetViewportPosition = new Vector2(Mathf.Clamp01(TargetViewportPosition.x),
            Mathf.Clamp01(TargetViewportPosition.y));
        
        m_farTarget.position = Camera.main.ViewportToWorldPoint(new Vector3(TargetViewportPosition.x, TargetViewportPosition.y, FAR_TARGET_MAX_DISTANCE));
        m_nearTarget.position = m_player.transform.position + (m_farTarget.position - m_player.transform.position) * 0.25f;
    }
}

