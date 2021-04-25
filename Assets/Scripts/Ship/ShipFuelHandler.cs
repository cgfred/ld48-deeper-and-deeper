using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFuelHandler : MonoBehaviour
{
    float currentFuelLevel = 10;

    float maxFuelLevel = 10;

    void Awake()
    {
        if(!GetComponent<ShipMovementHandler>().IsPlayer())
        {
            Destroy(this);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ConsumeFuel(float amount)
    {
        if (currentFuelLevel > 0)
            currentFuelLevel -= amount;

        currentFuelLevel = Mathf.Clamp(currentFuelLevel, 0, maxFuelLevel);
    }

    public bool IsOutOfFuel()
    {
        if (currentFuelLevel > 0)
            return false;
        else return true;
    }

    public float GetFuelLevel(out float maxFuel)
    {
        maxFuel = maxFuelLevel;

        return currentFuelLevel;
    }
}
