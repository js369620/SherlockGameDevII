using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using TMPro;

public class Pistol : MonoBehaviour
{
    public GunData gunData;
    public Camera cam;
    private Ray ray;
    private int ammoInClip;

    //Shooting
    private bool primaryFireIsShooting = false;
    private bool primaryFireHold = false;
    private float shootDelayTimer = 0.0f;

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
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 10000, Color.green);

        debugText.text = "Ammo In Mag: " + ammoInClip.ToString();

        PrimaryFire();

        if (shootDelayTimer > 0) shootDelayTimer -= Time.deltaTime;
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
            if(context.interaction is HoldInteraction && context.phase == InputActionPhase.Performed)
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

    private void PrimaryFire()
    {
        if (shootDelayTimer <= 0)
        {
            //delay gun from shooting again
            shootDelayTimer = gunData.primaryFireDelay;

            if (primaryFireIsShooting || primaryFireHold)
            {
                primaryFireIsShooting = false;

                Vector3 dir = Quaternion.AngleAxis(Random.Range(-gunData.spread, gunData.spread), Vector3.up) * cam.transform.forward;
                dir = Quaternion.AngleAxis(Random.Range(-gunData.spread, gunData.spread), Vector3.right) * dir;

                ray = new Ray(cam.transform.position, dir);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, gunData.range))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green, 0.05f);
                    //print(ammoInClip);
                }
                ammoInClip--;
                if (ammoInClip <= 0) ammoInClip = gunData.ammoPerClip;
            }
        }
        

        
    }

    private void SecondaryFire()
    {

    }
}
