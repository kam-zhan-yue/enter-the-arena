/*
 * Author: Alex Kam
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Singleton
    public static PlayerController PC;

    //Speed Variables
    public float speed;
    private Rigidbody2D rb;

    //Health Variables
    public static float healthAmount;
    public static float maxHealth;

    //Customisation Variables
    public SpriteRenderer player;
    public string myName;
    public Color[] playerColour = new Color[4];

    //Wave Variables
    public int wavesCompleted = 0;

    private void Awake()
    {
        if (PlayerController.PC == null)
            PlayerController.PC = this;
    }

    void Start()
    {
        UsePlayerChoices(GameController.GC.colourChoice, GameController.GC.playerName);
    }

    void Update()
    {
        FaceMouse(Input.mousePosition);

        if(healthAmount<=0)
        {
            Destroy(gameObject);
            GameOver.GO.EndGame();
        }
        //rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        wavesCompleted = SpawnController.SC.wavesCompleted;
    }

    void FixedUpdate()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move(moveInput);
    }

    //===============PROCEDURE===============//
    void UsePlayerChoices(int colourChoice, string playerName)
    //Purpose:          Sets the player settings to what was chosen
    //int colourChoice: Determines what colour the player is
    //string playerName: Determines what name the player has
    {
        //Declare health and max health
        healthAmount = 100f;
        maxHealth = 100f;

        rb = GetComponent<Rigidbody2D>();

        //Change colour according to what was chosen
        player.color = playerColour[colourChoice];

        //Change name according to what was typed
        if (string.IsNullOrEmpty(playerName))
            myName = "Player";
        myName = playerName;
    }

    //===============PROCEDURE===============//
    void Move(Vector2 dir)
    //Purpose:          Moves the player in the direction that was inputted
    //Vector2 dir:      Determines the x and y direction of the player
    {
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }

    //===============PROCEDURE===============//
    void FaceMouse(Vector3 input)
    //Purpose:          Rotates the player towards the position of the mouse
    //Vector3 input:   Stores the x,y,z location of the mouse
    {
        Vector3 mousePosition = input;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);
        transform.up = direction ;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("spike"))
        {
            healthAmount -= 1f;
        }

        if (healthAmount <= maxHealth)
        {
            if (col.gameObject.tag.Equals("healthPack"))
            {
                healthAmount += 10f;
                if(healthAmount>maxHealth)
                {
                    healthAmount = maxHealth;
                }
            }
        }
        /*
        if (col.gameObject.tag == "bullet")
        {
            if (col.gameObject.GetComponent<Bullet>().source == "Enemy")
            {
                healthAmount -= col.gameObject.GetComponent<Bullet>().damage;
                Destroy(col.gameObject);
            }
        }
        */
    }
}