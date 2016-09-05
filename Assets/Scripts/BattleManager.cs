//-----------------------------------------------------------------------
// <copyright file="BattleManager.cs" company="Jonathan Chien">
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
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the battle sequence and actions.
/// </summary>
public class BattleManager : MonoBehaviour
{
    public Player m_player;
    public UIBattleOrder m_orderUICanvas;

    private List<BattleCharacter> m_battleCharacters = new List<BattleCharacter>();

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Add characters on the airship.
            foreach (CharacterName name in m_player.m_charactersOnboard)
            {
                BattleCharacter bc = new BattleCharacter(name, true);
                _AddCharacterToQueue(bc);
            }

            // Add enemies surrounding the airship when it is detected.
            Collider[] results = new Collider[10];
            if (Physics.OverlapSphereNonAlloc(m_player.transform.position, 10.0f, results) > 0)
            {
                foreach(Collider c in results)
                {
                    if (c == null)
                    {
                        break;
                    }

                    Enemy enemy = c.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        foreach (CharacterName name in enemy.m_charactersOnboard)
                        {
                            BattleCharacter bc = new BattleCharacter(name, false);
                            _AddCharacterToQueue(bc);
                        }
                    }
                }
            }
                
            foreach (BattleCharacter bc in m_battleCharacters)
            {
                m_orderUICanvas.AddBattleCharacterUI(bc);
            }
        }
    }

    /// <summary>
    /// Adds the given character to the list of character in order of speed.
    /// </summary>
    /// <param name="battleCharacter">Character to add.</param>
    private void _AddCharacterToQueue(BattleCharacter battleCharacter)
    {
        for (int i = 0; i < m_battleCharacters.Count; i++)
        {
            if (battleCharacter.m_speed > m_battleCharacters[i].m_speed)
            {
                m_battleCharacters.Insert(i, battleCharacter);
                return;
            }
        }

        m_battleCharacters.Add(battleCharacter);
    }
}

