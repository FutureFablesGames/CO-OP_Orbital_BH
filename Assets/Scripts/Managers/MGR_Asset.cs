using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR_Asset : MonoBehaviour
{
    private static MGR_Asset instance;
    private static bool Initialized = false;
    public static MGR_Asset Initialize()
    {
        if (Initialized) Debug.LogWarning("WARNING: Asset Manager already exists.  Please use Manager.Assets instead.");

        if (instance == null) instance = new MGR_Asset();
        return instance;
    }
}
