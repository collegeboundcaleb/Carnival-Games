using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // controller vars
    public GameObject rightController;

    // projectile gameobject vars
    public GameObject projectileType;
    public float projectileForceAmount;
    private GameObject projectile;
    private GameObject oldProjectile;

    private bool holdingProjectile = false;


    // spawns projectile at position of right controller
    private void SpawnProjectile()
    {
        projectile = Instantiate(projectileType, rightController.transform.position, rightController.transform.rotation);
        holdingProjectile = true;
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

    // Start is called before the first frame update
    void Start()
    {
        if (rightController == null)
        {
            rightController = GameObject.Find("RightControllerAnchor");
        }
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
        }

        // if holding projetile, update position
        if (holdingProjectile)
        {
            projectile.transform.SetPositionAndRotation(rightController.transform.position, rightController.transform.rotation);
        }

    }
}
