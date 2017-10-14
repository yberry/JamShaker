using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FocusCam : MonoBehaviour {

    private Camera cam;
    private Vector3 initPosition = Vector3.zero;
    private Quaternion initRotation = Quaternion.identity;
    private float initSize = 5f;
    private bool isZooming = false;

    [Header("Durations")]
    public float zoomInDuration = 0.5f;
    public float zoomDuration = 2f;
    public float zoomOutDuration = 0.5f;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void Focus(Transform tr, float size)
    {
        if (isZooming)
        {
            return;
        }

        initPosition = transform.position;
        initRotation = transform.rotation;
        initSize = cam.orthographicSize;
        StartCoroutine(Transition(tr, size));
    }

    IEnumerator Transition(Transform tr, float size)
    {
        isZooming = true;

        float time = 0f;

        while (time <= zoomInDuration)
        {
            float coef = time / zoomInDuration;
            transform.position = Vector3.Lerp(initPosition, tr.position, coef);
            transform.rotation = Quaternion.Lerp(initRotation, tr.rotation, coef);
            cam.orthographicSize = Mathf.Lerp(initSize, size, coef);

            time += Time.deltaTime;
            yield return null;
        }
        transform.position = tr.position;
        transform.rotation = tr.rotation;
        cam.orthographicSize = size;

        yield return new WaitForSeconds(zoomDuration);

        time = 0f;

        while (time <= zoomOutDuration)
        {
            float coef = time / zoomOutDuration;
            transform.position = Vector3.Lerp(tr.position, initPosition, coef);
            transform.rotation = Quaternion.Lerp(tr.rotation, initRotation, coef);
            cam.orthographicSize = Mathf.Lerp(size, initSize, coef);

            time += Time.deltaTime;
            yield return null;
        }
        transform.position = initPosition;
        transform.rotation = initRotation;
        cam.orthographicSize = initSize;

        isZooming = false;
    }
}
