using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StructClips
{
    public EInstrument instrumentType;
    public List<AudioClip> audioClips;
}

public class AudioDatabase : MonoSingleton<AudioDatabase> {

    public AudioSource Back;
    public AudioSource Instrument;

    [SerializeField]
    public List<StructClips> clips;
}
