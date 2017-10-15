using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumBehaviour : InstrumentBehaviour
{
    List<AudioSource> sources = new List<AudioSource>();

    private bool firstPhase = true;

    private void OnEnable()
    {
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
                PlaySound();
                DisplayScore.Instance.AddScore(0.95f, 1);
                //IMPLEMENT SCORING
            }

            if (!firstPhase && e._collider == InstrumentParts[1]._instrumentPartCollider)
            {
                DisplayScore.Instance.AddScore(0.95f, 1);
                PlayFinalSound();
                //IMPLEMENT SCORING
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
}
