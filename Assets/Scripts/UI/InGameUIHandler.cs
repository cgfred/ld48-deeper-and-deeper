using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InGameUIHandler : MonoBehaviour
{
    [Header("Fuel indicator")]
    public Image fuelIndicatorImage;

    ShipFuelHandler shipFuelHandler;

    void Awake()
    {
       GameObject player =  GameObject.FindGameObjectWithTag("Player");

       shipFuelHandler = player.GetComponent<ShipFuelHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PeriodicUpdates());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PeriodicUpdates()
    {
        while(true)
        {
            if(shipFuelHandler !=null)
            {
                float currentFuelLevel = shipFuelHandler.GetFuelLevel(out float maxFuel);

                Vector2 fuelIndicatorScale = fuelIndicatorImage.transform.localScale;

                fuelIndicatorScale.x = (currentFuelLevel / maxFuel);

                fuelIndicatorImage.transform.localScale = fuelIndicatorScale;

            }

            yield return new WaitForSeconds(0.1f);
        }
       
    }
}
