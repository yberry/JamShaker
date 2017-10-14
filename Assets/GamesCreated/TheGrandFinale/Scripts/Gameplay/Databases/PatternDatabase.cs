using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternDatabase : MonoSingleton<PatternDatabase> {

    [SerializeField]
    public List<Pattern> patterns;

}
