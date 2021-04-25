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

    Vector3 aimAtVector = Vector3.right;

    float turretTurnFactor = 200;

    float weaponDamage = 0.25f;

    bool isPlayer = false;

    float weaponMaxDistance = 300;

    //Other components
    ShipInputHandler shipInputHandler;

    void Awake()
    {
        lineRenderer=GetComponent<LineRenderer>();

        laserHitParticleSystem = GetComponentInChildren<ParticleSystem>();
        laserParticleSystemEmissionModule = laserHitParticleSystem.emission;

        //Detach the laser particle system from the object it's attached to.
        laserHitParticleSystem.transform.parent = null;

        if (transform.root.CompareTag("Player"))
            isPlayer = true;

        shipInputHandler = GetComponentInParent<ShipInputHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isPlayer)
            turretTurnFactor = 20;
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
            if (Physics.SphereCast(transform.position, 3, transform.forward, out RaycastHit raycastHit, weaponMaxDistance))
            {
                hitPosition = raycastHit.point;

                laserHitParticleSystem.transform.position = hitPosition + transform.forward;

                laserParticleSystemEmissionModule.rateOverTime = 10;

                HPHandler hpHandler = raycastHit.transform.root.GetComponent<HPHandler>();

                if (hpHandler != null)
                    hpHandler.OnHit(weaponDamage);
            }
            else
            {
                hitPosition = transform.position + transform.forward * weaponMaxDistance;
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
        AimTurretAt();
    }

    void AimTurretAt()
    {
        //Desired rotation
        Quaternion desiredRotation = Quaternion.LookRotation(new Vector3(aimAtVector.x, 0, aimAtVector.z));

        //Get how far we should rotate
        float angle = Quaternion.Angle(transform.rotation, desiredRotation);

        float donePercentage = Mathf.Min(1F, turretTurnFactor * Time.deltaTime / angle);

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, donePercentage);
 
    }

    public void SetAimVector(Vector3 newAimVector)
    {
        aimAtVector = newAimVector;
    }

    public void SetIsFiring(bool isFiring_)
    {
        isFiring = isFiring_;
    }
}
