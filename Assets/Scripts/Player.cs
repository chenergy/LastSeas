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
/// Player character in the scene.
/// </summary>
public class Player : Airship
{
    public Transform m_model;

    /// <summary>
    /// The layer that register the ground position to move to.
    /// </summary>
    public LayerMask m_groundHitLayer;

    public enum FlightMode { ALL_RANGE, AUTO };
    public FlightMode m_mode = FlightMode.ALL_RANGE;

    /// <summary>
    /// Check whether player can be accessed.
    /// </summary>
    public bool m_movementEnabled = false;

    public float m_maxHorizontalRotation = 30f;
    public float m_maxVerticalRotation = 15f;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        if (m_mode == FlightMode.ALL_RANGE)
        {
            Vector3 curRotation = transform.rotation.eulerAngles;

            transform.rotation = Quaternion.Euler(-Input.GetAxis("Vertical") * m_maxVerticalRotation, 
                curRotation.y + Input.GetAxis("Horizontal") * m_maxHorizontalRotation, 0);
            
            m_model.transform.localRotation = Quaternion.Euler(Input.GetAxis("Horizontal") * m_maxHorizontalRotation * 10,
                90f, 0f);
        }
    }
}

