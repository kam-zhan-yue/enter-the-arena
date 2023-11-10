/*
 * Author: Yuta Kataoka
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string source;

    private GameObject gun;
    public Rigidbody2D rb;

    public float damage;
    public float velocity;
    public float duration;

    private float time = 0;

    private Vector2 angle;

    public static int healthAmount;

    // Start is called before the first frame update
    void Start()
    {
        angle = gameObject.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        time += 1 * Time.deltaTime;
        gameObject.transform.Translate(Vector2.up * velocity * Time.deltaTime);
        if(time > duration)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "bullet")
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }

        if (source == "player")
        {
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag=="Boss")
            {
                Destroy(gameObject);
                AudioManager.AM.Play("EnemyHit");
                StartCoroutine(CameraFollow.CF.Shake(.15f,.4f));
                other.gameObject.GetComponent<EnemyController>().healthAmount -= damage;
            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(gameObject);
                AudioManager.AM.Play("PlayerHit");
                StartCoroutine(CameraFollow.CF.Shake(.5f, .6f));
                PlayerController.healthAmount -= damage;
            }
        }
    }
}