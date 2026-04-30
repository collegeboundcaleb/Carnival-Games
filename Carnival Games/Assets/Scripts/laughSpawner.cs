using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughSpawner : MonoBehaviour
{
    public GameObject particleFxType;
    public float laughInterval;

    private AudioSource audioSource;
    private GameObject[] taggedObjects;
    private GameObject particleFx;

    private float timer;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particleFx = Instantiate(particleFxType, transform.position, transform.rotation);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= laughInterval)
        {
            taggedObjects = GameObject.FindGameObjectsWithTag("Target");
            if (taggedObjects.Length != 0)
            {
                particleFx.GetComponent<ParticleSystem>().Stop();
                int randomIndex = Random.Range(0, taggedObjects.Length);
                transform.position = taggedObjects[randomIndex].transform.position;
                particleFx.transform.position = taggedObjects[randomIndex].transform.position;
                particleFx.transform.rotation = taggedObjects[randomIndex].transform.rotation;
                particleFx.GetComponent<ParticleSystem>().Play();
                audioSource.Play();
                timer = 0; // Reset timer
            }
            
        }
    }
}
