using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SPattern
{
    public EInstrument instrumentType;
    public List<Pattern> patterns;
}

public class PatternDatabase : MonoSingleton<PatternDatabase> {

    [SerializeField]
    public List<SPattern> sPatterns;

}
