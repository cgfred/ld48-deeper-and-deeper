using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    TutorialUIHandler tutorialUIHandler;

    void Awake()
    {
        tutorialUIHandler=FindObjectOfType<TutorialUIHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.CompareTag("Player"))
        {
            tutorialUIHandler.OnTriggerHit();

            Destroy(gameObject);
        }
    }
}
