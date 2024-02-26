using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

/* 
 *     o
 *    o   ____
 *     o |    |
 *    o _|____|_
 *       (''>){
 *  blublublub imma fish wit a top hat
 * 
 * */

/*
 *    <_ `
 *      \ \ _
 *       \ ( o_o) F
 *        <     ` A
 *        /    ^ \ B
 *       /   /   \ \ U
 *      |    )     ~_ ) L
 *      /   /            O
 *     /   /|             U
 *    (   ( `              S
 *    |   |` \
 *    |  /  \ )
 *    | |    )/
 *    j )   Lj
 *   __/
 *   
 * */

/*
 * 
 * 1 meow, but            /\_______/\    ___________
 * 2 all sexy-like     __( ' O w O ' ) |               = > =-_
 * 3               _/   `  |     /    / '         "            `
 * 4          __--          |   /               '        ,       }
 * 5         ,        )   ` |              _,""           |      |
 * 6       /         /     `        __--"'                 |    /\
 * 7      /       ,                            ,              j   |
 * 8      |        T          i                 " -=  `            |
 * 9     /{         |          |                      /            \
 * 10   '        ,,  |          |                   ,|            | }
 * 11  {      '""  |            |                    |         !   |
 * 12   |           |',         |                   , \       /   }
 * 13  /,          /  \        / \                _'    > =     \  |
 * 14 {  "'   ' ,,,     '-___,' ...'-___  ____ --     /          | /
 * 15 !            \         \       ,,             , '         | /
 * 16 '        _____ \         \  __ .            -"             /
 * 17  '      ,     -- >\   ___/'    -_---_   "                /
 * 18   '   /   ____    )\ /    _     !     |[        ,    = /
 * 19    ' /   ______ _)  |  _- __    |     |           _  '
 * 20     `! __       )    |  -       |     |         -
 * 
 * "That's what a catboy should be" - Rowan Knutsen 2024
 * 
 * 
 * 
 * 
 * */

public class PlayerStateMachine : MonoBehaviour
{
    //State stuff
    private PlayerBaseState currentState;
    public PlayerGroundState groundState = new PlayerGroundState();
    public PlayerAirState airState = new PlayerAirState();

    //debug vars
    public TMP_Text debugText;

    //player input
    [HideInInspector] public Vector2 moveInput;
    //[HideInInspector] public bool grounded;

    //movement vars
    [HideInInspector] public CharacterController charController;
    [HideInInspector] public Vector3 playerVelocity;
    [HideInInspector] public Vector3 wishDir = Vector3.zero;
    [HideInInspector] public bool jumpButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        //get reference to character controller
        charController = GetComponent<CharacterController>();

        currentState = groundState;

        currentState.EnterState(this);

    }

    // Update is called once per frame
    void Update()
    {

        currentState.UpdateState(this);
        DebugText();
    }

    private void FixedUpdate()
    {
        FindWishDir();
        currentState.FixedUpdateState(this);
        MovePlayer();
    }

    public void SwitchState(PlayerBaseState curState, PlayerBaseState newState)
    {
        curState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void GetJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started) jumpButtonPressed = true;
        if(context.phase == InputActionPhase.Canceled) jumpButtonPressed = false;
    }

    public void DebugText()
    {
        debugText.text = "wishdir: " + wishDir.ToString();
        debugText.text += "\nPlayer Velocity: " + playerVelocity.ToString();
        debugText.text += "\nPlayer Speed: " + new Vector3(playerVelocity.x, 0, playerVelocity.z).magnitude.ToString();
        //debugText.text += "\nGrounded: " + grounded.ToString();
        debugText.text += "\nState: " + currentState.ToString();
    }

    public void FindWishDir()
    {
        //find wishdir
        wishDir = transform.right * moveInput.x + transform.forward * moveInput.y;
        wishDir = wishDir.normalized;
    }

    public void MovePlayer()
    {
        charController.Move(playerVelocity * Time.deltaTime);
    }
}
