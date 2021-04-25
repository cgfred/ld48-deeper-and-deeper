

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Loads all other required classes
public class Loader : MonoBehaviour
{
    public GameManager gameManagerPrefab;

    public GameObject levelHandler;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(gameManagerPrefab);

        if (LevelsHandler.instance == null)
            Instantiate(levelHandler);
    }

}
