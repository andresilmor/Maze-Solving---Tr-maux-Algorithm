using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsOfType<MusicController>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayMusic()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play(0);
        audioSource.loop = true;
    }
}
