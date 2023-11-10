/*
 * Author: Alex Kam
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController GC;
    //Player Variables
    public int colourChoice;
    public string playerName;
    public float fireRate;
    public float bulletSpeed;
    public float damage;
    public int wavesCompleted;
    public bool addEntry = false;

    private void Awake()
    {
        if (GameController.GC == null)
            GameController.GC = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if(addEntry)
        {
            Debug.Log(PlayerController.PC.wavesCompleted + PlayerController.PC.myName);
            Highscores.AddHighscoreEntry(PlayerController.PC.wavesCompleted, PlayerController.PC.myName);
            Debug.Log("Added Entry");
            addEntry = false;
        }

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.visible = true;
            GameObject customisation = GameObject.Find("CustomiseMenu");
            if(customisation!=null)
            {
                SetPlayerChoices();
            }
        }
    }

    //===============PROCEDURE===============//
    void SetPlayerChoices()
    //Purpose:          Sets the player's choices to the ones that were chosen previously
    {
        colourChoice = CustomisationOptions.CO.colourChoice;
        playerName = CustomisationOptions.CO.playerName;
        fireRate = CustomisationOptions.CO.fireRate;
        bulletSpeed = CustomisationOptions.CO.bulletSpeed;
        damage = CustomisationOptions.CO.damage;
    }

    //===============PROCEDURE===============//
    public void PlayGame()
    //Purpose:          Starts the main game if the button is chosen
    {
        Debug.Log("Start button was pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //===============PROCEDURE===============//
    public void QuitGame()
    //Purpose:          Quits the application if the button is chosen
    {
        Debug.Log("Quit button was pressed");
        Application.Quit();
    }
}
