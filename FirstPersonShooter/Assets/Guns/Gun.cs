//For his Neutral Special, he wields a GUN

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using TMPro;

public class Gun : MonoBehaviour
{
    //Gun vars
    public GunData gunData;
    public Camera cam;
    protected Ray ray;
    
    //Ammo
    protected int ammoInClip;

    //Shooting
    protected bool primaryFireIsShooting = false;
    protected bool primaryFireHold = false;
    protected float shootDelayTimer = 0.0f;

    //Debug
    public TMP_Text debugText;

    // Start is called before the first frame update
    void Start()
    {
        ammoInClip = gunData.ammoPerClip;
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = "Ammo in Mag: " + ammoInClip.ToString();

        PrimaryFire();

        if(shootDelayTimer > 0) shootDelayTimer -= Time.deltaTime;
    }

    public void GetPrimaryFireInput(InputAction.CallbackContext context)
    {
        //check for initial button press
        if (context.phase == InputActionPhase.Started)
        {
            primaryFireIsShooting = true;
        }

        //check if the gun is automatic
        if (gunData.automatic)
        {
            //check if the hold was completed
            if (context.interaction is HoldInteraction && context.phase == InputActionPhase.Performed)
            {
                primaryFireHold = true;
            }
        }

        //check for button release
        if (context.phase == InputActionPhase.Canceled)
        {
            primaryFireIsShooting = false;
            primaryFireHold = false;
        }
    }

    public void GetSecondaryFireInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) SecondaryFire();
    }

    protected virtual void PrimaryFire()
    {

    }
    protected virtual void SecondaryFire()
    {

    }
}
