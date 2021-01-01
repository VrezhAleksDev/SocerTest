using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class StateMachine : MonoBehaviour
    {
        protected State State;

        public virtual bool IsGrounded { get; set; }

        [HideInInspector]
        public Vector3 Velocity;


        public void SetState(State state)
        {
            State = state;  
            StartCoroutine(State.Start());
        }

        public virtual void SetAnimationValue(string key, float value) { }

        public virtual void SetAnimationValue(string key, bool value) { }

        public virtual void SetAnimationValue(string key, int value) { }

        public virtual void SetAnimationTrigger(string key) { }
    }
}
