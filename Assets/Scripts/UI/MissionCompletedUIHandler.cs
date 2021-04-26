using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MissionCompletedUIHandler : MonoBehaviour
{
    [Header("UI parts")]
    public GameObject characterDialogGameObject;
    public Typewriter dialogTypewriter;


    // Start is called before the first frame update
    void Awake()
    {
        //Only run the tutorial handler on tutorials. 
        if (SceneManager.GetActiveScene().name.Contains("Tutorial"))
        {
            characterDialogGameObject.SetActive(false);
            Destroy(this);
            return;
        }

        characterDialogGameObject.gameObject.SetActive(false);
    }

   public void OnMissionCompleted()
    {
        string dialogText = "Set some dialog";

        if (SceneManager.GetActiveScene().name.Contains("Missiles First Encounter"))
            dialogText = "Those UNET back stabbers! Why are they attacking us? We are supposed to be allied. Commander,  return to the jump gate we need to go deeper into space to figure out what the heck is going on.";

        if (SceneManager.GetActiveScene().name.Contains("Missiles Ambush"))
            dialogText = "UNET scum ambushed us! I've had it with this! Return to the jump gate, we're heading into deep space and the home of UNET. ";

        if (SceneManager.GetActiveScene().name.Contains("FinalBoss"))
            dialogText = "You defeated the UNET! Now you can finally go back and enjoy life in Unity. Return to the jump gate.";


        characterDialogGameObject.gameObject.SetActive(true);
        dialogTypewriter.SetTextToUse(dialogText);
        dialogTypewriter.Play(0);

    }
}
