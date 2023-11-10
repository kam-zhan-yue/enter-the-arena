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
using TMPro;

public class CustomisationOptions : MonoBehaviour
{
    //Create a singleton/record of the game object
    public static CustomisationOptions CO;

    //Name Variables
    public Text nameText;
    public string playerName;

    //Colour Variables
    public GameObject border;
    public Transform[] buttons = new Transform[4];
    public Image player;
    public Color[] playerColour = new Color[4];
    public int colourChoice;

    //Slider Variables
    public Slider[] sliders = new Slider[3];
    public GameObject[] addButtons = new GameObject[3];
    public float maxPoints = 10f;
    public float fireRate = 0f;
    public float bulletSpeed = 0f;
    public float damage = 0f;
    public float sumOfPoints;
    public TextMeshProUGUI[] pointsText = new TextMeshProUGUI[3];
    public TextMeshProUGUI maxPointsText;

    private void Awake()
    {
        //Creates a singleton
        if (CustomisationOptions.CO == null)
            CustomisationOptions.CO = this;
    }

    void Update()
    {
        player.color = playerColour[colourChoice];
        //Changes name of the player according to the input field
        playerName = nameText.text;

        if (sliders[0] != null)
        {
            sumOfPoints = SumOfSliders(sliders);
            if (sumOfPoints >= maxPoints)
            {
                foreach (GameObject item in addButtons)
                    item.SetActive(false);
            }
            else
            {
                foreach (GameObject item in addButtons)
                    item.SetActive(true);
            }
        }
        maxPointsText.text = "Max Points: " + (maxPoints - sliders[0].value - sliders[1].value - sliders[2].value).ToString();
        pointsText[0].text = sliders[0].value.ToString();
        pointsText[1].text = sliders[1].value.ToString();
        pointsText[2].text = sliders[2].value.ToString();
    }

    //===============PROCEDURE===============//
    public void White()
    //Purpose:          Selects white when button is chosen
    {
        Debug.Log("White was chosen");
        border.SetActive(true);
        border.transform.position = buttons[0].position;
        colourChoice = 0;
        FindObjectOfType<AudioManager>().Play("Button");
    }

    //===============PROCEDURE===============//
    public void Blue()
    //Purpose:          Selects blue when button is chosen
    {
        Debug.Log("Blue was chosen");
        border.SetActive(true);
        border.transform.position = buttons[1].position;
        colourChoice = 1;
        FindObjectOfType<AudioManager>().Play("Button");
    }

    //===============PROCEDURE===============//
    public void Red()
    //Purpose:          Selects red when button is chosen
    {
        Debug.Log("Red was chosen");
        border.SetActive(true);
        border.transform.position = buttons[2].position;
        colourChoice = 2;
        FindObjectOfType<AudioManager>().Play("Button");
    }

    //===============PROCEDURE===============//
    public void Purple()
    //Purpose:          Selects purple when button is chosen
    {
        Debug.Log("Purple was chosen");
        border.SetActive(true);
        border.transform.position = buttons[3].position;
        colourChoice = 3;
        FindObjectOfType<AudioManager>().Play("Button");
    }

    //===============PROCEDURE===============//
    public void IncreaseFireRate()
    //Purpose:          Increases fire rate when button is chosen
    {
        sliders[0].value += 1;
        fireRate = sliders[0].value * 2;
        Debug.Log("Fire Rate is: " + fireRate);
        FindObjectOfType<AudioManager>().Play("Add");
    }

    //===============PROCEDURE===============//
    public void DecreaseFireRate()
    //Purpose:          Decreases fire rate when button is chosen
    {
        sliders[0].value -= 1;
        fireRate = sliders[0].value * 2;
        Debug.Log("Fire Rate is: " + fireRate);
        FindObjectOfType<AudioManager>().Play("Delete");
    }

    //===============PROCEDURE===============//
    public void IncreaseBulletSpeed()
    //Purpose:          Increases bullet speed when button is chosen
    {
        sliders[1].value += 1;
        bulletSpeed = sliders[1].value * 8;
        Debug.Log("Bullet Speed is: " + bulletSpeed);
        FindObjectOfType<AudioManager>().Play("Add");
    }

    //===============PROCEDURE===============//
    public void DecreaseBulletSpeed()
    //Purpose:          Decreases bullet speed when button is chosen
    {
        sliders[1].value -= 1;
        bulletSpeed = sliders[1].value * 8;
        Debug.Log("Bullet Speed is: " + bulletSpeed);
        FindObjectOfType<AudioManager>().Play("Delete");
    }

    //===============PROCEDURE===============//
    public void IncreaseDamage()
    //Purpose:          Increases damage when button is chosen
    {
        sliders[2].value += 1;
        damage = sliders[2].value * 10;
        Debug.Log("Damage is: " + damage);
        FindObjectOfType<AudioManager>().Play("Add");
    }

    //===============PROCEDURE===============//
    public void DecreaseDamage()
    //Purpose:          Decreases damage when button is chosen
    {
        sliders[2].value -= 1;
        damage = sliders[2].value * 10;
        Debug.Log("Damage is: " + damage);
        FindObjectOfType<AudioManager>().Play("Delete");
    }

    //===============FUNCTION===============//
    public float SumOfSliders(Slider[] sliders)
    //Purpose:          Finds the sum of the value of the sliders
    //Slider[] sliders: Calls all available sliders in the array
    {
        float sum = 0f;
        foreach (Slider item in sliders)
            sum += item.value;
        return sum;
    }
}
