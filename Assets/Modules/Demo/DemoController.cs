using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class DemoController : StateMachine
{
    public Animator Animator;
    public Joystick Joystick;
    public float ForwardSpeed;
    public float ForwardVelocityMax;
    public float StrafeSpeed;
    public float StrafeVelocityMax;

    private void Start()
    {
        IsGrounded = true;
        SetState(new Begin(this));
    }

    private void Update()
    {
        Velocity.x = Mathf.Sign(Joystick.Horizontal) * Mathf.Min(Mathf.Abs(Joystick.Horizontal) * StrafeSpeed, ForwardVelocityMax);
        Velocity.z = Mathf.Sign(Joystick.Vertical) * Mathf.Min(Mathf.Abs(Joystick.Vertical) * ForwardSpeed, StrafeVelocityMax);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(State.BallRecieve());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(State.Kick());
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(State.Pass());
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
