/*
 * Author: Alex Kam
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //Game Over Variables
    public static GameOver GO;
    public bool gameOver = false;
    public GameObject gameOverCanvas;

    //Text Variables
    public TextMeshProUGUI name;
    public TextMeshProUGUI waves;

    private void Awake()
    {
        if (GameOver.GO == null)
            GameOver.GO = this;
    }

    //===============PROCEDURE===============//
    public void EndGame()
    //Purpose:          Ends the game and puts up the game over window
    {
        gameOver = true;
        if (gameOver)
        {
            Debug.Log("Game Over");
            AudioManager.AM.Play("GameOver");
            name.text = PlayerController.PC.myName;
            waves.text = PlayerController.PC.wavesCompleted.ToString() ;
            gameOverCanvas.SetActive(true);
            gameOver = false;
            GameController.GC.addEntry = true;
        }
    }

    //===============PROCEDURE===============//
    public void Quit()
    //Purpose:          Quits the application if the button is chosen
    {
        Debug.Log("Go back to menu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Retry()
    //Purpose:          Restarts the game scene if the button is chosen
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
