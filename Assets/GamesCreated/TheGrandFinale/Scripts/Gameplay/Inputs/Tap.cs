using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour
{
    public Collider _coll;

    bool isTouching = false;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.touchCount);
        if (Input.touchCount > 0 && isTouching == false)
        {
            isTouching = true;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    _coll = hit.collider;
                }
            }
            
            Events.Instance.Raise(new OnSwipeEvent() { _collider = _coll, numberSwipe = 0, _swipeType = ESwipeType.Tap });
        }
        else if (Input.touchCount <= 0)
        {
            isTouching = false;
        }
    }
}
