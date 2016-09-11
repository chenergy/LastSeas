﻿//-----------------------------------------------------------------------
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
    /// <summary>
    /// The layer that register the ground position to move to.
    /// </summary>
//    public LayerMask m_groundHitLayer;

    /// <summary>
    /// Check whether player can be accessed.
    /// </summary>
    public bool m_movementEnabled = false;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
    }

    public void MoveToPosition(Vector3 position)
    {
        if (m_movementEnabled)
        {
            StopAllCoroutines();
            StartCoroutine(_MoveToPointRoutine(position));
        }
    }
}

