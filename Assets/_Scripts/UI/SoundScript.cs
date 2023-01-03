using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    void Awake()
    {
        GameObject[] settings = GameObject.FindGameObjectsWithTag("Settings");

        if (settings.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
