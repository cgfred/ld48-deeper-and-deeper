 using UnityEngine;
using System.Collections;

using System.IO;

public class TakeScreenshot : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }

#if UNITY_EDITOR
    // Update is called once per frame
    void LateUpdate () {

		if(Input.GetKeyDown("p") )
		{
			ScreenCapture.CaptureScreenshot(Directory.GetCurrentDirectory() + "/screenshots/"+GameManager.gameName+"_"+System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")+".png");

            Debug.Log("Screenshot taken!");
		}
	}

#endif


}
