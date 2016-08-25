//-----------------------------------------------------------------------
// <copyright file="TurnManager.cs" company="Jonathan Chien">
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

public class TurnManager : MonoBehaviour
{
    private Queue<Character> m_charactersTurnOrder = new Queue<Character>();

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
    }

    private void _AddCharacterToQueue(Character character)
    {
        
    }
}

