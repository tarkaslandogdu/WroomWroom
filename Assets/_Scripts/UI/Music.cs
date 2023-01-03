using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource[] sounds;
    PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        BGSoundPreferance();
        PlayCarSound();
    }


    private void BGSoundPreferance()
    {
        if (PlayerPrefs.GetInt("sounds") == 1) AudioListener.pause = true;
        if (PlayerPrefs.GetInt("music") == 0 && !sounds[1].isPlaying) sounds[1].Play(); 
        if (PlayerPrefs.GetInt("music") == 1) sounds[1].Stop(); 
    }

    private void PlayCarSound()
    {
        if (PlayerPrefs.GetInt("sounds") == 0 && playerMovement.gameRuning)
        {
            if (sounds[0].isPlaying) return;
            if (!sounds[0].isPlaying) sounds[0].Play();
        }

        else if (!playerMovement.gameRuning) sounds[0].Stop(); 
    }
}