using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform Mc;
    public Transform Ai;
    private float nextActionTime = 0.0f; //start time
    public float period = 2f; //interpolation period 2 seconds
    void Update()
    {
        
        if (Vector3.Distance(Mc.position, transform.position) < 9 || Vector3.Distance(Ai.position, transform.position) < 9)
        {

            if (Time.time > nextActionTime) // if the time > than the interpolation period, it will trigger
            {
                nextActionTime = Time.time + period; //the interpolation period will update from 0.0f to 2.0f to 4.0f so every 2 seconds it is triggered
              
                Shoot();
            }
            
        } 
       
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
