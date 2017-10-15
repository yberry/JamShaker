using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreModifierEvent : GameEvent
{
    public int scoreAdded;
}

public class Gameplay : MonoSingleton<Gameplay>
{
    public float timeBetweenEachPattern = 4;

    private bool isPlaying = false;
    [HideInInspector]
    public float totalTime = 0;
    private bool goalReached = false;
	public GameObject BeginButton;
	public float endGameTime= 60;

    public int actualLevel = 1;

    public int maxScore;
    private int actualScore;

    private int step = 0;

    List<int> usedStep = new List<int>();

    public void StartGame()
    {
        FillJauge.Instance.SetMax(endGameTime);
        BeginButton.SetActive(false);
        usedStep = Enumerable.Range(0, 3).ToList();
        goalReached = false;
        isPlaying = true;
        StartCoroutine(GameLoop());
        DisplayScore.Instance.Reset();
    }

    public void ReachedGoal()
    {
        isPlaying = false;
        goalReached = true;

		StopCoroutine(GameLoop());
        AudioDatabase.Instance.Back.Stop();
        AudioDatabase.Instance.Instrument.Stop();
        goalReached = false;
        isPlaying = false;

        FillJauge.Instance.SetCurrentScore(0);
        BeginButton.SetActive(true);
    }

    IEnumerator GameLoop()
    {
        AudioDatabase.Instance.Back.Play();
        PrefabsInstrumentDatabase.Instance.DeactivateAllInstrument();

        float tmpTime = 0;
		totalTime = 0;
        while (!goalReached)
        {
            if (tmpTime >= TimeManager.Instance.getActualSeconds())
            {
                PrefabsInstrumentDatabase.Instance.DeactivateAllInstrument();
                LaunchPattern();
                tmpTime = 0;
                step++;
                if (step == 4)
                {
                    usedStep = Enumerable.Range(0, 3).ToList();
                    step = 0;
                }
            }

            if (totalTime >= endGameTime)
            {
                ReachedGoal();
                break;
            }
            FillJauge.Instance.SetCurrentScore(Gameplay.Instance.totalTime);
            tmpTime += Time.deltaTime;
            totalTime += Time.deltaTime;
            yield return null;
        }
    }

    private void LaunchPattern()
    {
        int patternID;
        if (step < PatternDatabase.Instance.sPatterns.Count - 1)
        {
            int random = Random.Range(0, usedStep.Count);
            patternID = usedStep[random];
            usedStep.Remove(patternID);
        }
        else
            patternID = 3;

        Pattern pat = new Pattern();
        pat.typeInstrument = PatternDatabase.Instance.sPatterns[patternID].typeInstrument;
        if (pat.typeInstrument != EInstrument.DRUM)
            pat.PlayRegionSound();

        Debug.Log("PATTERN ID : " + patternID);
        TargetCamDatabase.Instance.FocusTarget(PatternDatabase.Instance.sPatterns[patternID].typeInstrument);
        PrefabsInstrumentDatabase.Instance.ActivateInstrument(PatternDatabase.Instance.sPatterns[patternID].typeInstrument);    
    }

}
