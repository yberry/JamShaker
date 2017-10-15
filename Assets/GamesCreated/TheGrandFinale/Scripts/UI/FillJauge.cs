using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillJauge : MonoBehaviour {

    private static FillJauge instance = null;
    public static FillJauge Instance
    {
        get
        {
            return instance;
        }
    }

    public Image fillImage;
    public float fillSpeed = 2f;

    private float scoreMax = 1f;
    private float currentScore = 0f;

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

    public void SetMax(float max)
    {
        scoreMax = max;
    }

    public void SetCurrentScore(float score)
    {
        currentScore = score;
    }

    void Update()
    {
        fillImage.fillAmount = Mathf.MoveTowards(fillImage.fillAmount, currentScore / scoreMax, fillSpeed * Time.deltaTime);
    }
}
