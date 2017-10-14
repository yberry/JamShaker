﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSwipe : MonoBehaviour {

	public int stack = 0;
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
	}

	void Update() {
		if (Input.touchCount > 0) {

			if (Input.GetTouch(0).deltaPosition.x > 0) {
				if (co == null && currentSwipe != ESwipeType.RightSwipe) {
					StartCoroutine(SwipeTimer());
				}
				if (currentSwipe == ESwipeType.LeftSwipe || currentSwipe == ESwipeType.Null) {
					currentSwipe = ESwipeType.RightSwipe;
					Events.Instance.Raise(new OnSwipeEvent() { _collider = _coll, numberSwipe = ++stack, _swipeType = ESwipeType.RightSwipe });
				}
			}
			if (Input.GetTouch(0).deltaPosition.x < 0) {
				if (co == null && currentSwipe != ESwipeType.LeftSwipe) {
					StartCoroutine(SwipeTimer());
				}
				if (currentSwipe == ESwipeType.RightSwipe || currentSwipe == ESwipeType.Null) {
					currentSwipe = ESwipeType.LeftSwipe;
					Events.Instance.Raise(new OnSwipeEvent() { _collider = _coll, numberSwipe = ++stack, _swipeType = ESwipeType.LeftSwipe });
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
	}
}