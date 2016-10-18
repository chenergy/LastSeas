//-----------------------------------------------------------------------
// <copyright file="CameraController.cs" company="Jonathan Chien">
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
    [Range(0f, 1f)]
    public float m_followSpeed = 0.5f;

    /// <summary>
    /// Reference to the player character.
    /// </summary>
    public Player m_player;

    /// <summary>
    /// Reference to the cursor.
    /// </summary>
    public RectTransform m_primaryCursor;

    public RectTransform m_secondaryCursor;

    /// <summary>
    /// Reference to the canvas containing the cursor for screen size.
    /// </summary>
    public RectTransform m_canvasTransform;

    public Spline3D m_spline;

    private float m_timer;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start ()
    {
    }

    /// <summary>
    /// This function is called every fixed framerate frame.
    /// </summary>
    public void FixedUpdate()
    {
        //float width = m_canvasTransform.sizeDelta.x;
        //float height = m_canvasTransform.sizeDelta.y;
        float width = Screen.width;
        float height = Screen.height;
        Vector3 center = new Vector3(width / 2, height / 2, 0);
        //m_cursor.anchoredPosition = new Vector2(Input.GetAxis("Horizontal") * width / 2,
        //                                        Input.GetAxis("Vertical") * height / 2);
        Vector3 primaryPos = Camera.main.WorldToScreenPoint(m_player.transform.position + m_player.transform.forward * 50);
        Vector3 secondaryPos = Camera.main.WorldToScreenPoint(m_player.transform.position + m_player.transform.forward * 10);
        //m_cursor.anchoredPosition = TransformToCanvasPosition(worldPosition, center);
        m_primaryCursor.anchoredPosition = new Vector2(primaryPos.x - center.x, primaryPos.y - center.y);
        m_secondaryCursor.anchoredPosition = new Vector2(secondaryPos.x - center.x, secondaryPos.y - center.y);

        Quaternion targetRotation = Quaternion.Euler(Input.GetAxis("Vertical") * m_player.m_maxVerticalRotation,
                                                     -Input.GetAxis("Horizontal") * m_player.m_maxHorizontalRotation * 45,
                                                     0);
        Vector3 targetPosition = new Vector3(Input.GetAxis("Horizontal") * 5,
                                         transform.localPosition.y,
                                         transform.localPosition.z);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, m_followSpeed);
        //transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, m_followSpeed);

        //transform.position = m_spline.GetPoint(m_timer);
        //m_timer += Time.fixedDeltaTime * 0.1f;
        //m_timer = Mathf.Clamp(m_timer, 0.0f, 1.0f);
    }

    //private Vector2 TransformToCanvasPosition(Vector3 worldPosition, Vector3 canvasCenter)
    //{
    //    Camera.main.WorldToScreenPoint(worldPosition);
    //    return Vector2.zero;
    //}
}