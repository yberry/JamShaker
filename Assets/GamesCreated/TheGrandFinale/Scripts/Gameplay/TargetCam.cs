using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TargetCam : MonoBehaviour {

    private Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	public void GetFocus()
    {
        FocusCam.Instance.Focus(transform, cam.orthographicSize);
    }
}
