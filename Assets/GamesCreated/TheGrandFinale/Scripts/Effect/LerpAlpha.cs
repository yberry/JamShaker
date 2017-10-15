using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpAlpha : MonoBehaviour {

    SpriteRenderer sprite;
    Color32 basicColor;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        basicColor = sprite.color;
	}

    private void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = basicColor;
    }

    // Update is called once per frame
    void Update () {
        sprite.color = Color.Lerp(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 0), Mathf.PingPong(Time.time, 0.25f));
    }
}
