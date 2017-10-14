using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct structTarget
{
    public EInstrument instrument;
    public TargetCam cam;
}

public class TargetCamDatabase : MonoSingleton<TargetCamDatabase> {

    [SerializeField]
    public List<structTarget> targets;

    public void FocusTarget(EInstrument instru)
    {
        targets.Find(tar => tar.instrument == instru).cam.GetFocus();
    }
}
