using System.Collections;
using UnityEngine;

namespace AI
{
    public class Begin : State
    {
        public Begin(StateMachine stateMachine) : base(stateMachine) { }

        public override IEnumerator Start()
        {
            while (!StateMachine.IsGrounded)
            {
                yield return new WaitForEndOfFrame();
            }

            StateMachine.SetState(new PassiveOffense(StateMachine));
        }
    }
}