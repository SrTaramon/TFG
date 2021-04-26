using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds){
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;
           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
           s.source.loop = s.loop;
        }
    }

    void Start() {
        if (SceneManager.GetActiveScene().name == "Map" || SceneManager.GetActiveScene().name == "Intro" || SceneManager.GetActiveScene().name == "Start") Play("Theme");
    }

    public void Play (string name){
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }

    public void Stop (string name){
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Stop();
    }
}
