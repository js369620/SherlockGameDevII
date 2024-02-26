using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using TMPro;

public class Pistol : Gun
{


    protected override void PrimaryFire() //virtual fns need to be overridden in the child
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
                    print(ammoInClip);
                }
                ammoInClip--;
                if (ammoInClip <= 0) ammoInClip = gunData.ammoPerClip;
            }
        }
        

        
    }

    
}
