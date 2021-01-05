using AI.Models;
using AI.Trajectory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class StateMachine : MonoBehaviour
    {
        protected State State;

        public SocerInfo Info { get; set; }
        public virtual bool IsGrounded { get; set; }
        public LineTrajectory CurrentTrajectory { get; set; }

        [HideInInspector]
        public Vector3 Velocity;

        public virtual Vector3 AnimationVelocity => Velocity;

        public void SetState(State state)
        {
            StopAllCoroutines();
            State = state;  
            StartCoroutine(State.Start());
        }

        public virtual void MoveWithTeamDirection() { }

        public virtual void MoveByTraectory(LineTrajectory trajectory) { }

        public virtual void SetAnimationValue(string key, float value) { }

        public virtual void SetAnimationValue(string key, bool value) { }

        public virtual void SetAnimationValue(string key, int value) { }

        public virtual void SetAnimationTrigger(string key) { }
    }
}
