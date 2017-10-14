using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gameplay : MonoSingleton<Gameplay>
{
    public float timeBetweenEachPattern = 4;

    private bool isPlaying = false;
    private float totalTime = 0;
    private bool goalReached = false;

    private int step = 0;

    List<int> usedStep = new List<int>();

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StartGame();
    }

    public void StartGame()
    {
        usedStep = Enumerable.Range(0, 3).ToList();
        goalReached = false;
        isPlaying = true;
        StartCoroutine(GameLoop());
    }

    public void ReachedGoal()
    {
        isPlaying = false;
        goalReached = true;
    }

    IEnumerator GameLoop()
    {
        AudioDatabase.Instance.Back.Play();
        PrefabsInstrumentDatabase.Instance.DeactivateAllInstrument();
        Debug.Log("STarting");


        float tmpTime = 0;
        while (!goalReached)
        {
            if (tmpTime >= timeBetweenEachPattern)
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
            tmpTime += Time.deltaTime;
            totalTime += Time.deltaTime;
            yield return null;

            AudioDatabase.Instance.Back.Stop();
            AudioDatabase.Instance.Instrument.Stop();
            goalReached = false;
            isPlaying = false;
        }

        EndGame();

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

        Debug.Log("PATTERN ID : " + patternID);
        PrefabsInstrumentDatabase.Instance.ActivateInstrument(PatternDatabase.Instance.sPatterns[patternID].typeInstrument);    
    }

    private void EndGame()
    {

    }

}
