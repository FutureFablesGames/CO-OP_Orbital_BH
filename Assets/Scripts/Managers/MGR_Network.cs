using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR_Network : MonoBehaviour
{
    private static MGR_Network instance;
    private static bool Initialized = false;
    public static MGR_Network Initialize()
    {
        if (Initialized) Debug.LogWarning("WARNING: Network Manager already exists.  Please use Manager.Network instead.");

        if (instance == null) instance = new MGR_Network();
        return instance;
    }
}
