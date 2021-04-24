using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEngineParticleSystemHandler : MonoBehaviour
{
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    float inputForwardLastFrame = 0;

    //Other components
    ShipMovementHandler shipMovementHandler;

    void Awake()
    {
        particleSystemEmissionModule = GetComponent<ParticleSystem>().emission;
        shipMovementHandler = GetComponentInParent<ShipMovementHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputForwardCurrentFrame = shipMovementHandler.GetInputForwardAmount();

        if (inputForwardLastFrame == 0 && inputForwardCurrentFrame > 0)
            particleSystemEmissionModule.rateOverTime = 100;
        else particleSystemEmissionModule.rateOverTime = inputForwardCurrentFrame * 10;




        //Store the input value from last fram
        inputForwardLastFrame = shipMovementHandler.GetInputForwardAmount();
    }
}
