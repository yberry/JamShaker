using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentPart : MonoBehaviour {

	[HideInInspector]
	public Collider _instrumentPartCollider;
//	public ESwipeType _partRequireGesture;
	[SerializeField]
	public List<ESwipeType> _SwipeStack = new List<ESwipeType>();
	[HideInInspector]
	public List<ESwipeType> _SwipeStackTmp = new List<ESwipeType>();
	public List<GameObject> _arrows = new List<GameObject>();
	private int currentArrowIndex = 0;

	private void OnEnable() {
		//Activate image + collider + all
		currentArrowIndex = 0;
		for (int i = 0; i < _arrows.Count; i++)
			_arrows[i].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
		if (_instrumentPartCollider == null)
			_instrumentPartCollider = GetComponent<Collider>();
		_instrumentPartCollider.enabled = true;
		_SwipeStackTmp = new List<ESwipeType>(_SwipeStack);
	}

	public void ValidateArrow() {
		if (_arrows != null && _arrows[currentArrowIndex] != null) {
			_arrows[currentArrowIndex].GetComponent<SpriteRenderer>().material.color = Color.Lerp(Color.white, Color.green, .5f);
			currentArrowIndex++;
		}
	}
}
