using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    public int score = 0;

    public TMP_Text score_text;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void AddScore(int points)
    {
        score += points;
        score_text.text = score.ToString();
    }
}
