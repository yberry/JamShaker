using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FocusCam : MonoBehaviour {

    private Camera cam;

    private Vector3 initPosition = Vector3.zero;
    private Quaternion initRotation = Quaternion.identity;
    private float initSize = 5f;

    private Vector3 targetPosition = Vector3.zero;
    private Quaternion targetRotation = Quaternion.identity;
    private float targetSize = 5f;

    private bool isZooming = false;

    private static FocusCam instance = null;
    public static FocusCam Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Camera.main.gameObject.AddComponent<FocusCam>();
            }
            return instance;
        }
    }

    [Header("Durations")]
    public float zoomInDuration = 0.5f;
    public float zoomDuration = 2f;
    public float zoomOutDuration = 0.5f;

    void Awake()
    {
        cam = GetComponent<Camera>();
        instance = this;
    }

    public void Focus(Transform tr, float size)
    {
        if (!isZooming)
        {
            initPosition = transform.position;
            initRotation = transform.rotation;
            initSize = cam.orthographicSize;
        }
        targetPosition = tr.position;
        targetRotation = tr.rotation;
        targetSize = size;

        StopAllCoroutines();

        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        isZooming = true;

        float time = 0f;

        if (zoomInDuration > 0f)
        {
            while (time <= zoomInDuration)
            {
                float coef = time / zoomInDuration;
                transform.position = Vector3.Lerp(initPosition, targetPosition, coef);
                transform.rotation = Quaternion.Lerp(initRotation, targetRotation, coef);
                cam.orthographicSize = Mathf.Lerp(initSize, targetSize, coef);

                time += Time.deltaTime;
                yield return null;
            }
        }
        transform.position = targetPosition;
        transform.rotation = targetRotation;
        cam.orthographicSize = targetSize;

        time = 0f;

        while (time <= zoomDuration)
        {
            transform.position = targetPosition;
            transform.rotation = targetRotation;

            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;

        if (zoomOutDuration > 0f)
        {
            while (time <= zoomOutDuration)
            {
                float coef = time / zoomOutDuration;
                transform.position = Vector3.Lerp(targetPosition, initPosition, coef);
                transform.rotation = Quaternion.Lerp(targetRotation, initRotation, coef);
                cam.orthographicSize = Mathf.Lerp(targetSize, initSize, coef);

                time += Time.deltaTime;
                yield return null;
            }
        }
        transform.position = initPosition;
        transform.rotation = initRotation;
        cam.orthographicSize = initSize;

        isZooming = false;
    }
}
