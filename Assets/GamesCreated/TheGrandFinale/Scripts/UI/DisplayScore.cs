using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {

    private static DisplayScore instance = null;
    public static DisplayScore Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("Displaying Texts")]
    public Text displayScore;
    public Text displayTemp;
    public Text displayMult;
    public Text displayWord;

    [Header("Display durations")]
    public float displayDuration = 1f;
    public float fadingDuration = 1f;
	private int currentMultiplier = 1;
	private int nolimitMultiplier;

    Dictionary<int, Color> multColors = new Dictionary<int, Color>()
    {
        { 1, Color.red },
        { 2, Color.yellow },
        { 3, Color.green },
        { 5, Color.blue }
    };
    Vector2 tmpPosition;
    Vector2 targetPosition;

    int score = 0;
    public int Score
    {
        get
        {
            return score;
        }

        private set
        {
            score = value;
            displayScore.text = "Score : " + score.ToString();
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        tmpPosition = displayTemp.rectTransform.anchoredPosition;
        targetPosition = tmpPosition + new Vector2(0f, -70f);
    }

    public void AddScore(float time, EInstrument instrument)
    {
        int tmp = TimeToScore(time);
		int mult = currentMultiplier;
		if (time <= .5f) {
			mult += 1;
			nolimitMultiplier++;
		} else if (time > 1) {
			mult--;
			nolimitMultiplier++;
		}

		if (mult < 1)
			mult = 1;
		if (nolimitMultiplier < 1)
			nolimitMultiplier = 1;
		if (mult > 5)
			mult = 5;
		currentMultiplier = mult;

        displayTemp.text = "+" + tmp.ToString();
        displayTemp.rectTransform.anchoredPosition = tmpPosition;
        displayMult.text = "X" + mult.ToString();

        Score += mult * tmp;

		DisplayMessage.Instance.Display(instrument, nolimitMultiplier);

        StopAllCoroutines();
		if (mult != 4)
	        StartCoroutine(Fading(mult));
    }

    int TimeToScore(float time)
    {
        if (time < 0.25f)
        {
            displayWord.text = "PERFECT";
            return 1000;
        }
        else if (time < 0.3f)
        {
            displayWord.text = "PERFECT";
            return (int)(1500f - 2000f * time);
        }
        else if (time < 0.5f)
        {
            displayWord.text = "GOOD";
            return (int)(1200f - 1000f * time);
        }
        else if (time < 0.8f)
        {
            displayWord.text = "FASTER";
            return (int)((4600f - 5000f * time) / 3f);
        }
        else if (time <= 1f)
        {
            displayWord.text = "TOO SLOW";
            return (int)(800f - 750f * time);
        }
        else
        {
            displayWord.text = "MISSED!";
            return 0;
        }
    }

    IEnumerator Fading(int mult)
    {
        Color addColor = Color.white;
        Color multColor = multColors[mult];
        displayTemp.color = addColor;
        displayMult.color = multColor;
        displayWord.color = addColor;

        float time = 0f;

        if (displayDuration > 0f)
        {
            while (time <= displayDuration)
            {
                displayTemp.rectTransform.anchoredPosition = Vector2.Lerp(tmpPosition, targetPosition, time / displayDuration);

                time += Time.deltaTime;
                yield return null;
            }
        }

        time = 0f;

        if (fadingDuration > 0f)
        {
            while (time <= fadingDuration)
            {
                float a = 1f - time / displayDuration;
                addColor.a = a;
                multColor.a = a;
                displayTemp.color = addColor;
                displayMult.color = multColor;
                displayWord.color = addColor;

                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
