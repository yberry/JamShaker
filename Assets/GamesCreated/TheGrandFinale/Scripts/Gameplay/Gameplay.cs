using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public float timeBetweenEachPattern = 4;

    private bool isPlaying = false;
    private float totalTime = 0;
    private bool goalReached = false;

    public void StartGame()
    {
        goalReached = false;
        isPlaying = true;
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        float tmpTime = 0;
        while (goalReached)
        {
            if (tmpTime >= 4)
            {
                LaunchPattern();
                tmpTime = 0;
            }
            tmpTime += Time.deltaTime;
            totalTime += Time.deltaTime;
            yield return null;
        }
    }

    public void LaunchPattern()
    {
       
    }

}
