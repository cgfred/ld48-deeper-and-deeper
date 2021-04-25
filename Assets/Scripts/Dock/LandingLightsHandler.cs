using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingLightsHandler : MonoBehaviour
{
    int currentLightIndex = 1;

    Transform[] landingLightsTransforms;

    void Awake()
    {
        landingLightsTransforms = GetComponentsInChildren<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PeriodicUpdates());
    }

    void EnableAllLandingLights()
    {
        for (int i = 1; i < landingLightsTransforms.Length; i++)
        {
            landingLightsTransforms[i].gameObject.SetActive(true);
        }
    }

    IEnumerator PeriodicUpdates()
    {
        while (true)
        {
            EnableAllLandingLights();

            landingLightsTransforms[currentLightIndex].gameObject.SetActive(false);

            //Go to next light
            currentLightIndex++;

            if (currentLightIndex > landingLightsTransforms.Length - 1)
                currentLightIndex = 1;

            yield return new WaitForSeconds(0.2f);
        }

    }
}
