using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentPart : MonoBehaviour {

	public Collider _instrumentPartCollider;
	public Sprite _arrow;
	public ESwipeType _partRequireGesture;

	private void OnEnable() {
		//Activate image + collider + all
		_instrumentPartCollider.enabled = true;
	}
}
