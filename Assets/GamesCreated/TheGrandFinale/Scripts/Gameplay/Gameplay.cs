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
	public GameObject BeginButton;
	public float endGameTime= 60;

    public int actualLevel = 1;

    private int step = 0;

    List<int> usedStep = new List<int>();

	public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
            //StartGame();
    }

    public void StartGame()
    {
		BeginButton.SetActive(false);
        usedStep = Enumerable.Range(0, 3).ToList();
        goalReached = false;
        isPlaying = true;
        StartCoroutine(GameLoop());
    }

    public void ReachedGoal()
    {
        isPlaying = false;
        goalReached = true;

        AudioDatabase.Instance.Back.Stop();
        AudioDatabase.Instance.Instrument.Stop();
        goalReached = false;
        isPlaying = false;

		BeginButton.SetActive(true);
    }

    IEnumerator GameLoop()
    {
        AudioDatabase.Instance.Back.Play();
        PrefabsInstrumentDatabase.Instance.DeactivateAllInstrument();
        Debug.Log("STarting");


        float tmpTime = 0;
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
            tmpTime += Time.deltaTime;
            totalTime += Time.deltaTime;
			if (totalTime > endGameTime) {
				ReachedGoal();
				break;
			}

            yield return null;

            
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

        Pattern pat = new Pattern();
        pat.typeInstrument = PatternDatabase.Instance.sPatterns[patternID].typeInstrument;
        if (pat.typeInstrument != EInstrument.DRUM)
            pat.PlayRegionSound();

        Debug.Log("PATTERN ID : " + patternID);
        TargetCamDatabase.Instance.FocusTarget(PatternDatabase.Instance.sPatterns[patternID].typeInstrument);
        PrefabsInstrumentDatabase.Instance.ActivateInstrument(PatternDatabase.Instance.sPatterns[patternID].typeInstrument);    
    }

    private void EndGame()
    {

    }

}
