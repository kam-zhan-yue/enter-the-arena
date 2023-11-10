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

public class WaveUI : MonoBehaviour
{
    public TextMeshProUGUI waveCounter;
    public float timeOnScreen = 2f;
    public int previousWave;
    void Update()
    {
        if (SpawnController.SC.waveCountdown > 0)
            timeOnScreen = 2f;
        if (SpawnController.SC.waveCountdown <= 0 && timeOnScreen >= 0)
        {
            waveCounter.enabled = true;
            waveCounter.text = "Wave " + (SpawnController.SC.wavesCompleted + 1).ToString();
            timeOnScreen -= Time.deltaTime;
        }
        else
            waveCounter.enabled = false;

    }
}
