using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DataSound", menuName ="Assets/DataSound", order=1)]
public class DataAudio : ScriptableObject
{
    [SerializeField] List<DataSound> DataSounds = new List<DataSound>();

    [Serializable]
    private sealed class DataSound
    {
        public string nameClip;
        public AudioClip Clip;
    }  

    public AudioClip GetAudio(string nameclip)
    {
        AudioClip result = null;
        foreach(var data in DataSounds)
        {

            if (data.nameClip == nameclip)
            {
                result = data.Clip;
                return result;
            }
        }
        return result;
    }
}
