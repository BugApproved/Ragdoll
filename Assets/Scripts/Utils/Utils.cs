using UnityEngine;

public class Utils
{
    public static void DebugLog(string message)
    {
        Debug.Log($"{Time.time} {message}");
    }
    public static void DebugLogError(string message)
    {
        Debug.LogError($"{Time.time} {message}");
    }
}
