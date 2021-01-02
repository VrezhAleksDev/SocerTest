using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public abstract class State
    {
        protected StateMachine StateMachine;

        protected State(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual IEnumerator Start() { yield break; }
        public virtual IEnumerator Idle() { yield break; }
        public virtual IEnumerator Move() { yield break; }
        public virtual IEnumerator BallReceive() { yield break; }
        public virtual IEnumerator Pass() { yield break; }
        public virtual IEnumerator Kick() { yield break;  }
    }
}
