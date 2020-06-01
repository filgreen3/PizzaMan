using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    AudioSource audio;
   
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void PlayThisSound(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }
}
