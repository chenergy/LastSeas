//-----------------------------------------------------------------------
// <copyright file="Enemy.cs" company="Jonathan Chien">
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
/// Enemy airship in the scene.
/// </summary>
public class Enemy : Airship
{
    /// <summary>
    /// This function is called every fixed framerate frame.
    /// </summary>
    public void FixedUpdate()
    {
        float displacement = m_curSpeed * Time.deltaTime + (0.5f * m_acceleration * Time.deltaTime * Time.deltaTime);
        transform.position += transform.forward * displacement;
        m_curSpeed = Mathf.Min(m_topSpeed, m_curSpeed + m_acceleration * Time.deltaTime);
    }
}

