using UnityEngine;
using System.Collections;


public static class CGUtils {

    public static void DebugLog(string log)
	{
		if(GameManager.isDebugEnabled)
		{
			Debug.Log(Time.time+": "+log);
		}
	}

	public static void DebugLogError(string log)
	{
        if (GameManager.isDebugEnabled)
        {
            Debug.LogError($"{Time.time} {log}");
        }
	}

    public static void SetObjectIncludingChildren(Transform transform, int layer)
    {
        Transform[] transforms = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transforms.Length; i++)
        {
            //Change to the requested layer
            transforms[i].gameObject.layer = layer;
        }
    }

    public static float Clamp0360(float eulerAngles)
    {
        float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
        if (result < 0)
        {
            result += 360f;
        }

        return result;
    }

    public static Color HexToColor(string hex)
    {
        if (hex.Length == 0)
        {
            CGUtils.DebugLogError("HexToColor was called with hex length zero!");

            //Just go with black colors if we didn't the correct color.
            return new Color32(0, 0, 0, 255);
        }

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }

    

}