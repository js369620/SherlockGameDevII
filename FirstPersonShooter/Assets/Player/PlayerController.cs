/*
 * Jack Sherlock 1-29-2024
 * script for player controls (who woulda guessed)
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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


    // Start is called before the first frame update
    void Start()
    {
        //hide mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        //invert camera
        if (invertX) invertFactorX = -1;
        if (invertY) invertFactorY = -1;

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
}
