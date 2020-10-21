using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public ParticleSystem bloodEffectPrefab;

    public void PlayEffect()
    {
        bloodEffectPrefab.Play();
    }
}
