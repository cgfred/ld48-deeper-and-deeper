using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


using TMPro;

public class TutorialUIHandler : MonoBehaviour
{
    [Header("UI parts")]
    public GameObject characterDialogGameObject;
    public Typewriter dialogTypewriter;
    int tutorialTextIndex = 0;

    bool isTriggerColliderActivated = false;

    GameObject leachShip; 


    // Start is called before the first frame update
    void Awake()
    {
        //Only run the tutorial handler on tutorials. 
        if (!SceneManager.GetActiveScene().name.Contains("Tutorial"))
        {
            Destroy(this);
            return;
        }

        dialogTypewriter.SetTutorialHandler(this);

        leachShip = GameObject.FindGameObjectWithTag("LeachShip");
    }

    void Start()
    {
        //Set character dialog enabled
        characterDialogGameObject.gameObject.SetActive(true);

        ProgressTutorialBasics();
    }

    void Update()
    {
        if (leachShip == null && tutorialTextIndex < 3)
        {
            ProgressTutorialBasics();
        }
    }

    void PlayText(string textToUse)
    {
        dialogTypewriter.SetTextToUse(textToUse);

        float delay = 1.0f;

        if (tutorialTextIndex > 0)
            delay = 0.1f;

        dialogTypewriter.Play(delay);
    }

  

    public void IsDialogCompleted()
    {/*
        switch (SceneManager.GetActiveScene().name)
        {
            
            case "Tutoria":
                if (tutorialTextIndex == 1)
                    ShowNextIcon();

                if (tutorialTextIndex == 2)
                {
                    SetTankInputDisabled(false);
                    ShowThumbStickIcon();

                    if (arrowDriveTo != null)
                    {
                        arrowDriveTo.SetActive(true);
                        SFXHandler.instance.PlayPositiveResponse2(0.5f);
                    }
                }
            break;

            case "TutorialFireScene":
                if (tutorialTextIndex == 1)
                    ShowNextIcon();
                else if (tutorialTextIndex == 2)
                    ShowNextIcon();
                else if(tutorialTextIndex == 3)
                    SetTankFireInputDisabled(false);
                break;
        }

        */
    }

    void ProgressTutorial()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Tutorial":
                ProgressTutorialBasics();
                break;
        }
    }

    void ProgressTutorialBasics()
    {
       // CGUtils.DebugLog($"ProgressTutorialMovement index {tutorialTextIndex }");
        switch (tutorialTextIndex)
        {
            case 0:
                PlayText("Commander the jump gate is offline due to power failure. Follow the grid lines and check the power source. Use WAD or Arrow keys to steer your ship.");
                break;

            case 1:
                PlayText("What the heck! A Voxeltron ship is leaching from our power source. Use your mouse and shoot it down with left mouse button!");
                break;

            case 2:
                PlayText("Great work commmander, power is back online! Return to the jump gate and venture DEEPER into space.");
                break;
        }

        tutorialTextIndex++;

      //  CGUtils.DebugLog($"end ProgressTutorialMovement index {tutorialTextIndex }");
    }

    public void OnTriggerHit()
    {
        //Only detect even once 
        if (isTriggerColliderActivated)
            return;

        ProgressTutorialBasics();

        isTriggerColliderActivated = true;
    }
}
