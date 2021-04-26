using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherHandler : MonoBehaviour
{
    [Header("Settings")]
    public bool isTurretRotatable = false;

    [Header("MissilePrefab")]
    public GameObject missilePrefab;

    bool isFiring = false;

    float turretTurnFactor = 400;

    Vector3 aimAtVector;

    float lastTimeFired = 0;
    float reloadTime = 1.5f;

    //Other components
    ShipInputHandler shipInputHandler;

    void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }


    void FixedUpdate()
    {
        if (isTurretRotatable)
            AimTurretAt();
        else aimAtVector = transform.forward;
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

    public bool Fire()
    {
        //Wait until missile is reloaded.
        if (Time.time - lastTimeFired < reloadTime)
            return false;

        Instantiate(missilePrefab, transform.position, Quaternion.LookRotation(aimAtVector,Vector3.up));

        lastTimeFired = Time.time;

        return true;
    }
}
