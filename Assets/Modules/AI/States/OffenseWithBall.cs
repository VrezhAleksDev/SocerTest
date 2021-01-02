using System.Collections;
using UnityEngine;

namespace AI
{
    public class OffenseWithBall : State
    {
        public OffenseWithBall(StateMachine stateMachine) : base(stateMachine) { }

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
                StateMachine.SetAnimationValue("IsMoving", StateMachine.CurrentTrajectory != null && StateMachine.CurrentTrajectory.CurrentPoint);
                StateMachine.SetAnimationValue("VelocityZ", StateMachine.AnimationVelocity.z);
                StateMachine.SetAnimationValue("VelocityX", StateMachine.AnimationVelocity.x);

                StateMachine.MoveByTraectory(StateMachine.CurrentTrajectory);

                yield return new WaitForEndOfFrame();
            }
        }

        public override IEnumerator Kick()
        {
            StateMachine.SetAnimationTrigger("Kick");

            yield return new WaitForSeconds(0.2f);

            StateMachine.SetState(new OffenseWithoutBall(StateMachine));
        }

        public override IEnumerator Pass()
        {
            StateMachine.SetAnimationTrigger("Pass");

            yield return new WaitForSeconds(0.2f);

            StateMachine.SetState(new OffenseWithoutBall(StateMachine));
        }
    }
}