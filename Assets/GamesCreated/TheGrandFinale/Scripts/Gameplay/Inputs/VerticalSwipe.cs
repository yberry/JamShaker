using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSwipe : MonoBehaviour {

	public int stack = 0;
	public Coroutine co;
	public ESwipeType currentSwipe = ESwipeType.Null;
	private bool isSwiping = false;
	public Collider _coll;

	public float _minDistance;

    private Vector3 previousPosition = Vector3.zero;
	private Vector3 firstPosition = Vector3.zero;

    public IEnumerator SwipeTimer() {
		float timer = 0;
		while (timer < 1f) {
			timer += Time.deltaTime;
			yield return null;
		}
	}


	// Update is called once per frame
	void Update () {

#if UNITY_ANDROID || UNITY_IOS

		if (Input.touchCount > 0) {

			if (isSwiping == false) {
				isSwiping = true;
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				if (Physics.Raycast(ray, out hit)) {
					if (hit.collider != null) {
						_coll = hit.collider;
					}
				}
			}
			if (Input.GetTouch(0).deltaPosition.y > 0) {
				if (co == null && currentSwipe != ESwipeType.UpSwipe) {
					StartCoroutine(SwipeTimer());
				}
				if (currentSwipe == ESwipeType.DownSwipe || currentSwipe == ESwipeType.Null) {
					currentSwipe = ESwipeType.UpSwipe;
					Events.Instance.Raise(new OnSwipeEvent() { _collider = _coll, numberSwipe = ++stack, _swipeType = ESwipeType.UpSwipe });
				}
			}
			if (Input.GetTouch(0).deltaPosition.y < 0) {
				if (co == null && currentSwipe != ESwipeType.DownSwipe) {
					StartCoroutine(SwipeTimer());
				}
				if (currentSwipe == ESwipeType.UpSwipe || currentSwipe == ESwipeType.Null) {
					currentSwipe = ESwipeType.DownSwipe;
					Events.Instance.Raise(new OnSwipeEvent() { _collider = _coll, numberSwipe = ++stack, _swipeType = ESwipeType.DownSwipe });
				}
			}
		} else {
			isSwiping = false;
			StopCoroutine(SwipeTimer());
			if (co != null)
				co = null;
			currentSwipe = ESwipeType.Null;
			stack = 0;
		}
#else

        if (Input.GetMouseButton(0))
        {
            if (isSwiping == false)
            {
                isSwiping = true;

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        _coll = hit.collider;
                    }
                }
            }

			if (firstPosition == Vector3.zero)
				firstPosition = Input.mousePosition;

			if (firstPosition != Vector3.zero && Input.mousePosition.y - firstPosition.y > _minDistance)
            {
				if (co == null && currentSwipe != ESwipeType.UpSwipe)
                {
                    StartCoroutine(SwipeTimer());
                }
                if (currentSwipe == ESwipeType.DownSwipe || currentSwipe == ESwipeType.Null)
                {
					firstPosition = Vector3.zero;
					currentSwipe = ESwipeType.UpSwipe;
                    Events.Instance.Raise(new OnSwipeEvent() { _collider = _coll, numberSwipe = ++stack, _swipeType = ESwipeType.UpSwipe });
                }
            }
            if (firstPosition != Vector3.zero && firstPosition.y - Input.mousePosition.y > _minDistance)
            {
                if (co == null && currentSwipe != ESwipeType.DownSwipe)
                {
                    StartCoroutine(SwipeTimer());
                }
                if (currentSwipe == ESwipeType.UpSwipe || currentSwipe == ESwipeType.Null)
                {
					firstPosition = Vector3.zero;
					currentSwipe = ESwipeType.DownSwipe;
                    Events.Instance.Raise(new OnSwipeEvent() { _collider = _coll, numberSwipe = ++stack, _swipeType = ESwipeType.DownSwipe });
                }
            }
            previousPosition = Input.mousePosition;
        }
        else
        {
			firstPosition = Vector3.zero;
            isSwiping = false;
            StopCoroutine(SwipeTimer());
            if (co != null)
                co = null;
            currentSwipe = ESwipeType.Null;
            stack = 0;
            previousPosition = Vector3.zero;
        }
#endif
    }
}
