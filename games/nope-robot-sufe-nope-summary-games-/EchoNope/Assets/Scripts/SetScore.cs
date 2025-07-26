using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;
    public Text ScoreText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = PlayerScore.Score;
        ScoreText.text = score.ToString();
    }
}
