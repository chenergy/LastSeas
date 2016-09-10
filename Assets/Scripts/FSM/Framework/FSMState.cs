//-----------------------------------------------------------------------
// <copyright file="FSMState.cs" company="Jonathan Chien">
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

namespace FSM
{
    /// <summary>
    /// A container state in the FSM containing actions run by the context. Also
    /// contains the transitions available to other states.
    /// </summary>
    public class FSMState
    {
        /// <summary>
        /// The action when entering the state.
        /// </summary>
        private FSMAction m_entryAction;

        /// <summary>
        /// The action when updating the state.
        /// </summary>
        private FSMAction m_updateAction;

        /// <summary>
        /// The action when exiting the state.
        /// </summary>
        private FSMAction m_exitAction;

        /// <summary>
        /// The list of transitions to other states.
        /// </summary>
        private Dictionary<string, FSMTransition> m_transitionList;

        /// <summary>
        /// Initializes a new instance of the <see cref="FSM.FSMState"/> class.
        /// </summary>
        /// <param name="entryAction">Entry action.</param>
        /// <param name="updateAction">Update action.</param>
        /// <param name="exitAction">Exit action.</param>
        public FSMState(FSMAction entryAction, FSMAction updateAction, FSMAction exitAction)
        {
            this.m_entryAction = entryAction;
            this.m_updateAction = updateAction;
            this.m_exitAction = exitAction;
            this.m_transitionList = new Dictionary<string,FSMTransition>();
        }
		
        /// <summary>
        /// Adds an available transition as an event to this instance of a state.
        /// </summary>
        /// <param name="transition">Transition.</param>
        /// <param name="eventName">Name of the transition event.</param>
        public void AddTransition(FSMTransition transition, string eventName)
        {
            if (!m_transitionList.ContainsKey(eventName))
            {
                m_transitionList.Add(eventName, transition);
            }
        }

        /// <summary>
        /// Removes the transition from the list of transitions.
        /// </summary>
        /// <param name="eventName">Name of the transition event.</param>
        public void RemoveTransition(string eventName)
        {
            if (m_transitionList.ContainsKey(eventName))
            {
                m_transitionList.Remove(eventName);
            }
        }
		
        /// <summary>
        /// Call the update action and provide the list of parameters.
        /// </summary>
        /// <param name="fsmc">FSM context.</param>
        /// <param name="list">List of parameters.</param>
        public void Update(FSMContext fsmc, params object[] list)
        {
            m_updateAction(fsmc, list);
		}
		
        /// <summary>
        /// Dispatch the specified event.
        /// </summary>
        /// <param name="fsmc">FSM context.</param>
        /// <param name="eventName">Name of the transition event.</param>
        public void Dispatch(FSMContext fsmc, string eventName, params object[] list)
        {
            if (m_transitionList.ContainsKey(eventName))
            {
                fsmc.CurrentState.m_exitAction(fsmc, list);
                m_transitionList[eventName].Execute(fsmc, list);
                fsmc.CurrentState.m_entryAction(fsmc, list);
            }
        }
    }
}
