using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the behaviour of the camera.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// The speed that the camera should follow at, from 0 to 1.
    /// </summary>
//    [Range(0f, 1f)]
//    public float m_followSpeed = 0.5f;

    /// <summary>
    /// Reference to the player character.
    /// </summary>
    public Transform m_target;

    public float m_lerpSpeed = 1f;

    private const float MAX_HORIZONTAL_DISPLACEMENT = 10f;
    private const float MAX_VERTICAL_DISPLACEMENT = 10f;

    /// <summary>
    /// Reference to the cursor.
    /// </summary>
//    public RectTransform m_primaryCursor;
//
//    public RectTransform m_secondaryCursor;
//
//    /// <summary>
//    /// Reference to the canvas containing the cursor for screen size.
//    /// </summary>
//    public RectTransform m_canvasTransform;

    //public Spline3D m_spline;

    //private float m_timer;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start ()
    {
    }

    /// <summary>
    /// This function is called every fixed framerate frame.
    /// </summary>
    public void LateUpdate()
    {
        Vector3 target = m_target.transform.localPosition;
        target.x = Mathf.Clamp(target.x, -MAX_HORIZONTAL_DISPLACEMENT, MAX_HORIZONTAL_DISPLACEMENT);
        target.y = Mathf.Clamp(target.y, -MAX_VERTICAL_DISPLACEMENT, MAX_VERTICAL_DISPLACEMENT);
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * m_lerpSpeed);
        //float width = m_canvasTransform.sizeDelta.x;
        //float height = m_canvasTransform.sizeDelta.y;
//        float width = Screen.width;
//        float height = Screen.height;
//        Vector3 center = new Vector3(width / 2, height / 2, 0);
        //m_cursor.anchoredPosition = new Vector2(Input.GetAxis("Horizontal") * width / 2,
        //                                        Input.GetAxis("Vertical") * height / 2);
//        Vector3 primaryPos = Camera.main.WorldToScreenPoint(m_player.transform.position + m_player.transform.forward * 50);
//        Vector3 secondaryPos = Camera.main.WorldToScreenPoint(m_player.transform.position + m_player.transform.forward * 10);
//        //m_cursor.anchoredPosition = TransformToCanvasPosition(worldPosition, center);
//        m_primaryCursor.anchoredPosition = new Vector2(primaryPos.x - center.x, primaryPos.y - center.y);
//        m_secondaryCursor.anchoredPosition = new Vector2(secondaryPos.x - center.x, secondaryPos.y - center.y);

        //Quaternion targetRotation = Quaternion.Euler(Input.GetAxis("Vertical") * m_player.m_maxVerticalRotation,
        //                                             -Input.GetAxis("Horizontal") * m_player.m_maxHorizontalRotation * 45,
        //                                             0);
        //Vector3 targetPosition = new Vector3(Input.GetAxis("Horizontal"),
        //                                 transform.localPosition.y,
        //                                 transform.localPosition.z);
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, m_followSpeed);
        //transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, m_followSpeed);

    }
}