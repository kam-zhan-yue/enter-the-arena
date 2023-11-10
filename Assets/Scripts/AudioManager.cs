/*
 * Author: Alex Kam
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AM;
    public AudioMixerGroup mixer;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume;
        [Range(.1f,3f)]
        public float pitch;
        [HideInInspector]
        public AudioSource source;
    }

    public Sound[] sounds;

    void Awake()
    {
        if (AudioManager.AM == null)
            AudioManager.AM = this;
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = mixer;
        }
    }

    //===============PROCEDURE===============//
    public void Play(string name)
    //Purpose:          To play a specific audio clip
    //string name:      Finds the name of the audio
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //Error prevention
        if (s == null)
            Debug.LogError("Sound was not found, cannot play clip");
        else
            s.source.Play();
    }
}