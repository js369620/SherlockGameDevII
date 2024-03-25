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

    //Trail/particle vars
    [SerializeField]
    protected Transform shootPoint;
    [SerializeField]
    protected TrailRenderer bulletTrail;
    [SerializeField]
    protected ParticleSystem muzzleFlash;
    [SerializeField]
    protected ParticleSystem impactParticles;

    //"everyone got this down okay?"
    //"eh" - Rowan, 2024

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

    protected IEnumerator SpawnTrail(TrailRenderer trail, Vector3 direction, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;
        Vector3 endPosition = Vector3.zero;

        if (hit.point == Vector3.zero)
        {
            endPosition = startPosition + direction * 100;
        }
        else endPosition = hit.point;

        while(time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, endPosition, time);
             //lerp is starting at one value, ending at another value, and increment by certain third value (i think, he said it too fast for me to type)
            time += Time.deltaTime / trail.time;
            yield return null;
        }

        if(hit.point != Vector3.zero)
        {
            Instantiate(impactParticles, hit.point, Quaternion.LookRotation(hit.normal));
        }
        Destroy(trail.gameObject, trail.time);
    }
}
