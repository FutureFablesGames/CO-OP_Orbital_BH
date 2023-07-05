using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR_Sound
{
    private static MGR_Sound instance;
    private static bool Initialized = false;
    public static MGR_Sound Initialize()
    {
        if (Initialized) Debug.LogWarning("WARNING: Sound Manager already exists.  Please use Manager.Sound instead.");

        if (instance == null) instance = new MGR_Sound();
        return instance;
    }
}
