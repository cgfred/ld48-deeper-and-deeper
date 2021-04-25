using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsHandler : MonoBehaviour
{
    [Header("The instance")]
    public static LevelsHandler instance = null;

    //Local variables
    string[] sceneNames = {"Tutorial", "Tutorial" };
    int currentLevelIndex = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        //Find the current loaded scene and set the index if found. This happens when you load a scene in the editor and isn't the start level
        for (int i=0;i< sceneNames.Length;i++)
        {
            if (SceneManager.GetActiveScene().name == sceneNames[i])
            {
                currentLevelIndex = i;
                break;
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex > sceneNames.Length - 1)
            currentLevelIndex = 0;

        SceneManager.LoadScene(sceneNames[currentLevelIndex]);
    }

    public void LoadNextLevelDelayed(float delay)
    {
        Invoke("LoadNextLevel", delay);
    }

    public void RestartLevelDelayed(float delay)
    {
        Invoke("RestartLevel", delay);
    }

    //Events
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
     
    }
}
