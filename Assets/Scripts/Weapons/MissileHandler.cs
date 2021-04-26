using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileHandler : MonoBehaviour
{
    Transform targetTransform = null;

    float turnFactor = 70;
    float speed = 100f;

    //Other components
    HPHandler hpHandler;

    void Awake()
    {
        hpHandler = GetComponent<HPHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (targetTransform == null)
        {
            GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");

            if (playerGameObject != null)
                targetTransform = playerGameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Follow target
        RotateMissileTowardsTarget();

        //Move the missile forwards
        MoveForward();
    }

    void MoveForward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        Vector3 position = transform.position;

        //Force missiles into the Y = 0 plane 
        position.y = Mathf.Lerp(position.y, 0, Time.deltaTime * 3);
        transform.position = position;
    }

    void RotateMissileTowardsTarget()
    {
        if (targetTransform == null)
            return;

        Vector3 aimAtVector = targetTransform.position - transform.position;

        aimAtVector.Normalize();
        aimAtVector.y = 0;
     

        //Desired rotation
        Quaternion desiredRotation = Quaternion.LookRotation(new Vector3(aimAtVector.x, 0, aimAtVector.z));

        //Get how far we should rotate
        float angle = Quaternion.Angle(transform.rotation, desiredRotation);

        float donePercentage = Mathf.Min(1F, turnFactor * Time.deltaTime / angle);

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, donePercentage);

    }

    void OnTriggerEnter(Collider otherCollider)
    {
        Transform otherRootTransform = otherCollider.transform.root;

        if (otherRootTransform.CompareTag("Player"))
        {
            otherRootTransform.GetComponent<HPHandler>().OnHit(5);

          //  CGUtils.DebugLog($"Missile hit player caused damage {otherRootTransform.name}");
        }
       // else CGUtils.DebugLog($"Missile hit something {otherRootTransform.name}");

        //destroy own missile
        hpHandler.OnHit(1000);
    }
}
