using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus;

public class DetectHeadset : MonoBehaviour
{
    public bool hasFaceTracking;

    private void Start()
    {
        var headsetType = Utils.GetSystemHeadsetType();
        Debug.Log("System headset type: " + headsetType);
        string headsetString = headsetType.ToString();

        // Quest Pro
        if (headsetString == "Meta_Link_Quest_Pro" || headsetString == "Meta_Quest_Pro" || headsetString == "Placeholder_10")
        {
            OVRPlugin.systemDisplayFrequency = 90.0f;
            hasFaceTracking = true;
            // set fixed timestep
            Time.fixedDeltaTime = 0.0111f;
        }   
        else // Quest 2
        {
            hasFaceTracking = false;
            // set fixed timestep
            Time.fixedDeltaTime = 0.0138f;
        }        
    }
}
