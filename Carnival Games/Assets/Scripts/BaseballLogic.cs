using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballLogic : MonoBehaviour
{
    // 1. Add a variable for your sound clip. 
    // You will drag and drop your audio file into this slot in the Unity Inspector.
    public AudioClip pinHitSound;

    void Start()
    {
        Debug.Log("<color=cyan><b>HERE IT IS! ItemSpawner is attached to: </b></color>" + gameObject.name, gameObject);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            // 2. Play the sound at the exact position of the collision
            if (pinHitSound != null)
            {
                AudioSource.PlayClipAtPoint(pinHitSound, transform.position);
            }

            collision.gameObject.GetComponentInChildren<Animator>().SetTrigger("Die");
            foreach (CapsuleCollider col in collision.gameObject.GetComponents<CapsuleCollider>())
            {
                col.enabled = false;
            }
            Destroy(collision.gameObject, 1.06f);
            ScoreManager.instance.AddPoint();
        }
    }
}