using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public float maxSpeed = 6;
    public float acceleration = 60;
    //public float gravity = 20;
    public float stopSpeed = 0.5f;
    public float jumpImpulse = 10f;
    public float friction = 4;


    public override void EnterState(PlayerStateMachine stateMachine)
    {
        //Debug.Log("Ground State Enter");
        
    }
    public override void ExitState(PlayerStateMachine stateMachine)
    {
        //Debug.Log("Ground State Exit");
    }
    public override void UpdateState(PlayerStateMachine stateMachine)
    {
        //Debug.Log("Ground State Update");
        stateMachine.SwitchState(this, stateMachine.airState);
    }
    public override void FixedUpdateState(PlayerStateMachine stateMachine)
    {
        //Debug.Log("Ground State Fixed Update");
        stateMachine.playerVelocity = MoveGround(stateMachine.wishDir, stateMachine.playerVelocity);

        //switch states
        if(!stateMachine.charController.isGrounded)
        {
            stateMachine.SwitchState(this, stateMachine.airState);
        }
        if(stateMachine.jumpButtonPressed)
        {
            stateMachine.playerVelocity.y = jumpImpulse;
            stateMachine.SwitchState(this, stateMachine.airState);
        }
    }

    private Vector3 MoveGround(Vector3 wishDir, Vector3 currentVelocity)
    {
        Vector3 newVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);

        float speed = newVelocity.magnitude;
        if (speed <= stopSpeed)
        {
            newVelocity = Vector3.zero;
            speed = 0;
        }

        if (speed != 0)
        {
            float drop = speed * friction * Time.deltaTime;
            newVelocity *= Mathf.Max(speed - drop, 0) / speed;
        }

        newVelocity = new Vector3(newVelocity.x, currentVelocity.y, newVelocity.z);

        return Accelerate(wishDir, newVelocity, acceleration, maxSpeed);
    }
}
