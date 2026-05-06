using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameEntry : MonoBehaviour
{
    public static NameEntry instance;

    public GameObject nameEntryCanvas;
    public TextMeshPro scoreDisplay;
    public TextMeshPro slot1Text;
    public TextMeshPro slot2Text;
    public TextMeshPro slot3Text;

    private char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private int[] selectedIndex = { 0, 0, 0 };
    private int currentSlot = 0;
    private bool isActive = false;
    private float scrollCooldown = 0f;
    private float cooldownTime = 0.2f;

    void Start()
    {
        instance = this;
        nameEntryCanvas.SetActive(false);
    }

    void Update()
    {
        if (!isActive) return;

        scrollCooldown -= Time.deltaTime;

        // joystick movement
        float thumbstickY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y 
                          + OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;

        float thumbstickX = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x 
                          + OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

        if (scrollCooldown <= 0f)
        {
            if (thumbstickY < -0.5f)
            {
                selectedIndex[currentSlot] = (selectedIndex[currentSlot] + 1) % letters.Length;
                UpdateDisplay();
                scrollCooldown = cooldownTime;
            }
            else if (thumbstickY > 0.5f)
            {
                selectedIndex[currentSlot] = (selectedIndex[currentSlot] - 1 + letters.Length) % letters.Length;
                UpdateDisplay();
                scrollCooldown = cooldownTime;
            }
        
            // move slot right
            if (thumbstickX > .75f)
            {
                if (currentSlot < 2)
                {
                    currentSlot++;
                    UpdateDisplay();
                    scrollCooldown = cooldownTime;
                }
                else if (currentSlot == 2)
                {
                    UpdateDisplay();
                    scrollCooldown = cooldownTime;
                }
            }
            // move slot left
            else if (thumbstickX < -.75f)
            {
                if (currentSlot > 0)
                {
                    currentSlot--;
                    UpdateDisplay();
                    scrollCooldown = cooldownTime;
                }
            }
        }

        // A button to confirm current slot / submit
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            SubmitScore();
        }
    }

    public void Show()
    {
        isActive = true;
        nameEntryCanvas.SetActive(true);

        // spawn in front of player
        Transform cam = Camera.main.transform;
        nameEntryCanvas.transform.position = cam.position + cam.forward * 5f;
        nameEntryCanvas.transform.rotation = Quaternion.LookRotation(cam.forward);

        scoreDisplay.text = "You scored " + ScoreManager.instance.score + " points!";
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        slot1Text.text = currentSlot == 0 ? "[" + letters[selectedIndex[0]] + "]" : letters[selectedIndex[0]].ToString();
        slot2Text.text = currentSlot == 1 ? "[" + letters[selectedIndex[1]] + "]" : letters[selectedIndex[1]].ToString();
        slot3Text.text = currentSlot == 2 ? "[" + letters[selectedIndex[2]] + "]" : letters[selectedIndex[2]].ToString();
    }

    void SubmitScore()
    {
        string name = "" + letters[selectedIndex[0]] + letters[selectedIndex[1]] + letters[selectedIndex[2]];
        Leaderboard.instance.AddScore(name, ScoreManager.instance.score);
        nameEntryCanvas.SetActive(false);
        isActive = false;
        Leaderboard.instance.Show();
    }
}