using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementHandler : MonoBehaviour
{
    public AudioSource shipEngineAudioSource;
    public GameObject modelObject;

    Rigidbody shipRigidbody;

    public float maxTurnSpeed = 2;
    public float turnRate = 5;

    public float maxSpeed = 75;

    float tilt = 0;

    Vector2 inputVector = Vector2.zero;

    bool isPlayer = false;

    bool isLevelLoadingStarted = false;

    Vector3 jumpGatePosition;

    //Other components
    ShipFuelHandler shipFuelHandler;

    void Awake()
    {
        shipRigidbody = GetComponent<Rigidbody>();

        if (gameObject.CompareTag("Player"))
            isPlayer = true;

        if(isPlayer)
        {
            shipFuelHandler = GetComponent<ShipFuelHandler>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set our own inertiaTensor, otherwise it's set based on the colliders and causes a heap of problems with steering
        shipRigidbody.inertiaTensor = new Vector3(1, 1, 1);
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
        {
            shipRigidbody.AddForce(transform.forward * 150 * inputVector.y, ForceMode.Force);

            if (isPlayer)
                shipFuelHandler.ConsumeFuel(inputVector.y * 0.001f);

            shipRigidbody.drag = 0;

            shipEngineAudioSource.volume = Mathf.Lerp(shipEngineAudioSource.volume, 1.0f, Time.deltaTime * 2);
        }
        else
        {
            shipRigidbody.drag = 1;
            shipEngineAudioSource.volume = Mathf.Lerp(shipEngineAudioSource.volume, 0.2f, Time.deltaTime * 2);
        }

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

    public bool IsPlayer()
    {
        return isPlayer;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpGate") && isPlayer)
        {
            if (!isLevelLoadingStarted)
            {
                LevelsHandler.instance.LoadNextLevelDelayed(1.5f);

                shipRigidbody.isKinematic = true;

                isLevelLoadingStarted = true;

                StartCoroutine(ScaleOutPlayerCO());

                jumpGatePosition = other.transform.position;
            }


        }
    }

    //Co routines
    IEnumerator ScaleOutPlayerCO()
    {
        while (true)
        {

            //Delay a little bit before telling the UI that we are done.
            yield return new WaitForSeconds(0.01f);

            transform.localScale = transform.localScale * 0.96f;
            transform.position = Vector3.Lerp(transform.position, jumpGatePosition, Time.deltaTime*5);
        }

    }
}
