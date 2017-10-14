using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EInstrument
{
    GUITAR,
    DRUM,
    VOICE,
    TROMBONE
}

[System.Serializable]
public class Pattern
{
    public AudioSource soundRegion;
    public AudioSource soundTrigger;
    public EInstrument typeInstrument;
    public int id;

    public void PlayRegionSound()
    {
        soundRegion.Play();
    }

    public void PlayTriggerSound()
    {
        soundTrigger.Play();
    }
}
