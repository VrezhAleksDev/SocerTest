using AI.Trajectory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI 
{
    //public class VelocityComparator : IComparer<BasicController>
    //{
    //    public int Compare(BasicController x, BasicController y)
    //    {
    //        if (x.Velocity.magnitude > y.Velocity.magnitude)
    //        {
    //            return 1;
    //        }

    //        if (x.Velocity.magnitude < y.Velocity.magnitude)
    //        {
    //            return -1;
    //        }

    //        return 0;
    //    }
    //}
    public class BasicController : StateMachine
    {
        //private readonly VelocityComparator VelocityComparator = new VelocityComparator();

        public Animator Animator;
        public float ForwardSpeed;
        public Vector3 VelocityAnimationMultiplier;
        public Vector3 VelocityAnimationMax;

        private Vector3 _animationVelocity;
        public override Vector3 AnimationVelocity
        {
            get
            {
                _animationVelocity.x = Mathf.Sign(Velocity.x) * Mathf.Min(Mathf.Abs(Velocity.x) * VelocityAnimationMultiplier.x, VelocityAnimationMax.x);
                _animationVelocity.z = Mathf.Sign(Velocity.z) * Mathf.Min(Mathf.Abs(Velocity.z) * VelocityAnimationMultiplier.z, VelocityAnimationMax.z);

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
            if (!Scenario.MainScenarioController.Socers.ContainsKey(Team))
            {
                return;
            }

            if (Scenario.MainScenarioController.Socers[Team].Count - 1 == 0)
            {
                return;
            }

            Vector3 middleValue = Vector3.zero;

            for (int i = 0; i < Scenario.MainScenarioController.Socers[Team].Count; i++)
            {
                if (Scenario.MainScenarioController.Socers[Team][i] != this)
                {
                    middleValue += Scenario.MainScenarioController.Socers[Team][i].Velocity;
                }
            }

            Velocity.x = middleValue.x / Scenario.MainScenarioController.Socers[Team].Count;
            Velocity.z = middleValue.z / Scenario.MainScenarioController.Socers[Team].Count;

            //int medianIndex = Common.Extensions.Math.GetMedianIndex(Scenario.MainScenarioController.Socers[Team].ToArray(), VelocityComparator, out bool isEvan);

            //if (isEvan)
            //{
            //    Velocity = Scenario.MainScenarioController.Socers[Team][medianIndex].Velocity;
            //}
            //else
            //{
            //    Velocity.x = (Scenario.MainScenarioController.Socers[Team][medianIndex].Velocity + Scenario.MainScenarioController.Socers[Team][medianIndex - 1].Velocity).x * 0.5f;
            //    Velocity.z = (Scenario.MainScenarioController.Socers[Team][medianIndex].Velocity + Scenario.MainScenarioController.Socers[Team][medianIndex - 1].Velocity).z * 0.5f;
            //}

            transform.Translate(Velocity.normalized * ForwardSpeed * Time.deltaTime);
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

            transform.Translate(dirtyDirection.normalized * ForwardSpeed * Time.deltaTime);

            if (dirtyDirection.magnitude < 0.1f)
            {
                trajectory.CurrentPointReached();
            }
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
