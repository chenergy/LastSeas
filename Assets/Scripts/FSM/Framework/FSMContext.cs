//-----------------------------------------------------------------------
// <copyright file="FSMContext.cs" company="Jonathan Chien">
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
using System;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// Manages the current state and transitions between states.
    /// </summary>
    public class FSMContext
    {
        /// <summary>
        /// An additional initial action taken when the class is created.
        /// </summary>
        private FSMAction m_initAction;

        /// <summary>
        /// The current state being run in the context.
        /// </summary>
        private FSMState m_currentState;

        /// <summary>
        /// Gets or sets the current state.
        /// </summary>
        /// <value>The current state.</value>
        public FSMState CurrentState
        {
            get { return m_currentState; }
            set { m_currentState = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSM.FSMContext"/> class.
        /// </summary>
        /// <param name="startState">Start state.</param>
        /// <param name="initAction">Init action.</param>
        public FSMContext(FSMState startState, FSMAction initAction)
        {
            this.m_currentState = startState;
            this.m_initAction = initAction;
			this.m_initAction(this);
        }
		
        /// <summary>
        /// Transition to a new state with the given event name.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        public void Dispatch(string eventName, params object[] list)
        {
            m_currentState.Dispatch(this, eventName, list);
        }

        /// <summary>
        /// Update the current state with the given list of params.
        /// </summary>
        /// <param name="list">List.</param>
        public void Update(params object[] list)
        {
            m_currentState.Update(this, list);
		}
    }
}
