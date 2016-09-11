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

/// <summary>
/// Manages the battle sequence and actions.
/// </summary>
public class BattleManager : MonoBehaviour
{
    /// <summary>
    /// Reference to the player.
    /// </summary>
    public Player m_player;

    /// <summary>
    /// The layer that register the ground position to move to.
    /// </summary>
//    public LayerMask m_groundHitLayer;

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
    /// The UI to end movement.
    /// </summary>
    public UIBattleMoveMenu m_moveMenuUI;

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
    /// The finite state machine that controls the battle sequence.
    /// </summary>
    private FSMContext m_fsm;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
        m_selectionMenuUI.gameObject.SetActive(false);
        m_moveMenuUI.gameObject.SetActive(false);
        m_movementRange.SetActive(false);
        _InitFSM();
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

    public bool IsInMovementRange(Vector3 point)
    {
        return (point - m_startPosition).magnitude < m_combatRadius;
    }

    public void ButtonMove()
    {
        m_fsm.Dispatch("move");
    }

    public void ButtonAttack()
    {
        m_fsm.Dispatch("attack");
    }

    public void ButtonFinalizeMove()
    {
        if ((m_player.transform.position - m_startPosition).magnitude > m_combatRadius)
        {
            m_fsm.Dispatch("escape");
        }
        else
        {
            m_fsm.Dispatch("selection");
        }
    }

    public void ButtonFinalizeAttack(string attack)
    {
        
    }

    public void ButtonEndTurn()
    {
        m_fsm.Dispatch("endTurn");
    }

    /// <summary>
    /// Setup the states used in the finite state machine.
    /// </summary>
    private void _InitFSM()
    {
        FSMState inactiveState = new FSMState(_NoAction, _NoAction, _NoAction);
        FSMState startupState = new FSMState(_StartupEnterAction, _StartupUpdateAction, _StartupExitAction);
        FSMState selectionState = new FSMState(_SelectionEnterAction, _SelectionUpdateAction, _SelectionExitAction);
        FSMState moveState = new FSMState(_MoveEnterAction, _MoveUpdateAction, _MoveExitAction);
        FSMState attackState = new FSMState(_AttackEnterAction, _AttackUpdateAction, _AttackExitAction);
        FSMState endTurnState = new FSMState(_EndTurnEnterAction, _EndTurnUpdateAction, _EndTurnExitAction);
        FSMState victoryState = new FSMState(_VictoryEnterAction, _VictoryUpdateAction, _VictoryExitAction);
        FSMState loseState = new FSMState(_LoseEnterAction, _LoseUpdateAction, _LoseExitAction);
        FSMState escapeState = new FSMState(_EscapeEnterAction, _EscapeUpdateAction, _EscapeExitAction);

        FSMTransition inactiveToStartupTransition = new FSMTransition(startupState, _NoAction);
        FSMTransition startupToSelectionTransition = new FSMTransition(selectionState, _NoAction);
        FSMTransition selectionToMoveTransition = new FSMTransition(moveState, _NoAction);
        FSMTransition selectionToAttackTransition = new FSMTransition(attackState, _NoAction);
        FSMTransition selectionToEndTurnTransition = new FSMTransition(endTurnState, _NoAction);
        FSMTransition moveToSelectionTransition = new FSMTransition(selectionState, _NoAction);
        FSMTransition moveToEscapeTransition = new FSMTransition(escapeState, _NoAction);
        FSMTransition attackToSelectionTransition = new FSMTransition(selectionState, _NoAction);
        FSMTransition attackToVictoryTransition = new FSMTransition(victoryState, _NoAction);
        FSMTransition attackToLoseTransition = new FSMTransition(loseState, _NoAction);
        FSMTransition endTurnToSelectionTransition = new FSMTransition(selectionState, _NoAction);
        FSMTransition escapeToInactiveTransition = new FSMTransition(inactiveState, _NoAction);
        FSMTransition victoryToInactiveTransition = new FSMTransition(inactiveState, _NoAction);
        FSMTransition loseToInactiveTransition = new FSMTransition(inactiveState, _NoAction);

        inactiveState.AddTransition(inactiveToStartupTransition, "startup");
        startupState.AddTransition(startupToSelectionTransition, "selection");
        selectionState.AddTransition(selectionToMoveTransition, "move");
        selectionState.AddTransition(selectionToAttackTransition, "attack");
        selectionState.AddTransition(selectionToEndTurnTransition, "endTurn");
        moveState.AddTransition(moveToSelectionTransition, "selection");
        moveState.AddTransition(moveToEscapeTransition, "escape");
        attackState.AddTransition(attackToSelectionTransition, "selection");
        attackState.AddTransition(attackToVictoryTransition, "victory");
        attackState.AddTransition(attackToLoseTransition, "lose");
        endTurnState.AddTransition(endTurnToSelectionTransition, "selection");
        escapeState.AddTransition(escapeToInactiveTransition, "inactive");
        victoryState.AddTransition(victoryToInactiveTransition, "inactive");
        loseState.AddTransition(loseToInactiveTransition, "inactive");

        m_fsm = FSM.FSM.CreateFSMInstance(inactiveState, _NoAction);
    }

