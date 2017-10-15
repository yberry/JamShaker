using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentPart : MonoBehaviour {

	[HideInInspector]
	public Collider _instrumentPartCollider;
	public Image _arrow;
//	public ESwipeType _partRequireGesture;
	[SerializeField]
	public List<ESwipeType> _SwipeStack = new List<ESwipeType>();
	[HideInInspector]
	public List<ESwipeType> _SwipeStackTmp = new List<ESwipeType>();

	private void OnEnable() {
		//Activate image + collider + all
		if (_instrumentPartCollider == null)
			_instrumentPartCollider = GetComponent<Collider>();
		_instrumentPartCollider.enabled = true;
		_SwipeStackTmp = new List<ESwipeType>(_SwipeStack);
	}

    public virtual void Activate()
    {
        _arrow.gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        _arrow.gameObject.SetActive(false);
    }
}
