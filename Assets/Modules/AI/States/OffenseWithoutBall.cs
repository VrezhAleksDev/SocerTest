using System.Collections;
using UnityEngine;

namespace AI
{
    public class OffenseWithoutBall : State
    {
        public OffenseWithoutBall(StateMachine stateMachine) : base(stateMachine) { }

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
                StateMachine.SetAnimationValue("VelocityZ", StateMachine.AnimationVelocity.z);
                StateMachine.SetAnimationValue("VelocityX", StateMachine.AnimationVelocity.x);

                if (StateMachine.CurrentTrajectory == null)
                {
                    StateMachine.MoveWithTeamDirection();
                }
                else
                {
                    StateMachine.MoveByTraectory(StateMachine.CurrentTrajectory);
                }

                yield return new WaitForEndOfFrame();
            }
        }

        public override IEnumerator BallReceive()
        {
            StateMachine.SetAnimationTrigger("BallReceive");

            StateMachine.SetState(new OffenseWithBall(StateMachine));
            yield break;
        }
    }
}