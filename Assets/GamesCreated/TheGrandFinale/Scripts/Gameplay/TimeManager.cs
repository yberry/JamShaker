using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BPMStruct
{
    public int level;
    public int BPM;
    public int secondes;
}

public class TimeManager : MonoSingleton<TimeManager> {

    [SerializeField]
    public List<BPMStruct> bpmStructs;


    public float getActualSeconds()
    {
        return (bpmStructs.Find(str => str.level == Gameplay.Instance.actualLevel).secondes);
    }
}
