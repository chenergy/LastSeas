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
using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Manages the battle sequence and actions.
/// </summary>
public class BattleManager : MonoBehaviour
{
    /// <summary>
    /// The layer that register the ground position to move to.
    /// </summary>
    public LayerMask m_groundHitLayer;

    /// <summary>
    /// Reference to the player.
    /// </summary>
    public Player m_player;

    public GameObject m_movementRange;

    /// <summary>
    /// The UI displaying the order of battle.
    /// </summary>
    public UIBattleCombatOrder m_combatOrderUI;

    /// <summary>
    /// The UI to selection player driven actions.
    /// </summary>
    public UIBattleSelectionMenu m_selectionMenuUI;

    /// <summary>
    /// The radius around the player to include enemies.
    /// </summary>
    public float m_combatRadius = 10.0f;

    /// <summary>
    /// If the character has already moved yet.
    /// </summary>
    public bool m_hasMoved = false;

    /// <summary>
    /// If the character has already attacked yet.
    /// </summary>
    public bool m_hasAttacked = false;

    /// <summary>
    /// The position that battle started at.
    /// </summary>
    private Vector3 m_startPosition;

    /// <summary>
    /// The list of characters involved in battle.
    /// </summary>
    private List<BattleCharacter> m_battleCharacters = new List<BattleCharacter>();

    /// <summary>
    /// The last attack used. Used to destroy attack object after state change.
    /// </summary>
    private Attack m_lastAttack;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
        m_selectionMenuUI.gameObject.SetActive(false);
        m_movementRange.SetActive(false);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _EnterCombat();
        }
    }

    public void TouchMove(BaseEventData bed)
    {
        PointerEventData ped = (PointerEventData)bed;
        Ray ray = Camera.main.ScreenPointToRay(ped.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_groundHitLayer))
        {
            if (_IsInMovementRange(hit.point))
            {
//                m_player.MoveToPosition(hit.point);
            }
        }
    }

    private bool _IsInMovementRange(Vector3 point)
    {
        return (point - m_startPosition).magnitude < m_combatRadius;
    }

    /// <summary>
    /// Performs actions related to entering combat.
    /// </summary>
    private void _EnterCombat()
    {
//        m_fsm.Dispatch("startup");
        _SetupBattleOrder();
        BeginTurn();
    }

    /// <summary>
    /// Adds the given character to the list of character in order of speed.
    /// </summary>
    /// <param name="battleCharacter">Character to add.</param>
    private void _AddCharacterToQueue(BattleCharacter battleCharacter)
    {
        for (int i = 0; i < m_battleCharacters.Count; i++)
        {
            if (battleCharacter.m_baseSpeed > m_battleCharacters[i].m_baseSpeed)
            {
                m_battleCharacters.Insert(i, battleCharacter);
                return;
            }
        }

        m_battleCharacters.Add(battleCharacter);
    }

    /// <summary>
    /// Initialize the battle order by adding player characters and enemies.
    /// </summary>
    private void _SetupBattleOrder()
    {
        m_combatOrderUI.Reset();
        m_battleCharacters.Clear();

        // Add characters on the airship.
        foreach (CharacterName name in m_player.m_charactersOnboard)
        {
            BattleCharacter bc = new BattleCharacter(name, m_player, true);
            _AddCharacterToQueue(bc);
        }

        // Add enemies surrounding the airship when it is detected.
        m_startPosition = m_player.transform.position;
        Collider[] results = new Collider[10];
        if (Physics.OverlapSphereNonAlloc(m_player.transform.position, m_combatRadius, results) > 0)
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
                        BattleCharacter bc = new BattleCharacter(name, enemy, false);
                        _AddCharacterToQueue(bc);
                    }
                }
            }
        }

        // Update the UI to reflect battle order.
        foreach (BattleCharacter bc in m_battleCharacters)
        {
            m_combatOrderUI.AddBattleCharacterUI(bc);
        }
    }

    public void BeginTurn()
    {
        if (m_battleCharacters[0].m_playerControlled)
        {
            m_selectionMenuUI.gameObject.SetActive(true);
            m_selectionMenuUI.BeginTurn(m_battleCharacters[0]);
        }
        else
        {
            _DetermineAIAction();
        }
    }

    public void EndTurn()
    {
        m_hasMoved = false;
        m_hasAttacked = false;
        BattleCharacter bc = m_battleCharacters[0];
        m_battleCharacters.RemoveAt(0);
        m_battleCharacters.Add(bc);
        BeginTurn();
    }

    private void _DetermineAIAction()
    {
        EndTurn();
    }
}

