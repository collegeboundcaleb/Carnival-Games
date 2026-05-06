using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;
    public float timeRemaining = 60f;
    public TextMeshPro timerText;
    private bool timerRunning;

// Start is called before the first frame update
    void Start()
    {
        instance = this;
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerRunning = false;
                TimerEnd();
            }

            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.FloorToInt(timeRemaining);
        timerText.text = seconds.ToString();
    }

    void TimerEnd()
    {
        FindObjectOfType<ItemSpawner>().canSpawn = false;
        if (Leaderboard.instance.Qualifies(ScoreManager.instance.score))
        {
                    NameEntry.instance.Show();

        }
        else
        {
            Leaderboard.instance.Show();
        }
    }
}
