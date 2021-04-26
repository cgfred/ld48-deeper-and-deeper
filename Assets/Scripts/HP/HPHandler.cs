using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPHandler : MonoBehaviour
{
    public GameObject shipExplosionParticleSystem;

    public float hp = 1.0f;

    float maxHP;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetHP(out float maxHP_)
    {
        maxHP_ = maxHP;

        return hp;
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
