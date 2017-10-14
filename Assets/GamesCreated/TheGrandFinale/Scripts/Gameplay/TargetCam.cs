using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TargetCam : MonoBehaviour {

    private Camera cam;
    private FocusCam focus;

	// Use this for initialization
	void Awake () {
        cam = GetComponent<Camera>();
        focus = Camera.main.GetComponent<FocusCam>();
	}
	
	public void GetFocus()
    {
        focus.Focus(transform, cam.orthographicSize);
    }
}
