/*
 * PlayerBaseState.cs 2-12-2024
 * 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState //we will NOT inherit from MonoBehaviour
{
    public abstract void EnterState(PlayerStateMachine stateMachine);
    public abstract void ExitState(PlayerStateMachine stateMachine);
    public abstract void UpdateState(PlayerStateMachine stateMachine);
    public abstract void FixedUpdateState(PlayerStateMachine stateMachine);

    public Vector3 Accelerate(Vector3 wishDir, Vector3 currentVelocity, float accel, float maxSpeed)
    {
        //project current speed onto wishDir
        float projectedSpeed = Vector3.Dot(currentVelocity, wishDir);
        //how fast we need to accelerate the player
        float accelSpeed = accel * Time.deltaTime;
        //cap our speed on ground
        if (projectedSpeed + accelSpeed > maxSpeed)
        {
            accelSpeed = maxSpeed - projectedSpeed;
        }

        return currentVelocity + wishDir * accelSpeed;
    }
}
