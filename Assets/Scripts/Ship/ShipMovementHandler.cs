using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementHandler : MonoBehaviour
{
    public GameObject modelObject;

    Rigidbody shipRigidbody;

    float maxTurnSpeed = 2;
    float turnRate = 5;

    float maxSpeed = 75;

    float tilt = 0;

    Vector2 inputVector = Vector2.zero;

    bool isPlayer = false;

    void Awake()
    {
        shipRigidbody = GetComponent<Rigidbody>();

        if (gameObject.CompareTag("Player"))
            isPlayer = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set our own inertiaTensor, otherwise it's set based on the colliders and causes a heap of problems with steering
        shipRigidbody.inertiaTensor = new Vector3(1, 1, 1);

        if (!isPlayer)
            maxSpeed = maxSpeed * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        RotateShip();
        MoveForward();
        UpdateShipTilt();
    }

    void MoveForward()
    {
        if (inputVector.y > 0)
            shipRigidbody.AddForce(transform.forward * 150 * inputVector.y, ForceMode.Force);

        if (shipRigidbody.velocity.magnitude > maxSpeed)
            shipRigidbody.velocity = shipRigidbody.velocity.normalized * maxSpeed;
    }

    void RotateShip()
    {
        if (inputVector.x > 0 && shipRigidbody.angularVelocity.y > maxTurnSpeed)
            return;

        if (inputVector.x < 0 && shipRigidbody.angularVelocity.y < maxTurnSpeed*-1)
            return;

        //Stop the ship from rotating when the player lets go of the keys
        /*
        if (inputVector.x == 0)
            shipRigidbody.angularDrag = 20;
        else */

        shipRigidbody.angularDrag = 20.0f - Mathf.Abs(inputVector.x)*20.0f;

        shipRigidbody.AddTorque(new Vector3(0, inputVector.x * turnRate, 0));
    }

    void UpdateShipTilt()
    {
        float angularVelocity = shipRigidbody.angularVelocity.y * 25 * -1;

        tilt = Mathf.Lerp(tilt, angularVelocity, Time.fixedDeltaTime * 4);

        tilt = Mathf.Clamp(tilt, -40, 40);

        modelObject.transform.localRotation = Quaternion.AngleAxis(tilt, Vector3.forward);
    }

    public void SetInput(Vector2 input)
    {
        input.x = Mathf.Clamp(input.x, -1, 1);
        input.y = Mathf.Clamp(input.y, -1, 1);

        inputVector = input;
    }

    public float GetInputForwardAmount()
    {
        return inputVector.y;
    }
}
