using System.Collections;
using UnityEngine;

namespace AI
{
    public class PassiveOffense : State
    {
        public PassiveOffense(StateMachine stateMachine) : base(stateMachine) { }

        public override IEnumerator Start()
        {
            StateMachine.SetAnimationValue("HasBall", false);
            StateMachine.StartCoroutine(Move());
            yield break;
        }

        public override IEnumerator Move()
        {
            while (true)
            {
                StateMachine.SetAnimationValue("IsMoving", true);
                StateMachine.SetAnimationValue("VelocityZ", StateMachine.Velocity.z);
                StateMachine.SetAnimationValue("VelocityX", StateMachine.Velocity.x);

                yield return new WaitForEndOfFrame();
            }
        }

        public override IEnumerator BallRecieve()
        {
            StateMachine.SetAnimationTrigger("BallRecieve");

            StateMachine.SetState(new ActiveOffense(StateMachine));
            yield break;
        }
    }
}