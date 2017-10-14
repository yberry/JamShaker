using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESwipeType {
	HorizontalSwipe,
	VerticlaSwipe,
	RoundSwipe
}

public class OnSwipeEvent : GameEvent {
	public Collider2D _coll = null;
	public ESwipeType _swipeType;
	public EInstrument _instrument;
}

public class GestureEventManager : MonoBehaviour {

	private void Update () {
		//Handle gestures

		/*
		 * 
		 * 
		 */

		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
			if (hit.collider != null) {
//				Events.Instance.Raise(new OnSwipeEvent() { _swipeType = ESwipeType.VerticlaSwipe, _coll = hit.collider, _instrument = hit.collider.gameObject.GetComponent<Instrument>() });
			}
		}
	}
}
