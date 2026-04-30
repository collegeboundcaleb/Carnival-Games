using System.Collections;
using UnityEngine;

public class RandomBlinker : MonoBehaviour
{
    [Header("Time Settings (Seconds)")]
    public float minVisibleTime = 2f;
    public float maxVisibleTime = 6f;
    
    public float minHiddenTime = 2f;
    public float maxHiddenTime = 5f;

    // We store these so we can turn the visuals and physics on/off
    private Renderer[] allRenderers;
    private Collider[] allColliders;

    void Start()
    {
        // Grab all the visual and physics components on this prefab (and any children)
        allRenderers = GetComponentsInChildren<Renderer>();
        allColliders = GetComponentsInChildren<Collider>();

        // Start the continuous spawn/despawn loop
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        // This loop will run forever as long as the game is playing
        while (true)
        {
            // 1. BECOME VISIBLE
            SetVisibility(true);
            float showTime = Random.Range(minVisibleTime, maxVisibleTime);
            yield return new WaitForSeconds(showTime); // Wait for X seconds

            // 2. BECOME HIDDEN
            SetVisibility(false);
            float hideTime = Random.Range(minHiddenTime, maxHiddenTime);
            yield return new WaitForSeconds(hideTime); // Wait for X seconds
        }
    }

    // A helper function to turn all graphics and colliders on or off at once
    void SetVisibility(bool isVisible)
    {
        foreach (Renderer r in allRenderers)
        {
            r.enabled = isVisible;
        }

        foreach (Collider c in allColliders)
        {
            c.enabled = isVisible;
        }
    }
}