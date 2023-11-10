/*
 * Author: Alex Kam 
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    public Text nameUI;
    void Start()
    {
        if (CustomisationOptions.CO.playerName == null)
            nameUI.text = "Player";
        else
            nameUI.text = CustomisationOptions.CO.playerName;
    }
}
