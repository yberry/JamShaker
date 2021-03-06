﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrumBehaviour : InstrumentBehaviour
{
    List<AudioSource> sources = new List<AudioSource>();
    public List<GameObject> FX = new List<GameObject>();

    private List<int> idFXActive = new List<int>();
	public GameObject particleSystemPrefab;

    private bool firstPhase = true;
	private bool positiveScore = false;

    private void OnEnable()
    {
        idFXActive = Enumerable.Range(0, FX.Count).ToList();
        positiveScore = false;
        Activate();
        Events.Instance.AddListener<OnSwipeEvent>(HandleSwipeEvent);
        firstPhase = true;
    }

    private void OnDisable()
    {
        Deactivate();
        Events.Instance.RemoveListener<OnSwipeEvent>(HandleSwipeEvent);
    }

    public void Start()
    {
        for (int i = 0; i < AudioDatabase.Instance.clips[3].audioClips.Count; i++)
        {
            sources.Add(gameObject.AddComponent<AudioSource>());
            sources[i].playOnAwake = false;
            sources[i].loop = false;
            sources[i].clip = AudioDatabase.Instance.clips[3].audioClips[i];
        }
    }

    public void HandleSwipeEvent(OnSwipeEvent e)
    {
        if (e._swipeType == ESwipeType.Tap)
        {
            if (firstPhase && e._collider == InstrumentParts[0]._instrumentPartCollider)
            {
				GameObject.Instantiate(particleSystemPrefab, new Vector3(0, 0, -100) + Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                PlaySound();
                DisplayScore.Instance.AddScore(0.95f, EInstrument.DRUM);
				//IMPLEMENT SCORING
				positiveScore = true;

                if (FX.Count > 0)
                {
                    int random = Random.Range(0, FX.Count);
                    StartCoroutine(AddEffect(random));
                }
            }

            if (!firstPhase && e._collider == InstrumentParts[1]._instrumentPartCollider)
            {
				GameObject.Instantiate(particleSystemPrefab, new Vector3(0, 0, -100) + Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
				DisplayScore.Instance.AddScore(0.95f, EInstrument.DRUM);
                PlayFinalSound();
				//IMPLEMENT SCORING
				positiveScore = true;

                if (FX.Count > 0)
                {
                    int random = Random.Range(0, FX.Count);
                    StartCoroutine(AddEffect(random));
                }
            }
        }
    }

    private void PlaySound()
    {
        if (AudioDatabase.Instance.Instrument.isPlaying)
            AudioDatabase.Instance.Instrument.Stop();
        int randomId = Random.Range(0, sources.Count - 2);
       sources[randomId].Play();
    }

    private void PlayFinalSound()
    {
        sources[sources.Count - 1].Play();
    }

    public override void Activate()
    {
        base.Activate();
		_beginTime = 0;
		StartCoroutine(Timer());
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _beginTime = 0;
        StopCoroutine(Timer());

		DisplayMessage.Instance.EndDrummer(positiveScore);

	}

	IEnumerator Timer()
    {
        float time = 0;

        while (time <= TimeManager.Instance.getActualSeconds() * 0.60f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        InstrumentParts[0].gameObject.SetActive(false);
        InstrumentParts[1].gameObject.SetActive(true);

        firstPhase = false;
    }

    IEnumerator AddEffect(int id)
    {

        float time = 0;

        FX[id].SetActive(true);
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            yield return null;
        }
        idFXActive.Add(id);
        FX[id].SetActive(false);
    }
}
