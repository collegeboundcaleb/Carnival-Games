using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
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
