using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour
{

    private static DisplayMessage instance = null;
    public static DisplayMessage Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("Messages")]
    public Text gorgeProfondeMessage;
    public Text guitareMessage;
    public Text tromboneMessage;
    public Text batterieMessage;
    public Text drummerFeedback;

    [Header("Display variables")]
    public float displayDuration = 1f;
    public float freqScale = 1f;
    public float amplitude = 2f;
    public float freqBlink = 10f;

    Dictionary<EInstrument, string[]> messages = new Dictionary<EInstrument, string[]>()
    {
        { EInstrument.VOICE, new string[] { "\"SINGER\"", "DIVA", "AWESOME", "GENIOUS" } },
        { EInstrument.GUITAR, new string[] { "\"GUITARIST\"", "COOL KID", "ROCKSTAR", "HENDRIX" } },
        { EInstrument.TROMBONE, new string[] { "BLOW DRIER", "GOODENOUGH", "BLOW MASTER", "EPIC TROMBONE GUY" } },
        { EInstrument.DRUM, new string[] { "\"DRUMMER\"", "\"AWESOMAN\"", "RITHMAHOLIC", "DRUM KING" } }
    };

    Dictionary<EInstrument, Text> texts;

    Dictionary<EInstrument, Coroutine> coroutines;

    List<Color> colors = new List<Color>()
    {
        Color.red,
        Color.yellow,
        Color.green,
        Color.blue
    };

    Coroutine drummerCoroutine = null;

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

        texts = new Dictionary<EInstrument, Text>()
        {
            { EInstrument.VOICE, gorgeProfondeMessage },
            { EInstrument.GUITAR, guitareMessage },
            { EInstrument.TROMBONE, tromboneMessage },
            { EInstrument.DRUM, batterieMessage }
        };

        coroutines = new Dictionary<EInstrument, Coroutine>()
        {
            { EInstrument.VOICE, null },
            { EInstrument.GUITAR, null },
            { EInstrument.TROMBONE, null },
            { EInstrument.DRUM, null }
        };
    }

    public void Display(EInstrument miniGame, int state)
    {
        if (state <= 5)
            state = 1;
        else if (state <= 10)
            state = 2;
        else if (state <= 19)
            state = 3;
        else
            state = 4;

        string message = messages[miniGame][state - 1];
        Text displayMessage = texts[miniGame];
        displayMessage.text = message;


        if (coroutines[miniGame] != null)
        {
            StopCoroutine(coroutines[miniGame]);
        }
        coroutines[miniGame] = StartCoroutine(Blink(displayMessage, state));

        if (miniGame == EInstrument.DRUM)
        {
            drummerCoroutine = StartCoroutine(Drummer(state));
        }
    }

    public void EndDrummer(bool success)
    {
        if (drummerCoroutine != null)
            StopCoroutine(drummerCoroutine);
        drummerCoroutine = StartCoroutine(DrummerResult(success));

    }

    IEnumerator Blink(Text displayMessage, int state)
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

    IEnumerator Drummer(int state)
    {
        float time = 0f;
        Color color = colors[state - 1];
        drummerFeedback.text = "HIT EVERYTHING";
        drummerFeedback.color = color;

        while (true)
        {
            drummerFeedback.color = Color.Lerp(color, Color.white, (Mathf.Sin(freqBlink * time) + 1f) * 0.5f);

            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator DrummerResult(bool success)
    {
        float time = 0f;
        Color color = success ? Color.green : Color.red;
        drummerFeedback.color = color;
        drummerFeedback.text = success ? "PERFECT" : "BOOOOOOOOH";

        while (time <= displayDuration)
        {
            if (success)
            {
                drummerFeedback.color = Color.Lerp(color, Color.white, (Mathf.Sin(freqBlink * time) + 1f) * 0.5f);
            }

            time += Time.deltaTime;
            yield return null;
        }

        drummerFeedback.text = "";
    }
}
