using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInputHandler : MonoBehaviour
{

    ShipMovementHandler shipMovementHandler;

    WeaponHandler[] weaponHandlers;

    ShipFuelHandler shipFuelHandler;

    bool isFiring = false;

    void Awake()
    {
        shipMovementHandler = GetComponent<ShipMovementHandler>();

        //Only use this component on players
        if(!shipMovementHandler.IsPlayer())
        {
            Destroy(this);
            return;
        }

        weaponHandlers = GetComponentsInChildren<WeaponHandler>();
        shipFuelHandler = GetComponent<ShipFuelHandler>();

    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.y = Input.GetAxis("Vertical");
        inputVector.y = Mathf.Clamp01(inputVector.y);

        //Sorry you cannot go forward without fuel.
        if (shipFuelHandler.IsOutOfFuel())
            inputVector.y = 0;

        inputVector.x = Input.GetAxis("Horizontal");

        isFiring = Input.GetButton("Fire1");

        shipMovementHandler.SetInput(inputVector);

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y;


        for (int i=0;i< weaponHandlers.Length;i++)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector3 aimVector = mouseWorldPosition - weaponHandlers[i].transform.position;

            aimVector.Normalize();

            weaponHandlers[i].SetAimVector(aimVector);

        }
    }

    public bool IsFiring()
    {
        return isFiring;
    }
}
