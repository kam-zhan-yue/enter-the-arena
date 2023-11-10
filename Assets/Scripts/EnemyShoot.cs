/*
 * Author: Yuta Kataoka
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject origin;

    public float fireRate;
    public float damage;
    public float velocity;
    public float duration;
    public string type;

    private float nextTimeToFire = 0f;

    //===============PROCEDURE===============//
    public void Shoot()
    //Purpose:          Shoots projectiles depending on the enemy's type and gun
    {
        if (type == "auto")
        {
            if (Time.time >= nextTimeToFire)
            {
                AudioManager.AM.Play("Enemy");
                LaunchProjectile();
                nextTimeToFire = Time.time + 1f / fireRate;
            }
        }
        else if (type == "semi")
        {
            if (Time.time >= nextTimeToFire)
            {
                LaunchProjectile();
                nextTimeToFire = Time.time + 1f / fireRate;
            }
        }
        else if(type == "boss")
        {
            if (Time.time >= nextTimeToFire)
            {
                AudioManager.AM.Play("Boss");
                LaunchProjectile();
                nextTimeToFire = Time.time + 1f / fireRate;
            }
        }
        
    }

    //===============PROCEDURE===============//
    public void LaunchProjectile()
    //Purpose:          Shoots a projectile with the enemy's stats
    {
        GameObject shotBullet = Instantiate(bullet, origin.transform.position, origin.transform.rotation);
        shotBullet.GetComponent<Bullet>().source = "Enemy";
        shotBullet.GetComponent<Bullet>().damage = damage;
        shotBullet.GetComponent<Bullet>().velocity = velocity;
        shotBullet.GetComponent<Bullet>().duration = duration;
    }
}
