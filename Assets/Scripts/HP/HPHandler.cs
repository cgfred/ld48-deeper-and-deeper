using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPHandler : MonoBehaviour
{
    public GameObject shipExplosionParticleSystem;

    float hp = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit(float damageAmount)
    {
        hp -= damageAmount;

        if(hp <= 0)
        {
            shipExplosionParticleSystem.SetActive(true);
            shipExplosionParticleSystem.transform.parent = null;

            Destroy(gameObject);
        }
    }
}
