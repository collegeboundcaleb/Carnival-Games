using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLogic : MonoBehaviour
{
    private GameObject headset;

    // Start is called before the first frame update
    void Start()
    {
        headset = GameObject.Find("CenterEyeAnchor");
    }

    // Update is called once per frame
    void Update()
    {
        if (headset != null)
        {
            LookAtPlayer();
        }
        else
        {
            headset = GameObject.Find("CenterEyeAnchor");
        }
    }

    private void LookAtPlayer()
    {
        // Look toward player
        Vector3 direction = headset.transform.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Euler(
                0f,
                targetRotation.eulerAngles.y,
                0f
            );
        }
    }
}
