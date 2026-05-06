using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;

    public GameObject leaderboardCanvas;
    public TextMeshPro leaderboardText;
    public GameObject restartButton;

    private List<string> names = new List<string>();
    private List<int> scores = new List<int>();
    private int maxEntries = 10;

    void Start()
    {
        instance = this;
        leaderboardCanvas.SetActive(false);
        LoadScores();
    }

    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Three) && OVRInput.Get(OVRInput.Button.Four))
        {
            ClearLeaderboard();
        }
    }

    public void AddScore(string name, int score)
    {
        names.Add(name);
        scores.Add(score);
        SortScores();
        SaveScores();
    }

    void SortScores()
    {
        // bubble sort high to low
        for (int i = 0; i < scores.Count - 1; i++)
        {
            for (int j = 0; j < scores.Count - i - 1; j++)
            {
                if (scores[j] < scores[j + 1])
                {
                    int tempScore = scores[j];
                    scores[j] = scores[j + 1];
                    scores[j + 1] = tempScore;

                    string tempName = names[j];
                    names[j] = names[j + 1];
                    names[j + 1] = tempName;
                }
            }
        }

        // trim to max entries
        while (scores.Count > maxEntries)
        {
            scores.RemoveAt(scores.Count - 1);
            names.RemoveAt(names.Count - 1);
        }
    }

    public void Show()
    {
        leaderboardCanvas.SetActive(true);

        // spawn in front of player
        Transform cam = Camera.main.transform;
        leaderboardCanvas.transform.position = cam.position + cam.forward * 10f;
        leaderboardCanvas.transform.rotation = Quaternion.LookRotation(cam.forward);

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        string display = "--- TOP SCORES ---\n\n";
        for (int i = 0; i < scores.Count; i++)
        {
            display += (i + 1) + ".\t" + names[i] + "  " + scores[i] + "\n";
        }
        leaderboardText.text = display;
    }

    public bool Qualifies(int score)
    {
        if (scores.Count < maxEntries)
            return true;
        return score > scores[scores.Count-1];
    }

    void SaveScores()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetString("Name" + i, names[i]);
            PlayerPrefs.SetInt("Score" + i, scores[i]);
        }
        PlayerPrefs.SetInt("ScoreCount", scores.Count);
        PlayerPrefs.Save();
    }

    void LoadScores()
    {
        int count = PlayerPrefs.GetInt("ScoreCount", 0);
        for (int i = 0; i < count; i++)
        {
            names.Add(PlayerPrefs.GetString("Name" + i, "???"));
            scores.Add(PlayerPrefs.GetInt("Score" + i, 0));
        }
    }

    public void ClearLeaderboard()
    {
        names.Clear();
        scores.Clear();
        PlayerPrefs.DeleteKey("ScoresCount");
        for (int i = 0; i < maxEntries; i++)
        {
            PlayerPrefs.DeleteKey("Name" + i);
            PlayerPrefs.DeleteKey("Score" + i);
        }
        PlayerPrefs.Save();
    }
}