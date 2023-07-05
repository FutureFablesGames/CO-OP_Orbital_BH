using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR_Interface : MonoBehaviour
{
    private static MGR_Interface instance;
    private static bool Initialized = false;

    public static MGR_Interface Initialize()
    {
        if (Initialized) Debug.LogWarning("WARNING: UI Manager already exists.  Please use Manager.UI instead.");

        if (instance == null) instance = new MGR_Interface();
        return instance;
    }
}
