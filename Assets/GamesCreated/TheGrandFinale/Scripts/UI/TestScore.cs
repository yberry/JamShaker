using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TestScore : MonoBehaviour {

    public float Temps { get; set; }

    public Dropdown drop;

    private int mult = 1;
    private int index = 0;

	// Use this for initialization
	void Start () {
        Temps = 0f;

        drop.AddOptions(new List<string>() { "1", "2", "3", "5" });
        drop.onValueChanged.AddListener(i => index = i);
        mult = Convert.ToInt32(drop.options[index].text);
    }

    public void Send()
    {
        DisplayScore.Instance.AddScore(Temps, mult);
    }

    public void SendMessage()
    {
        DisplayMessage.Instance.Display(MiniGame.Batterie, index + 1);
    }

    public void EndDrummer()
    {
        DisplayMessage.Instance.EndDrummer(false);
    }
}
