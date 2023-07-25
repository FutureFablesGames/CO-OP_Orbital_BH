using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR_UI
{
    //This file might be useless. Reference MGR_Interface.cs
    private static MGR_UI instance;
    private static bool Initialized = false;

    public static MGR_UI Initialize()
    {
        if (Initialized) Debug.LogWarning("Warning: UI Manager already exists. Please use Manager.UI instead.");

        if (instance == null) instance = new MGR_UI();
        return instance;
    }
}
