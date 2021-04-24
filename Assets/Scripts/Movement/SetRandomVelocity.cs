using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomVelocity : MonoBehaviour
{
    Rigidbody rigidbody_;

    Vector3 randomVelocity = Vector3.zero;

    void Awake()
    {
        rigidbody_ = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        randomVelocity.x = Random.Range(-1.0f, 1.0f);
        randomVelocity.y = Random.Range(-1.0f, 1.0f);
        randomVelocity.z = Random.Range(-1.0f, 1.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody_.angularVelocity = randomVelocity;
    }
}
