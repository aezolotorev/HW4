using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(AudioSource))]
internal sealed class AudioPlay : MonoBehaviour
{
    public AudioSource _audioSourse;
    public DataAudio DataSounds;
 
    private void Start()
    {
        _audioSourse = GetComponent<AudioSource>();
        

    }

    public void PlayAudio(string nameclip)
    {
        _audioSourse.clip=DataSounds.GetAudio(nameclip);
        _audioSourse.Play();
    }

   
}
