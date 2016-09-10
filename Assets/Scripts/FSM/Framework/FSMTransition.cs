//-----------------------------------------------------------------------
// <copyright file="FSMTransition.cs" company="Jonathan Chien">
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

namespace FSM
{
    /// <summary>
    /// The container for a transition action to a target state.
    /// </summary>
    public class FSMTransition
    {
        /// <summary>
        /// The state to transition to.
        /// </summary>
        private FSMState m_target;

        /// <summary>
        /// The action to perform on transition.
        /// </summary>
        private FSMAction m_action;

        /// <summary>
        /// Initializes a new instance of the <see cref="FSM.FSMTransition"/> class.
        /// </summary>
        /// <param name="targetState">Target state to transition to.</param>
        /// <param name="action">Action to perform during transition.</param>
        public FSMTransition(FSMState targetState, FSMAction action)
        {
            this.m_target = targetState;
            this.m_action = action;
        }

        /// <summary>
        /// Execute the transition to the target state.
        /// </summary>
        /// <param name="c">The FSM context.</param>
        public void Execute(FSMContext c, params object[] list)
        {
            m_action(c, list);
            c.CurrentState = m_target;
        }
    }
}
