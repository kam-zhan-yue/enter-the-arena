/*
 * Author: Yuta Kataoka
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject bullet;
    public GameObject origin;

    public float fireRate;
    public float damage;
    public float velocity;
    public float duration;
    public string type;

    private float nextTimeToFire = 0f;
    
    void Start()
    {
        if (GameController.GC.fireRate < 0 || GameController.GC.bulletSpeed < 0 
            || GameController.GC.damage < 0)
        {
            Debug.LogError("Player's stats are negative.");
            return;
        }
        else;
        {
            fireRate = 2 + GameController.GC.fireRate;
            velocity = 10 + GameController.GC.bulletSpeed;
            damage = 5 + GameController.GC.damage;
        }
    }
    
    void Update()
    {
        if(type == "auto")
        {
            if(Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
            {
                AudioManager.AM.Play("Player");
                LaunchProjectile();
                nextTimeToFire = Time.time + 1f / fireRate;
            }
        }
        else if (type == "semi")
        {
            if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
            {
                LaunchProjectile();
                nextTimeToFire = Time.time + 1f / fireRate;
            }
        }
    }

    //===============PROCEDURE===============//
    void LaunchProjectile()
    //Purpose:          Shoots a projectile with the player's stats
    {
        GameObject playerBullet = Instantiate(bullet, origin.transform.position, origin.transform.rotation);
        playerBullet.GetComponent<Bullet>().source = "player";
        playerBullet.GetComponent<Bullet>().damage = damage;
        playerBullet.GetComponent<Bullet>().velocity = velocity;
        playerBullet.GetComponent<Bullet>().duration = duration;
    }
    
}

