//-----------------------------------------------------------------------
// <copyright file="UIBattlePlayerMovement.cs" company="Jonathan Chien">
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
using UnityEngine.EventSystems;
using System.Collections;

public class UIBattlePlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Reference to the player in the scene.
    /// </summary>
    public Player m_player;

    public BattleManager m_battleManager;

    /// <summary>
    /// The layer that register the ground position to move to.
    /// </summary>
    public LayerMask m_groundHitLayer;

//    // Use this for initialization
//    void Start ()
//    {
//	
//    }
//	
//    // Update is called once per frame
//    void Update ()
//    {
//	
//    }


    public void TouchMove(BaseEventData bed)
    {
        PointerEventData ped = (PointerEventData)bed;
        Ray ray = Camera.main.ScreenPointToRay(ped.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_groundHitLayer))
        {
            if (m_battleManager.IsInMovementRange(hit.point))
            {
                m_player.MoveToPosition(hit.point);
            }
        }
    }
}

