using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentBehaviour : MonoBehaviour {

	public Collider _collider;
	public Sprite _sprite;
	public float score;
	public float _beginTime;

	public List<InstrumentPart> InstrumentParts = new List<InstrumentPart>();
	public int currentPartID = -1;

	public virtual void Activate() {
		int x = Random.Range(0, InstrumentParts.Count);
		for (int i = 0; i < InstrumentParts.Count; i++) {
			if (i == x) {
				InstrumentParts[i].gameObject.SetActive(true);
				currentPartID = i;
			} else
				InstrumentParts[i].gameObject.SetActive(false);
		}
	}


	public virtual void Deactivate() {
		for (int i = 0; i < InstrumentParts.Count; i++) {
			InstrumentParts[i].gameObject.SetActive(false);
		}
	}

}
