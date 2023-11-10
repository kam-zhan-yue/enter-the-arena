/*
 * Author: Alex Kam
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar: MonoBehaviour
{
    //Declare Vector3 to be healthbar's position
    Vector3 localScale;

    //Declare Color fields
    [SerializeField] public Color fullColour;
    [SerializeField] public Color lowColour;
    [SerializeField] public Image bar;

    void Start()
    {
        localScale = transform.localScale;
    }

    void Update()
    {
        HealthController();
    }

    //===============PROCEDURE===============//
    void HealthController()
    //Purpose:          Adjusts the colour and size of the HP bar according to its value
    {
        localScale.x = 3 * PlayerController.healthAmount / 100;
        transform.localScale = localScale;
        //Change colour from Green to Red according to health amount
        bar.color = Color.Lerp(lowColour, fullColour, PlayerController.healthAmount/100);
    }
}
