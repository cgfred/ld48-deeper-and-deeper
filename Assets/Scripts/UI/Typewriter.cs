using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Typewriter : MonoBehaviour
{
    public TextMeshProUGUI dialogTextMesh;
    AudioSource textAppearAudioSource;

    //Local variables
    string dialogText;

    float startDelay = 0;

    //Other components
    TutorialUIHandler tutorialUIHandler=null;

    void Awake()
    {
        textAppearAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetTextToUse(string newText)
    {
        dialogText = newText;
    }

    public void SetTutorialHandler(TutorialUIHandler newTutorialUIHandler)
    {
        tutorialUIHandler = newTutorialUIHandler;
    }

    public void Play(float startDelay_)
    {
        //Clear existing text
        dialogTextMesh.text = "";

        startDelay = startDelay_;

        StartCoroutine(TypeCO());
    }

    public TextMeshProUGUI GetTextMesh()
    {
        return dialogTextMesh;
    }

    //Co routines
    IEnumerator TypeCO()
    {
        float characterTypeDelay = 0.02f;

        yield return new WaitForSeconds(startDelay);

        int currentTextLength = 0;
        bool isDoneTyping = false;

        if(textAppearAudioSource !=null)
            textAppearAudioSource.volume = 0.1f;

        while (!isDoneTyping)
        {
            dialogTextMesh.text = dialogText.Substring(0, currentTextLength);
            dialogTextMesh.ForceMeshUpdate(true);

            currentTextLength++;

            if (currentTextLength > dialogText.Length)
                isDoneTyping = true;

            //Increase SFX volume as more text appears
            float completedPercentage = currentTextLength / 20.0f;
            completedPercentage = Mathf.Clamp01(completedPercentage);

            if (textAppearAudioSource != null)
                textAppearAudioSource.volume = Mathf.Lerp(textAppearAudioSource.volume, 0.9f, completedPercentage);

            yield return new WaitForSeconds(characterTypeDelay);
        }

        if (textAppearAudioSource != null)
            textAppearAudioSource.volume = 0;

        //Delay a little bit before telling the UI that we are done.
        yield return new WaitForSeconds(0.3f);

        //Tell the tutorial handler that the current dialog is done
        if (tutorialUIHandler != null)
            tutorialUIHandler.IsDialogCompleted();
    }
}
