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
using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract airship class containing base behaviours.
/// </summary>
public abstract class Airship : MonoBehaviour
{
    /// <summary>
    /// The list of characters onboard the ship.
    /// </summary>
    public CharacterName[] m_charactersOnboard;

    /// <summary>
    /// The movement speed of the airship.
    /// </summary>
    public float m_topSpeed = 1;

    /// <summary>
    /// The acceleration to top speed.
    /// </summary>
    [Range(0f, 1f)]
    public float m_acceleration = 0.1f;

    /// <summary>
    /// The animator attached to the ship objects.
    /// </summary>
    public Animator m_animator;

    /// <summary>
    /// Internal current speed for movement.
    /// </summary>
    private float m_curSpeed;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
    }

    /// <summary>
    /// Moves to the point.
    /// </summary>
    /// <returns>The coroutine.</returns>
    /// <param name="point">The point to move to.</param>
    protected IEnumerator _MoveToPointRoutine(Vector3 point)
    {
        transform.forward = point - transform.position;

        float t = 0;
        float distance = (point - transform.position).magnitude;
        Vector3 start = transform.position;

        while (t < distance)
        {
            m_curSpeed = Mathf.Lerp(m_curSpeed, m_topSpeed, m_acceleration);
            transform.position = Vector3.Lerp(start, point, t / distance);
            m_animator.SetFloat("Speed", m_curSpeed);

            t += Time.deltaTime * m_curSpeed;
            yield return new WaitForEndOfFrame();
        }

        transform.position = point;
        m_curSpeed = 0;
        m_animator.SetFloat("Speed", 0);
    }
}

