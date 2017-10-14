using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Instrument
{
    public EInstrument instrumentType;
    public GameObject GOInstrument;
}

public class PrefabsInstrumentDatabase : MonoSingleton<PrefabsInstrumentDatabase>
{
    public List<Instrument> instruments;

    public void ActivateInstrument(EInstrument instru)
    {
        for (int i = 0; i < instruments.Count; i++)
        {
            instruments[i].GOInstrument.SetActive(instruments[i].instrumentType == instru);
        }
    }
}
