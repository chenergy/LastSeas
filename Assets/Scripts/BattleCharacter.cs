//-----------------------------------------------------------------------
// <copyright file="BattleCharacter.cs" company="Jonathan Chien">
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
/// During battle, character info taken from database and saved player data.
/// </summary>
public class BattleCharacter
{
    public bool m_playerControlled;
    public Airship m_airship;
    public string m_name;
    public Sprite m_icon;
    public int m_experience;
    public int m_curHealth;
    public int m_curEnergy;
    public int m_baseSpeed;
    public int m_baseMovement;

    public BattleCharacter(CharacterName name, Airship airship, bool playerControlled)
    {
        m_playerControlled = playerControlled;
        m_airship = airship;

        CharacterData databaseChar = Database.Instance.GetCharacterData(name);
        m_name = databaseChar.m_name;
        m_icon = databaseChar.m_icon;
        m_baseSpeed = databaseChar.m_baseSpeed;
        m_baseMovement = databaseChar.m_baseMovement;

        CharacterSavedData savedChar = SavedData.LastSavedData.GetCharacterSavedData(name);
        m_experience = savedChar.m_experience;
        m_curHealth = savedChar.m_curHealth;
        m_curEnergy = savedChar.m_curEnergy;
    }
}