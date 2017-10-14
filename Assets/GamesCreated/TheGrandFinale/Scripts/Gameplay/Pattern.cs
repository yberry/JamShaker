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
    private AudioSource soundRegion;
    public EInstrument typeInstrument;
    public int id;

    public void PlayRegionSound()
    {

        soundRegion = AudioDatabase.Instance.Instrument;

        if (soundRegion.isPlaying)
            soundRegion.Stop();
        int randomID = Random.Range(0, AudioDatabase.Instance.clips.Find(s => s.instrumentType == typeInstrument).audioClips.Count);
        soundRegion.clip = AudioDatabase.Instance.clips.Find(s => s.instrumentType == typeInstrument).audioClips[randomID];
        soundRegion.Play();
    }
}
