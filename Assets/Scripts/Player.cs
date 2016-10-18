//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="Jonathan Chien">
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
/// Airship controlled by the player in the scene.
/// </summary>
public class Player : Airship
{
    /// <summary>
    /// Reference to the base model object contained in the GameObject.
    /// </summary>
    public Transform m_model;

    public Transform m_bow;
    public Transform m_port;
    public Transform m_starboard;

    /// <summary>
    /// The layer that register the ground position to move to.
    /// </summary>
    public LayerMask m_groundHitLayer;

    /// <summary>
    /// Mode showing how the flight should behave.
    /// </summary>
    public enum FlightMode { ALL_RANGE, ON_RAILS };

    /// <summary>
    /// The mode the airship is currently in.
    /// </summary>
    public FlightMode m_mode = FlightMode.ALL_RANGE;

    /// <summary>
    /// Check whether player can be accessed.
    /// </summary>
    public bool m_movementEnabled = false;

    /// <summary>
    /// Maximum horizontal rotation in degrees.
    /// </summary>
    public float m_maxHorizontalRotation = 30f;

    /// <summary>
    /// Maximum vertical rotation in degrees.
    /// </summary>
    public float m_maxVerticalRotation = 15f;

    private Transform m_railsParent;

    /// <summary>
    /// This function is called every fixed framerate frame.
    /// </summary>
    public void FixedUpdate()
    {
        if (m_mode == FlightMode.ALL_RANGE)
        {
            float displacement = m_curSpeed * Time.deltaTime + (0.5f * m_acceleration * Time.deltaTime * Time.deltaTime);
            transform.position += transform.forward * displacement;
            m_curSpeed = Mathf.Min(m_topSpeed, m_curSpeed + m_acceleration * Time.deltaTime);

            Vector3 curRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(-Input.GetAxis("Vertical") * m_maxVerticalRotation,
                                                  curRotation.y + Input.GetAxis("Horizontal") * m_maxHorizontalRotation, 0);
            m_model.transform.localRotation = Quaternion.Euler(Input.GetAxis("Horizontal") * m_maxHorizontalRotation * 20,
                90f, 0f);
        }
        else if (m_mode == FlightMode.ON_RAILS)
        {
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -10, 10),
                                                  Mathf.Clamp(transform.localPosition.y, -10, 10),
                                                  transform.localPosition.z);
            m_model.transform.localRotation = Quaternion.Euler(Input.GetAxis("Horizontal") * m_maxHorizontalRotation * 20,
                90f, 0f);
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            GameObject gobj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gobj.transform.position = m_bow.transform.position;
            gobj.GetComponent<Collider>().isTrigger = true;
            Rigidbody rb = gobj.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = m_bow.transform.forward * m_curSpeed * 50;
        }
    }
}

