/*
 * Author: Alex Kam
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Singleton
    public static CameraFollow CF;

    //Player that is being followed
    public GameObject playerGO;
    public Transform player;

    //Zeroing out the velocity
    Vector3 velocity = Vector3.zero;
    
    //Time to follow target
    public float smoothTime = .15f;

    //Enable and set the max Y value
    public bool YMaxEnabled = false;
    public float YMaxValue = 0f;

    //Enable and set the min Y value
    public bool YMinEnabled = false;
    public float YMinValue = 0f;

    //Enable and set the max X value
    public bool XMaxEnabled = false;
    public float XMaxValue = 0f;

    //Enable and set the min X value
    public bool XMinEnabled = false;
    public float XMinValue = 0f;

    void Start()
    {
        if(playerGO == null)
            playerGO = GameObject.FindWithTag("Player");
        player = playerGO.transform;
        if (CameraFollow.CF == null)
            CameraFollow.CF = this;
    }

    void FixedUpdate()
    {
        //Error Prevention
        if (player == null)
            return;
        //Target position
        Vector3 playerPos = player.position;

        //Vertical Clamping
        if (YMinEnabled && YMaxEnabled)
        {
            playerPos.y = Mathf.Clamp(player.position.y, YMinValue, YMaxValue);
        }
        else if (YMinEnabled)
        {
            playerPos.y = Mathf.Clamp(player.position.y, YMinValue, player.position.y);
        }
        else if (YMaxEnabled)
        {
            playerPos.y = Mathf.Clamp(player.position.y, player.position.y, YMaxValue);
        }

        //Horizontal Clamping
        if (XMinEnabled && XMaxEnabled)
        {
            playerPos.x = Mathf.Clamp(player.position.x, XMinValue, XMaxValue);
        }
        else if (XMinEnabled)
        {
            playerPos.x = Mathf.Clamp(player.position.x, XMinValue, player.position.x);
        }
        else if (XMaxEnabled)
        {
            playerPos.x = Mathf.Clamp(player.position.x, player.position.x, XMaxValue);
        }

        //Align camera and the player's z position
        playerPos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref velocity, smoothTime);
    }

    //===============PROCEDURE===============//
    public IEnumerator Shake(float duration, float magnitude)
    //Purpose:          To shake the camera in a random way
    //float duration:    Controls how long the shake occurs
    //float magnitude: Controls how powerful the shake is
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;
        while(elapsed<duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y+y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}