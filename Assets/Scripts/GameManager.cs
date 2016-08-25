//-----------------------------------------------------------------------
// <copyright file="GameManager.cs" company="Jonathan Chien">
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
/// Game manager.
/// </summary>
public class GameManager : MonoBehaviour 
{
    /// <summary>
    /// The instance of the GameManager, available statically.
    /// </summary>
    private static GameManager m_instance = null;

    /// <summary>
    /// Gets the GameManager instance.
    /// </summary>
    /// <value>The instance.</value>
    public static GameManager Instance
    {
        get { return m_instance; }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    public void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
}
