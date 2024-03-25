using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    protected override void PrimaryFire()
    {
        if (shootDelayTimer <= 0)
        {
            //delay gun from shooting again
            shootDelayTimer = gunData.primaryFireDelay;
            primaryFireIsShooting = false;

            //shoots 6 pellets
            for(int i = 0; i < 6; i++)
            {
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

                    //trails
                    TrailRenderer trail = Instantiate(bulletTrail, shootPoint.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, dir, hit));


                }
            }
                    ammoInClip--;
                    if (ammoInClip <= 0) ammoInClip = gunData.ammoPerClip;

                    muzzleFlash.Play();
            
        }
    }
}
