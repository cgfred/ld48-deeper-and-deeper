using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInputHandler : MonoBehaviour
{

    ShipMovementHandler shipMovementHandler;

    void Awake()
    {
        shipMovementHandler = GetComponent<ShipMovementHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.y = Input.GetAxis("Vertical");
        inputVector.y = Mathf.Clamp01(inputVector.y);

        inputVector.x = Input.GetAxis("Horizontal");

        shipMovementHandler.SetInput(inputVector);

    }
}
