﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TestScore : MonoBehaviour {

    public float Temps { get; set; }

    public Dropdown drop;

    private int mult = 1;

	// Use this for initialization
	void Start () {
        Temps = 0f;

        drop.AddOptions(new List<string>() { "1", "2", "3", "5" });
        drop.onValueChanged.AddListener(i => mult = Convert.ToInt32(drop.options[i].text));
	}

    public void Send()
    {
        DisplayScore.Instance.AddScore(Temps, mult);
    }
}