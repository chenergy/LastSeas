//-----------------------------------------------------------------------
// <copyright file="UIBattleOrder.cs" company="Jonathan Chien">
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
using System.Collections.Generic;

/// <summary>
/// User interface representation the overall battle order.
/// </summary>
public class UIBattleCombatOrder : MonoBehaviour
{
    /// <summary>
    /// The prefab to generate character UI elements.
    /// </summary>
    public GameObject m_characterUIPrefab;

    /// <summary>
    /// The overlay highlight to show a character is currently selected.
    /// </summary>
    public RectTransform m_selectedCharacterHighlight;

    /// <summary>
    /// Reference to the RectTransform of the current canvas.
    /// </summary>
    private RectTransform m_rectTransform;

    /// <summary>
    /// The list of character UI elements created.
    /// </summary>
    private List<UIBattleCharacterOrder> m_battleCharacterOrder;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_battleCharacterOrder = new List<UIBattleCharacterOrder>();
    }

    /// <summary>
    /// Adds the battle character UI to the list of characters.
    /// </summary>
    /// <param name="character">Battle character information to add.</param>
    public void AddBattleCharacterUI(BattleCharacter character)
    {
        UIBattleCharacterOrder characterUI = Instantiate(m_characterUIPrefab).GetComponent<UIBattleCharacterOrder>();
        characterUI.m_icon.sprite = character.m_icon;
        characterUI.m_name.text = character.m_name;

        RectTransform rectTransform = characterUI.GetComponent<RectTransform>();
        characterUI.transform.SetParent(m_rectTransform);
        rectTransform.anchoredPosition = new Vector2(0, -100 * m_battleCharacterOrder.Count);

        m_battleCharacterOrder.Add(characterUI);
    }

    /// <summary>
    /// Reset the UI.
    /// </summary>
    public void Reset()
    {
        foreach (UIBattleCharacterOrder ui in m_battleCharacterOrder)
        {
            Destroy(ui.gameObject);
        }

        m_battleCharacterOrder.Clear();
    }

    /// <summary>
    /// Shows that the given character is selected
    /// </summary>
    /// <param name="character">Selected character.</param>
    public void SelectedCharacter(int character)
    {
        m_selectedCharacterHighlight.position = m_battleCharacterOrder[character].transform.position;
    }
}

