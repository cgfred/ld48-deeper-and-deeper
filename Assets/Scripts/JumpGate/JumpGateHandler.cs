using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGateHandler : MonoBehaviour
{
    public Renderer planeRenderer;

    public Material warp1Material;
    public Material warp2Material;

    bool isWarp1MaterialUsed = true;

    GameObject[] leachShips;

    BoxCollider levelCompletedBoxCollider;

    bool isMissionCompletedHandled = false;

    InGameUIHandler inGameUiHandler;

    //Oter components

    void Awake()
    {
        planeRenderer.gameObject.SetActive(false);

        levelCompletedBoxCollider = GetComponentInChildren<BoxCollider>();

        levelCompletedBoxCollider.enabled = false;

        inGameUiHandler=FindObjectOfType<InGameUIHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        leachShips = GameObject.FindGameObjectsWithTag("LeachShip");

        StartCoroutine(CheckMissionCompletedCO());
    }

    

    //Co routines
    IEnumerator SwapCO()
    {

        while(true)
        {
            yield return new WaitForSeconds(0.3f);

            if (isWarp1MaterialUsed)
                isWarp1MaterialUsed = false;
            else isWarp1MaterialUsed = true;

            if (isWarp1MaterialUsed)
                planeRenderer.material = warp1Material;
            else 
                planeRenderer.material = warp2Material;
        }

    }

    //Co routines
    IEnumerator CheckMissionCompletedCO()
    {
        while (true)
        {
            int aliveCounter = 0;
            for (int i = 0; i < leachShips.Length; i++)
            {
                if (leachShips[i] != null)
                    aliveCounter++;
            }

            if (aliveCounter <= 0)
            {
                StartCoroutine(SwapCO());

                planeRenderer.gameObject.SetActive(true);
                levelCompletedBoxCollider.enabled = true;

                inGameUiHandler.OnMissionCompleted(); 

                isMissionCompletedHandled = true;

                break;
            }

            yield return new WaitForSeconds(1);

        }

    }
}
