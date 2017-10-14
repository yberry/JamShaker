using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public float timeBetweenEachPattern = 4;

    private bool isPlaying = false;
    private float totalTime = 0;
    private bool goalReached = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StartGame();
    }

    public void StartGame()
    {
        goalReached = false;
        isPlaying = true;
        StartCoroutine(GameLoop());
    }


    IEnumerator GameLoop()
    {
        AudioDatabase.Instance.Back.Play();

        float tmpTime = 0;
        while (goalReached)
        {
            if (tmpTime >= timeBetweenEachPattern)
            {
                LaunchPattern();
                tmpTime = 0;
            }
            tmpTime += Time.deltaTime;
            totalTime += Time.deltaTime;
            yield return null;
        }
        AudioDatabase.Instance.Back.Stop();
        AudioDatabase.Instance.Instrument.Stop();
        goalReached = false;
        isPlaying = false;
    }

    public void LaunchPattern()
    {
        int patternID = Random.Range(0, PatternDatabase.Instance.sPatterns.Count);

        PrefabsInstrumentDatabase.Instance.ActivateInstrument(PatternDatabase.Instance.sPatterns[patternID].instrumentType);
        
    }

}
