using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TextController : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float totalTime = 0f;
    private int score = 0;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "0:0";
    }

    private void FixedUpdate()
    {
        if (score >= 5)
        {
            return;
        }
        else
        {
            totalTime += Time.deltaTime;
            text.text = Mathf.FloorToInt(totalTime / 60) + ":" + (float)System.Math.Round(totalTime % 60, 1);
        }
    }

    public void addScore()
    {
        score++;
        Debug.Log(score);
    }
}