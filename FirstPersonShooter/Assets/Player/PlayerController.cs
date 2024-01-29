/*
 * Jack Sherlock 1-29-2024
 * script for player controls (who woulda guessed)
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //cam vars
    public Camera cam;
    private Vector2 lookInput = Vector2.zero; //catches mouse input
    private float lookSpeed = 60;
    private float horizontalLookAngle = 0; //how far we should be looking along the horizontal rotation of the cam
    public bool invertX = false;
    public bool invertY = false;
    private int invertFactorX = 1;
    private int invertFactorY = 1;
    [Range(0.01f,1f)]public float sensitivity; //makes a slider in inspector

    //debug vars
    public TMP_Text debugText; //stores reference to debug text on the canvas

    //player input
    private Vector2 moveInput;
    private bool grounded;

    //movement vars
    private CharacterController charController;
    private Vector3 playerVelocity;
    private Vector3 wishDir = Vector3.zero;
    public float maxSpeed = 6;
    public float acceleration = 60;
    public float gravity = 20;
    public float stopSpeed = 0.5f;
    public float jumpImpulse = 10f;
    public float friction = 4;

    // Start is called before the first frame update
    void Start()
    {
        //hide mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        //invert camera
        if (invertX) invertFactorX = -1;
        if (invertY) invertFactorY = -1;

        //get reference to character controller
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Look();

    }

    public void GetLookInput(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void GetJumpInput(InputAction.CallbackContext context)
    {
        Jump();
    }

    //remember to bring a snack for before class bc I'm hungy and can't eat for another 2 hours from now goddammit
    //also we bringing back commenting out the wazoo yeahhhhh
    //i'm gonna be brutally honest here how did someone mess their project up this early into creation (we've been sitting here for like at least ten minutes now)


    private void Look()
    {
        //L/R 
        transform.Rotate(Vector3.up, lookInput.x * lookSpeed * Time.deltaTime * invertFactorX * sensitivity);

        //U/D
        float angle = lookInput.y * lookSpeed * Time.deltaTime * invertFactorY * sensitivity;
        //how the far the cam will rotate each frame
        horizontalLookAngle -= angle;
        horizontalLookAngle = Mathf.Clamp(horizontalLookAngle, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(horizontalLookAngle, 0, 0);
    }

    private void Jump()
    {
        if (grounded)
        {
            //do this later 
        }
    }

    private Vector3 Accelerate(Vector3 wishDir, Vector3 currentVelocity, float accel, float maxSpeed)
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

    private Vector3 MoveAir(Vector3 wishDir, Vector3 currentVelocity)
    {
    
        return Accelerate(wishDir, currentVelocity, acceleration, maxSpeed);
    }
}
