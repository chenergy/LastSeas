//-----------------------------------------------------------------------
// <copyright file="Character.cs" company="Jonathan Chien">
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

public class Character : MonoBehaviour
{
    public enum ControlType { PLAYER, COMPUTER };

    public int m_hp;

    public int m_mp;

    public ControlType m_controlType;

    public Attack[] m_attacks;

    private TurnManager m_turnManager;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
        m_turnManager = FindObjectOfType<TurnManager>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UseAttack(0);
        }
    }

    public void UseAttack(int attackNum)
    {
        m_attacks[attackNum].UseAttack();
    }
}

