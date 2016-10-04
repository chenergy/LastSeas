//-----------------------------------------------------------------------
// <copyright file="UISelectionMenu.cs" company="Jonathan Chien">
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
using UnityEngine.UI;
using System.Collections;

public class UIBattleSelectionMenu : MonoBehaviour
{
    public GameObject m_mainMenu;
    public GameObject m_moveFinalizeMenu;
    public GameObject m_attackMenu;
    public GameObject m_attackFinalizeMenu;
    public Text m_characterName;

    private BattleManager m_battleManager;
    private BattleCharacter m_curBattleCharacter;

    // Use this for initialization
    void Start ()
    {
        m_battleManager = FindObjectOfType<BattleManager>();
        _Reset();
    }

    public void BeginTurn(BattleCharacter character)
    {
        m_curBattleCharacter = character;
        m_characterName.text = character.m_name;
        m_mainMenu.gameObject.SetActive(true);
    }

    public void ButtonMove()
    {
        m_mainMenu.gameObject.SetActive(false);
        m_moveFinalizeMenu.gameObject.SetActive(true);
    }

    public void ButtonAttack()
    {
        m_mainMenu.gameObject.SetActive(false);
        m_attackMenu.gameObject.SetActive(true);
    }

    public void ButtonMoveFinalize()
    {
        m_moveFinalizeMenu.gameObject.SetActive(false);
        m_mainMenu.gameObject.SetActive(true);
    }

    public void ButtonMoveFinalizeCancel()
    {
        m_moveFinalizeMenu.gameObject.SetActive(false);
        m_mainMenu.gameObject.SetActive(true);
    }

    public void ButtonAttackSelection(string attack)
    {
        AttackName name = (AttackName)System.Enum.Parse(typeof(AttackName), attack);
        AttackData data = Database.Instance.GetAttackData(name);
        GameObject.Instantiate(data.m_prefab, m_battleManager.m_player.transform.position, Quaternion.identity);

        m_attackMenu.gameObject.SetActive(false);
        m_attackFinalizeMenu.gameObject.SetActive(true);
    }

    public void ButtonAttackSelectionCancel()
    {
        m_attackMenu.gameObject.SetActive(false);
        m_mainMenu.gameObject.SetActive(true);
    }

    public void ButtonAttackFinalize()
    {
        m_attackFinalizeMenu.gameObject.SetActive(false);
        m_mainMenu.gameObject.SetActive(true);
    }

    public void ButtonAttackFinalizeCancel()
    {
        m_attackFinalizeMenu.gameObject.SetActive(false);
        m_attackMenu.gameObject.SetActive(true);
    }

    public void ButtonEndTurn()
    {
        m_battleManager.EndTurn();
        _Reset();
    }

    private void _Reset()
    {
        m_mainMenu.gameObject.SetActive(false);
        m_moveFinalizeMenu.gameObject.SetActive(false);
        m_attackMenu.gameObject.SetActive(false);
        m_attackFinalizeMenu.gameObject.SetActive(false);
    }
}

