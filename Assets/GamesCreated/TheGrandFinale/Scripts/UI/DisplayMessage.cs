using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MiniGame
{
    GorgeProfonde,
    Guitare,
    Trombone,
    Batterie
}

public class DisplayMessage : MonoBehaviour {

    private static DisplayMessage instance = null;
    public static DisplayMessage Instance
    {
        get
        {
            return instance;
        }
    }

    public Text displayMessage;
    public float displayDuration = 1f;
    public float freqScale = 1f;
    public float amplitude = 2f;
    public float freqBlink = 10f;

    Dictionary<MiniGame, string[]> messages = new Dictionary<MiniGame, string[]>()
    {
        { MiniGame.GorgeProfonde, new string[] { "\"SINGER\"", "DIVA", "AWESOME", "GENIOUS" } },
        { MiniGame.Guitare, new string[] { "\"GUITARIST\"", "COOL KID", "ROCKSTAR", "HENDRIX" } },
        { MiniGame.Trombone, new string[] { "BLOW DRIER", "GOODENOUGH", "BLOW MASTER", "EPIC TROMBONE GUY" } },
        { MiniGame.Batterie, new string[] { "\"DRUMMER3\"", "\"AWESOMAN\"", "RITHMAHOLIC", "DRUM KING" } }
    };

    List<Color> colors = new List<Color>()
    {
        Color.red,
        Color.yellow,
        Color.green,
        Color.blue
    };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Display(MiniGame miniGame, int state)
    {
        string message = messages[miniGame][state - 1];
        displayMessage.text = message;

        StopAllCoroutines();
        StartCoroutine(Blink(state));
    }

    IEnumerator Blink(int state)
    {
        float time = 0f;
        Color color = colors[state - 1];
        displayMessage.transform.localScale = Vector3.one;
        displayMessage.color = color;

        while (time <= displayDuration)
        {
            switch (state)
            {
                case 3:
                    displayMessage.transform.localScale = Mathf.Lerp(1f, amplitude, (Mathf.Sin(freqScale * time) + 1f) * 0.5f) * Vector3.one;
                    break;

                case 4:
                    displayMessage.color = Color.Lerp(color, Color.white, (Mathf.Sin(freqBlink * time) + 1f) * 0.5f);
                    break;
            }

            time += Time.deltaTime;
            yield return null;
        }

        displayMessage.text = "";
    }
}
