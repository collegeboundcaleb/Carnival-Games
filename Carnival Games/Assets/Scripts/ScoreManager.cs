using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TextMeshPro scoreText;

// Start is called before the first frame update
    void Start()
    {
        instance = this;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
