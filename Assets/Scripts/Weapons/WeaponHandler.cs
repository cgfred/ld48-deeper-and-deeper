using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    LineRenderer lineRenderer;

    Vector3 hitPosition = Vector3.zero;

    ParticleSystem laserHitParticleSystem;
    ParticleSystem.EmissionModule laserParticleSystemEmissionModule;

    bool isFiring = false;

    //Other components
    ShipInputHandler shipInputHandler;

    void Awake()
    {
        lineRenderer=GetComponent<LineRenderer>();

        laserHitParticleSystem = GetComponentInChildren<ParticleSystem>();
        laserParticleSystemEmissionModule = laserHitParticleSystem.emission;

        //Detach the laser particle system from the object it's attached to.
        laserHitParticleSystem.transform.parent = null;

        shipInputHandler = GetComponentInParent<ShipInputHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(shipInputHandler !=null)
        {
            isFiring = shipInputHandler.IsFiring();
        }

        if (isFiring)
        {
            if (Physics.SphereCast(transform.position, 3, transform.forward, out RaycastHit raycastHit, 300))
            {
                hitPosition = raycastHit.point;

                laserHitParticleSystem.transform.position = hitPosition + transform.forward;

                laserParticleSystemEmissionModule.rateOverTime = 10;
            }
            else
            {
                hitPosition = transform.position + transform.forward * 300;
                laserParticleSystemEmissionModule.rateOverTime = 0;
            }

            //Render LAZERS
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hitPosition);
        }
        else
        {
            lineRenderer.enabled = false;
            laserParticleSystemEmissionModule.rateOverTime = 0;
        }
    }

    void FixedUpdate()
    {
      
    }

    public void SetFiring(bool isFiring_)
    {
        isFiring = isFiring_;
    }
}
