using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    public ParticleSystem fireEffectPrefab;

    public void PlayEffect()
    {
        fireEffectPrefab.Play();
    }
}
