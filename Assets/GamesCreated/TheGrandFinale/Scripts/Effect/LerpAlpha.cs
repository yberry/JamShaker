using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpAlpha : MonoBehaviour {

    SpriteRenderer sprite;
    public Color32 basicColor = new Color32(255, 255, 255, 255);
    public Color32 endcolor = new Color32(255, 255, 255, 0);
    public float time = 0.25f;

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
        sprite.color = Color.Lerp(basicColor, endcolor , Mathf.PingPong(Time.time, time));
    }
}
