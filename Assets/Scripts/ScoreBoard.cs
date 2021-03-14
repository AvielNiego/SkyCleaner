using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private int score;

    private TMP_Text textUi;

    private void Start()
    {
        textUi = GetComponent<TMP_Text>();
        textUi.text = score.ToString();
    }

    public void IncreaseScore(int points = 10)
    {
        score += points;
        textUi.text = score.ToString();
        Debug.Log($"New score {score}");
    }
}
