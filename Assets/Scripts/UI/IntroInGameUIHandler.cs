using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroInGameUIHandler : MonoBehaviour
{
    public Typewriter typewriter;
    public bool isOutro = false;

    void Awake()
    {
        if (!isOutro)
        {
            typewriter.SetTextToUse("Ahh there you are commander. The Unity Star Node System has stopped working. We're sending you out to check what is wrong with it. Probably just a glitch in the latest patch. Press SPACE to start. We recommend to play in full screen mode.");
            typewriter.Play(1.5f);
        }
        else
        {
            typewriter.SetTextToUse("Thanks for playing my entry for LD48. Special thanks to Kenney for creating KenShap which the models were made in. Press SPACE to play again.");
            typewriter.Play(0.5f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelsHandler.instance.LoadFirstLevel();
        }
    }
}