    /// <summary>
    /// Performs actions related to entering combat.
    /// </summary>
    private void _EnterCombat()
    {
        m_fsm.Dispatch("startup");
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


    private void _OpenSelectionMenu()
    {
        m_selectionMenuUI.gameObject.SetActive(true);
    }

    private void _CloseSelectionMenu()
    {
        m_selectionMenuUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// Performs no action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _NoAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// Startup enter action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _StartupEnterAction(FSMContext fsm, params object[] list)
    {
        Debug.Log("startup enter");
        _SetupBattleOrder();
        m_fsm.Dispatch("selection");
    }

    /// <summary>
    /// Startup update action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _StartupUpdateAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// Startup exit action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _StartupExitAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// Selection enter action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _SelectionEnterAction(FSMContext fsm, params object[] list)
    {
        if (m_hasMoved)
        {
            m_selectionMenuUI.m_moveButton.interactable = false;
        }

        if (m_hasAttacked)
        {
            m_selectionMenuUI.m_attackButton.interactable = false;
        }

        BattleCharacter bc = m_battleCharacters[0];
        Debug.Log("starting turn for " + bc.m_name);
        if (bc.m_playerControlled)
        {
            _OpenSelectionMenu();
        }
        else
        {
            // Computer decides what action to take.
            m_fsm.Dispatch("endTurn");
        }
    }

    /// <summary>
    /// Selection update action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _SelectionUpdateAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// Selection exit action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _SelectionExitAction(FSMContext fsm, params object[] list)
    {
        _CloseSelectionMenu();
    }

    /// <summary>
    /// Move enter action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _MoveEnterAction(FSMContext fsm, params object[] list)
    {
        if (!m_hasMoved)
        {
            m_hasMoved = true;
            m_moveMenuUI.gameObject.SetActive(true);
            m_movementRange.SetActive(true);
            m_movementRange.transform.position = m_player.transform.position;
            m_movementRange.transform.localScale = new Vector3(m_combatRadius * 2.0f, 0.01f, m_combatRadius * 2.0f);

            BattleCharacter bc = m_battleCharacters[0];
            if (bc.m_playerControlled)
            {
                m_player.m_movementEnabled = true;
            }
            else
            {
                // Computer decides where to move to.
                m_fsm.Dispatch("endTurn");
            }
        }
    }

    /// <summary>
    /// Move update action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _MoveUpdateAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// Move exit action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _MoveExitAction(FSMContext fsm, params object[] list)
    {
        m_moveMenuUI.gameObject.SetActive(false);
        m_movementRange.gameObject.SetActive(false);

        BattleCharacter bc = m_battleCharacters[0];
        if (bc.m_playerControlled)
        {
            m_player.m_movementEnabled = false;
        }
    }

    /// <summary>
    /// Attack enter action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _AttackEnterAction(FSMContext fsm, params object[] list)
    {
        // perform the attack
        Debug.Log("perform attack");

        int totalPlayerHp = 0;
        int totalEnemyHp = 0;
        foreach (BattleCharacter bc in m_battleCharacters)
        {
            if (bc.m_playerControlled)
            {
                totalPlayerHp += bc.m_curHealth;
            }
            else
            {
                totalEnemyHp += bc.m_curHealth;
            }
        }

        if (totalEnemyHp <= 0)
        {
            m_fsm.Dispatch("victory");
        }
        else if (totalPlayerHp <= 0)
        {
            m_fsm.Dispatch("lose");
        }
        else
        {
            m_fsm.Dispatch("selection");
        }
    }

    /// <summary>
    /// Attack update action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _AttackUpdateAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// Attack exit action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _AttackExitAction(FSMContext fsm, params object[] list)
    {
        m_hasAttacked = true;
    }

    /// <summary>
    /// End turn enter action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _EndTurnEnterAction(FSMContext fsm, params object[] list)
    {
        m_hasMoved = false;
        m_hasAttacked = false;
        BattleCharacter bc = m_battleCharacters[0];
        m_battleCharacters.RemoveAt(0);
        m_battleCharacters.Add(bc);
        m_fsm.Dispatch("selection");
    }

    /// <summary>
    /// End turn update action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _EndTurnUpdateAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// End turn exit action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _EndTurnExitAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// End turn enter action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _VictoryEnterAction(FSMContext fsm, params object[] list)
    {
        Debug.Log("victory");
        m_fsm.Dispatch("inactive");
    }

    /// <summary>
    /// End turn update action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _VictoryUpdateAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// End turn exit action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _VictoryExitAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// End turn enter action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _LoseEnterAction(FSMContext fsm, params object[] list)
    {
        Debug.Log("lose");
    }

    /// <summary>
    /// End turn update action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _LoseUpdateAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// End turn exit action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _LoseExitAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// End turn enter action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _EscapeEnterAction(FSMContext fsm, params object[] list)
    {
        Debug.Log("escaped");
        m_fsm.Dispatch("inactive");
    }

    /// <summary>
    /// End turn update action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _EscapeUpdateAction(FSMContext fsm, params object[] list)
    {
    }

    /// <summary>
    /// End turn exit action.
    /// </summary>
    /// <param name="context">FSM.</param>
    /// <param name="list">List of parameters.</param>
    private void _EscapeExitAction(FSMContext fsm, params object[] list)
    {
    }
}

