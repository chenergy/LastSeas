using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float m_followSpeed = 0.5f;
    public Player m_player;
    public RectTransform m_cursor;
    public RectTransform m_canvasTransform;

    // Use this for initialization
    void Start ()
    {
    }
	
    // Update is called once per frame
    void FixedUpdate ()
    {
        float width = m_canvasTransform.sizeDelta.x;
        float height = m_canvasTransform.sizeDelta.y;
        Vector3 center = new Vector3(width / 2, height / 2, 0);
        m_cursor.anchoredPosition = new Vector2(Input.GetAxis("Horizontal") * width / 2,
            Input.GetAxis("Vertical") * height / 2);

        Quaternion targetRotation = Quaternion.Euler(Input.GetAxis("Vertical") * m_player.m_maxVerticalRotation, 
            -Input.GetAxis("Horizontal") * m_player.m_maxHorizontalRotation, 0);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, m_followSpeed);
    }
}