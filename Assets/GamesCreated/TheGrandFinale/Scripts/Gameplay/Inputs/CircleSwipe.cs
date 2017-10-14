using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSwipe : MonoBehaviour {

	public int stack = 0;
	private int squareCount = 0;
	public Coroutine co;
	public ESwipeType currentSwipe = ESwipeType.Null;
	private bool isSwiping = false;
	public Collider _coll;

	public IEnumerator SwipeTimer() {
		float timer = 0;
		while (timer < 1f) {
			timer += Time.deltaTime;
			yield return null;
		}
		currentSwipe = ESwipeType.Null;
		squareCount = 0;
	}

	private void Update() {
		//Handle gestures

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
			if (Input.GetTouch(0).deltaPosition.x > 0) {
				if (currentSwipe == ESwipeType.LeftSwipe || currentSwipe == ESwipeType.Null) {
					squareCount++;
				} else if (currentSwipe != ESwipeType.RightSwipe) {
					squareCount = 0;
				}
				currentSwipe = ESwipeType.RightSwipe;
			} else if (Input.GetTouch(0).deltaPosition.x < 0) {
				if (currentSwipe == ESwipeType.RightSwipe || currentSwipe == ESwipeType.Null) {
					squareCount++;
				} else if (currentSwipe != ESwipeType.LeftSwipe) {
					squareCount = 0;
				}

				currentSwipe = ESwipeType.LeftSwipe;

			} else if (Input.GetTouch(0).deltaPosition.y > 0) {
				if (currentSwipe != ESwipeType.DownSwipe || currentSwipe == ESwipeType.Null) {
					squareCount++;
				} else if (currentSwipe != ESwipeType.UpSwipe) {
					squareCount = 0;
				}

				currentSwipe = ESwipeType.UpSwipe;

			} else if (Input.GetTouch(0).deltaPosition.y < 0) {
				if (currentSwipe == ESwipeType.UpSwipe || currentSwipe == ESwipeType.Null) {
					squareCount++;
				} else if (currentSwipe != ESwipeType.DownSwipe) {
					squareCount = 0;
				}

				currentSwipe = ESwipeType.DownSwipe;
			}
			if (squareCount == 4) {
				squareCount = 0;

				//				Events.Instance.Raise(new ONSQUAREEVENT() { });
				//				-> stack/4
			}
		} else {
			isSwiping = false;
			StopCoroutine(SwipeTimer());
			if (co != null)
				co = null;
			currentSwipe = ESwipeType.Null;
			stack = 0;
		}
	}

}
