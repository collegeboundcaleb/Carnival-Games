using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject headsetCenter;

    // controller vars
    public GameObject rightController;

    // projectile gameobject vars
    public GameObject projectileType;
    public float projectileForceAmount;
    private GameObject projectile;

    // target vars
    public GameObject targetPrefab;
    public Vector3 targetSpawnOffset;
    private GameObject target;


    private bool holdingProjectile = false;

    private Vector3 getProjectileControllerLocation()
    {
        Vector3 offset = new Vector3(-0.01f, 0f, 0.09f);
        Vector3 newPos = rightController.transform.position + (rightController.transform.rotation * offset);
        return (newPos);
    }


    // spawns projectile at position of right controller
    private void SpawnProjectile()
    {
        projectile = Instantiate(projectileType, getProjectileControllerLocation(), rightController.transform.rotation);
        holdingProjectile = true;
    }

    private void SpawnTarget()
    {
        Vector3 spawnPos = headsetCenter.transform.position + targetSpawnOffset;
        Quaternion spawnRot = Quaternion.LookRotation(spawnPos);
        target = Instantiate(targetPrefab, spawnPos, spawnRot);
    }

    private void FireProjectile()
    {
        // disable projectile launch line
        GameObject projectileLine = projectile.GetNamedChild("Line");
        projectileLine.SetActive(false);

        // launch the projectile
        holdingProjectile = false;
        projectile.GetComponent<Rigidbody>().useGravity = true;
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * projectileForceAmount);
    }

    private void ResetScene()
    {
        Destroy(target);
        SpawnTarget();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (rightController == null)
        {
            rightController = GameObject.Find("RightControllerAnchor");
        }

        // spawn target
        SpawnTarget();
    }

    // Update is called once per frame
    void Update()
    {
        // Update right hand pos var
        //rightControllerPos = rightController.transform.position;

        // right controller trigger 
        //float rightControllerTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);

        // trigger activation
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            if (holdingProjectile)
            {
                FireProjectile();
            }
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }

        // "A" button to spawn projectile
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            if (!holdingProjectile)
            {
                SpawnProjectile();
            }
        } else if (OVRInput.GetUp(OVRInput.Button.Two))
        {
            ResetScene();
        }

        // if holding projetile, update position
        if (holdingProjectile)
        {
            projectile.transform.SetPositionAndRotation(getProjectileControllerLocation(), rightController.transform.rotation);
        }

    }
}
