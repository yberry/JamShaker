﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroatBehaviour : InstrumentBehaviour {

	private void OnEnable() {
		Activate();
		Events.Instance.AddListener<OnSwipeEvent>(HandleSwipeEvent);
	}

	private void OnDisable() {
		Deactivate();
		Events.Instance.RemoveListener<OnSwipeEvent>(HandleSwipeEvent);
	}

	public void HandleSwipeEvent(OnSwipeEvent e) {
		//si le collider est bon et le type de swipe est bon par rapport a instrumentparts[currentinstrumentpart]
		if (e._collider == InstrumentParts[currentPartID] && e._swipeType == InstrumentParts[currentPartID]._partRequireGesture) {
			StopCoroutine(ActiveTimer());
			//validate gesture || up score
		}
	}

	public override void Activate() {
		base.Activate();
		_beginTime = 0;
	}

	public override void Deactivate() {
		base.Deactivate();
		_beginTime = 0;
	}

	IEnumerator ActiveTimer() {
		while (_beginTime <= 1.5f) {
			_beginTime += Time.deltaTime;
			yield return null;
		}
	}

}
