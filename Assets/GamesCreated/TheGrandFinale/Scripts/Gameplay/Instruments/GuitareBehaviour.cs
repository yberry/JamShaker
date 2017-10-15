using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitareBehaviour : InstrumentBehaviour {

    public Animator animator;

    private void OnEnable() {
        animator.SetBool("Activate", true);
        Activate();
		Events.Instance.AddListener<OnSwipeEvent>(HandleSwipeEvent);
	}

	private void OnDisable() {
        animator.SetBool("Activate", false);
        Deactivate();
		Events.Instance.RemoveListener<OnSwipeEvent>(HandleSwipeEvent);
	}

	public void HandleSwipeEvent(OnSwipeEvent e) {
		//si le collider est bon et le type de swipe est bon par rapport a instrumentparts[currentinstrumentpart]
		//		if (e._collider == InstrumentParts[currentPartID] && e._swipeType == InstrumentParts[currentPartID]._partRequireGesture) {
//		Debug.Log(e._swipeType + " => " + InstrumentParts[currentPartID]._SwipeStack[0]);

		if (InstrumentParts[currentPartID]._SwipeStackTmp.Count > 0 && InstrumentParts[currentPartID]._SwipeStackTmp[0] == e._swipeType) {
			InstrumentParts[currentPartID]._SwipeStackTmp.RemoveAt(0);
			InstrumentParts[currentPartID].ValidateArrow();
		}


		if (InstrumentParts[currentPartID]._SwipeStackTmp.Count == 0) {
			StopCoroutine(ActiveTimer());
			InstrumentParts[currentPartID]._SwipeStackTmp = new List<ESwipeType>(InstrumentParts[currentPartID]._SwipeStack);
            Next();
            DisplayScore.Instance.AddScore(_beginTime, EInstrument.GUITAR);
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
