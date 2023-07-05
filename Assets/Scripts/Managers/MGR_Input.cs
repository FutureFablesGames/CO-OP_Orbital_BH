using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR_Input
{
    private static MGR_Input instance;
    private static bool Initialized = false;
    public static MGR_Input Initialize()
    {
        if (Initialized) Debug.LogWarning("WARNING: Input Manager already exists.  Please use Manager.Input instead.");

        if (instance == null) instance = new MGR_Input();
        return instance;
    }
}
