using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Meta.XR.MRUtilityKit.FindSpawnPositions;

public class LaughSpawner : MonoBehaviour
{
    public float laughInterval;

    private AudioSource audioSource;
    private GameObject[] taggedObjects;
    private Vector3 spawnLocation;

    private float timer;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= laughInterval)
        {
            taggedObjects = GameObject.FindGameObjectsWithTag("Target");
            if (taggedObjects.Length != 0)
            {
                int randomIndex = Random.Range(0, taggedObjects.Length);
                transform.position = taggedObjects[randomIndex].transform.position;
                audioSource.Play();
                timer = 0; // Reset timer
            }
            
        }
    }
}
