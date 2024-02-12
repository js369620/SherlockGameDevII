using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public float maxSpeed = 6f;
    public float acceleration = 60f;
    public float gravity = 15f;


    public override void EnterState(PlayerStateMachine stateMachine)
    {
        //Debug.Log("Air State Enter");
    }
    public override void ExitState(PlayerStateMachine stateMachine)
    {
        //Debug.Log("Air State Exit");
        stateMachine.playerVelocity.y = -2;
    }
    public override void UpdateState(PlayerStateMachine stateMachine)
    {
        //Debug.Log("Air State Update");

    }
    public override void FixedUpdateState(PlayerStateMachine stateMachine)
    {
        //Debug.Log("Air State Fixed Update");
        //Gravity
        stateMachine.playerVelocity.y -= gravity * Time.deltaTime;

        //Set velocity 
        stateMachine.playerVelocity = MoveAir(stateMachine.wishDir, stateMachine.playerVelocity);

        //Switch states
        if (stateMachine.charController.isGrounded)
        {
            stateMachine.SwitchState(this, stateMachine.groundState);
        }
    }

    private Vector3 MoveAir(Vector3 wishDir, Vector3 currentVelocity)
    {

        return Accelerate(wishDir, currentVelocity, acceleration, maxSpeed);
    }
}
