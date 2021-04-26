using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InGameUIHandler : MonoBehaviour
{
    [Header("Fuel indicator")]
    public Image fuelIndicatorImage;

    [Header("HP indicator")]
    public Image hpIndicatorImage;

    [Header("Heat indicator")]
    public Image heatIndicatorImage;

    [Header("GameOver")]
    public GameObject gameOver;

    ShipFuelHandler shipFuelHandler;
    HPHandler hpHandler;
    ShipInputHandler shipInputHandler;

    bool isGameOverHandled = false;

    void Awake()
    {
       GameObject player =  GameObject.FindGameObjectWithTag("Player");

       shipFuelHandler = player.GetComponent<ShipFuelHandler>();
       hpHandler = player.GetComponent<HPHandler>();
       shipInputHandler = player.GetComponent<ShipInputHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PeriodicUpdates());
    }

    // Update is called once per frame
    void Update()
    {
        if(hpHandler == null && !isGameOverHandled)
        {
            StartCoroutine(GameOverCO());
          
            isGameOverHandled = true;
        }

        if(isGameOverHandled && Input.GetKeyDown(KeyCode.Space))
        {
            LevelsHandler.instance.RestartLevel();
        }
    }

    IEnumerator GameOverCO()
    {
        yield return new WaitForSeconds(1.5f);

        gameOver.SetActive(true);

    }

    IEnumerator PeriodicUpdates()
    {
        //Give the game a bit of time to start first
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            if(shipFuelHandler !=null)
            {
                float currentFuelLevel = shipFuelHandler.GetFuelLevel(out float maxFuel);

                Vector2 fuelIndicatorScale = fuelIndicatorImage.transform.localScale;

                fuelIndicatorScale.x = (currentFuelLevel / maxFuel);

                fuelIndicatorImage.transform.localScale = fuelIndicatorScale;

            }

            if (shipInputHandler != null)
            {
                float currentHeatLevel = shipInputHandler.GetHeatLevel(out float maxLevel);

                Vector2 indicatorScale = heatIndicatorImage.transform.localScale;

                indicatorScale.x = (currentHeatLevel / maxLevel);

                heatIndicatorImage.transform.localScale = indicatorScale;

            }

            if (hpHandler !=null)
            {
                float currentHPLevel = hpHandler.GetHP(out float maxHP);

                Vector2 hpIndicatorScale = hpIndicatorImage.transform.localScale;

                hpIndicatorScale.x = (currentHPLevel / maxHP);

                hpIndicatorImage.transform.localScale = hpIndicatorScale;
            }
            else
            {
                hpIndicatorImage.transform.localScale = new Vector3(0, hpIndicatorImage.transform.localScale.y, hpIndicatorImage.transform.localScale.z);
            }

            yield return new WaitForSeconds(0.1f);
        }
       
    }

    public void OnMissionCompleted()
    {
        MissionCompletedUIHandler missionCompletedUIHandler = GetComponent<MissionCompletedUIHandler>();

        if (missionCompletedUIHandler != null)
            missionCompletedUIHandler.OnMissionCompleted();
    }
}
