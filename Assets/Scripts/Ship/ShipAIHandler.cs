using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAIHandler : MonoBehaviour
{
    public enum AIMode { followPlayer, followWaypoints };

    [Header("AI settings")]
    public AIMode aiMode;

    //Local variables
    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;

    WeaponHandler[] weaponHandlers;
    MissileLauncherHandler[] missileLauncherHandler;


    float minDistanceBeforeFollowingPlayer = 600;
    float minDistanceBeforeAttackingPlayer = 450;

    //Components
    ShipMovementHandler shipMovementHandler;

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        shipMovementHandler = GetComponent<ShipMovementHandler>();
        weaponHandlers = GetComponentsInChildren<WeaponHandler>();
        missileLauncherHandler = GetComponentsInChildren<MissileLauncherHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame and is frame dependent
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        switch (aiMode)
        {
            case AIMode.followPlayer:
                FollowPlayer();
                break;

            case AIMode.followWaypoints:
                FollowWaypoints();
                break;
        }

        inputVector.x = TurnTowardTarget();
        inputVector.y = 1.0f - inputVector.x/0.9f; //Make the ship accelerate less while turning

        AimAtPlayer();

        FireAtPlayer();

        //Send the input to the car controller.
        shipMovementHandler.SetInput(inputVector);
    }

    void FollowPlayer()
    {
        if (targetTransform == null)
        {
            GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");

            if (playerGameObject != null)
                targetTransform = playerGameObject.transform;
        }

        if (targetTransform != null)
        {
            if((transform.position - targetTransform.position).magnitude < minDistanceBeforeFollowingPlayer )
                targetPosition = targetTransform.position;
        }
    }

    void FollowWaypoints()
    {
        /*
        if (currentWaypoint == null)
            currentWaypoint = (Waypoint)FindObjectOfType(typeof(Waypoint));

        //Set the target on the waypoints position
        if (currentWaypoint != null)
        {
            targetPosition = currentWaypoint.transform.position;

            //Store how close we are to the target
            float distanceToWayPoint = (targetPosition - transform.position).magnitude;

            //Check if we are close enough to consider that we have reached the waypoint
            if (distanceToWayPoint <= currentWaypoint.minDistanceToReachWaypoint)
            {
                //If we are close enough then follow to the next waypoint, if there are multiple waypoints then pick one at random.
                currentWaypoint = currentWaypoint.nextWaypoint[Random.Range(0, currentWaypoint.nextWaypoint.Length)];
            }
        }
        */
    }

    void FireAtPlayer()
    {
        if (targetTransform == null)
            return;

        if ((transform.position - targetTransform.position).magnitude > minDistanceBeforeAttackingPlayer)
            return;

        for (int i=0;i< missileLauncherHandler.Length;i++)
        {
            missileLauncherHandler[i].Fire();
        }
    }

    void AimAtPlayer()
    {
        if (targetTransform == null)
        {
            GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");

            if (playerGameObject != null)
                targetTransform = playerGameObject.transform;
        }

        Vector3 aimAtPosition = Vector3.one;

        if (targetTransform != null)
            aimAtPosition = targetTransform.position;

        for (int i = 0; i < weaponHandlers.Length; i++)
        {
            Vector3 aimVector = aimAtPosition - weaponHandlers[i].transform.position;
            aimVector.Normalize();

            weaponHandlers[i].SetAimVector(aimVector);
            weaponHandlers[i].SetIsFiring(true);
        }
    }

    float TurnTowardTarget()
    {
        float angleToTarget = Vector3.SignedAngle(transform.forward, targetPosition - transform.position, Vector3.up);

        float steerAmount = angleToTarget / 90.0f;

        return steerAmount;
    }

}
