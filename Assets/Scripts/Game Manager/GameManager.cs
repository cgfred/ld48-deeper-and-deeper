
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{

    //Static instance of GameManager so other scripts can access it
    public static GameManager instance = null;

    //Globals variables
    public static bool isDebugEnabled = true;
    public static bool isFPSCounterEnabled = false;

    public static string gameVersion = "0.0.2";

    public const string gameName = "LD48";

   
    bool isOneTimeSetupHandled = false;


    //Event declarations
    public event Action<GameManager> OnGameStateChange;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        OneTimeSetup();

        
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }


    void OneTimeSetup()
    {
        if (isOneTimeSetupHandled)
            return;

        Application.targetFrameRate = 60; //We wan't 60 FPS
        Physics.reuseCollisionCallbacks = true; //We want to reduce garbage collection

      
        //Set specific rendering qualities for Nintendo Switch
#if UNITY_SWITCH
        QualitySettings.renderPipeline = nintendoSwitchRenderPipleLineQuality;
#endif

        isOneTimeSetupHandled = true;
    }

    void LevelStart()
    {

    }

   

   

    /// <summary>
    /// EVENTS!
    /// </summary>

    private void OnEnable()
    {
        //Tell our 'OnSceneLoaded' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LevelStart();
    }

   
}
