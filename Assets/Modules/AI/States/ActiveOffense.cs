using System.Collections;
using UnityEngine;

namespace AI
{
    public class ActiveOffense : State
    {
        public ActiveOffense(StateMachine stateMachine) : base(stateMachine) { }

        public override IEnumerator Start()
        {
            StateMachine.SetAnimationValue("HasBall", true);
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

        public override IEnumerator Kick()
        {
            StateMachine.SetAnimationTrigger("Kick");

            yield return new WaitForSeconds(0.2f);

            StateMachine.SetState(new PassiveOffense(StateMachine));
        }

        public override IEnumerator Pass()
        {
            StateMachine.SetAnimationTrigger("Pass");

            yield return new WaitForSeconds(0.2f);

            StateMachine.SetState(new PassiveOffense(StateMachine));
        }
    }
}