using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR_Settings
{
    private static MGR_Settings instance;
    private static bool Initialized = false;
    public static MGR_Settings Initialize()
    {
        if (Initialized) Debug.LogWarning("WARNING: Settings Manager already exists.  Please use Manager.Settings instead.");

        if (instance == null) instance = new MGR_Settings();
        return instance;
    }
}
