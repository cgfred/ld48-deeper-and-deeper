using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioSource audioSourceLayer2;
    public AudioSource audioSourceLayer7;
    public AudioSource audioSourceLayer8;

    float lastTimeCombat = -100;

    void Awake()
    {
        audioSourceLayer8.volume = 0;
        audioSourceLayer7.volume = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Time.time - lastTimeCombat < 4)
            audioSourceLayer8.volume = Mathf.Lerp(audioSourceLayer8.volume, 1.0f, Time.deltaTime * 1);
        else audioSourceLayer8.volume = Mathf.Lerp(audioSourceLayer8.volume, 0.0f, Time.deltaTime * 0.5f);
        */

        if (Time.time - lastTimeCombat < 5)
        {
            audioSourceLayer2.volume = 0;
            audioSourceLayer7.volume = 1.0f;
        }
        else
        {
            audioSourceLayer2.volume = 1.0f;
            audioSourceLayer7.volume = 0.0f;
        }
    }

    public void SetLastTimeMissileFired()
    {
        lastTimeCombat = Time.time;
    }
}
