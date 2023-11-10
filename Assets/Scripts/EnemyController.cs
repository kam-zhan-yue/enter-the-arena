/*
 * Author: Yuta Kataoka
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem enemyDeath;
    private GameObject playerObject;
    private Rigidbody2D rb;
    public float speed;

    float distanceToPlayer;
    int strafeDir;
    float timer;

    public float range;

    public GameObject gun;

    public float healthAmount;
    
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        timer = Time.time + 2;
        if (gameObject.tag == "Enemy")
            healthAmount = 100f * (SpawnController.SC.multiplier / 2f);
        if(gameObject.tag == "Boss")
            healthAmount = 500f * (SpawnController.SC.multiplier / 2f);
    }

    void Update()
    {
        //Error Prevention
        if (playerObject == null)
            return;
        distanceToPlayer = Vector2.Distance(playerObject.GetComponent<Rigidbody2D>().transform.position, transform.position);
        LookAt();

        if (distanceToPlayer < range)
        {
            gun.GetComponent<EnemyShoot>().Shoot();
        }

        if (healthAmount <= 0f)
        {
            Debug.Log("Enemy dead, instantiating particles...");
            Instantiate(enemyDeath, transform.localPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        ZombieAI();
    }

    //===============PROCEDURE===============//
    private void ZombieAI()
    //Purpose:          Calculates distance towards player and moves accordingly
    {
        if (distanceToPlayer > 9.9)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * speed * Time.deltaTime);
            Strafe();
        }
        else if(distanceToPlayer < 9.5)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * speed * Time.deltaTime);
        }
        else
        {
            Strafe();
        }
    }

    //===============PROCEDURE===============//
    private void LookAt()
    //Purpose:          Rotates the enemy to face towards the player
    {
        Vector3 diff = playerObject.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    //===============PROCEDURE===============//
    private void Strafe()
    //Purpose:          Makes the enemy strafe, making it harder to hit
    {
        if (Time.time >= timer)
        {
            strafeDir = Random.Range(0, 3);
            timer = Time.time + 1;
        }

        if (strafeDir == 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * speed * Time.deltaTime);
        }
        else if (strafeDir == 1)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.right * speed * Time.deltaTime);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (strafeDir == 0)
        {
            strafeDir = 1;
        }
        else if (strafeDir == 1)
        {
            strafeDir = 0;
        }
        timer = Time.time + 1;
    }
}
