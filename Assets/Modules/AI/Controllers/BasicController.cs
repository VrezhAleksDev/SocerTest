using AI.Trajectory;
using AI.VelocityMultipliers;
using UnityEngine;

namespace AI 
{
    public class BasicController : StateMachine
    {
        public Animator Animator;
        public float ForwardSpeed;
        public float VelocityAnimationLerp;
        public float VelocityAnimationMultiplier;

        private Vector3 _animationVelocity;

        private readonly IMultiplier VelocityMultiplier = new RoleAndPositionMultiplier();
        public override Vector3 AnimationVelocity
        {
            get
            {
                _animationVelocity.x = Mathf.Lerp(_animationVelocity.x, Velocity.normalized.x * Velocity.magnitude * VelocityAnimationMultiplier, Time.deltaTime * VelocityAnimationLerp);
                _animationVelocity.z = Mathf.Lerp(_animationVelocity.z, Velocity.normalized.z * Velocity.magnitude * VelocityAnimationMultiplier, Time.deltaTime * VelocityAnimationLerp);

                return _animationVelocity;
            }

        }

        private void Start()
        {
            IsGrounded = true;
            SetState(new Begin(this));
        }

        public override void MoveWithTeamDirection()
        {
            if (!Scenario.MainScenarioController.Socers.ContainsKey(Info.Team))
            {
                return;
            }

            if (Scenario.MainScenarioController.Socers[Info.Team].Count - 1 == 0)
            {
                return;
            }

            Vector3 middleValue = Vector3.zero;
            int counter = 0;

            for (int i = 0; i < Scenario.MainScenarioController.Socers[Info.Team].Count; i++)
            {
                if (Scenario.MainScenarioController.Socers[Info.Team][i].CurrentTrajectory != null)
                {
                    counter++;
                    middleValue += Scenario.MainScenarioController.Socers[Info.Team][i].Velocity;
                }
            }

            Velocity.x = middleValue.x / counter;
            Velocity.z = middleValue.z / counter;

            Velocity.Scale(VelocityMultiplier.CalculateMultiplier(Info, transform));

            Move();
        }

        public override void MoveByTraectory(LineTrajectory trajectory)
        {
            if (trajectory == null || trajectory.CurrentPoint == null)
            {
                Velocity.x = 0;
                Velocity.z = 0;
                return;
            }

            Vector3 dirtyDirection = trajectory.CurrentPoint.transform.position - transform.position;

            Velocity.x = dirtyDirection.normalized.x;
            Velocity.z = dirtyDirection.normalized.z;

            Move();

            if (dirtyDirection.magnitude < 0.1f)
            {
                trajectory.CurrentPointReached();
            }
        }

        private void Move()
        {
            transform.forward = Velocity.normalized;

            transform.Translate(Vector3.forward * Velocity.magnitude * ForwardSpeed * Time.deltaTime);
        }

        public override void SetAnimationValue(string key, float value)
        {
            Animator.SetFloat(key, value);
        }

        public override void SetAnimationValue(string key, bool value)
        {
            Animator.SetBool(key, value);
        }

        public override void SetAnimationValue(string key, int value)
        {
            Animator.SetInteger(key, value);
        }

        public override void SetAnimationTrigger(string key)
        {
            Animator.SetTrigger(key);
        }
    }
}
