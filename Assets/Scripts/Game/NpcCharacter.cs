using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCharacter : Character
{
    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    public float CurrentResources;
    //public float MaxResources = 500.0f;
    public Weapon CurrentWeapon;

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================

    private void Awake()
    {        
        Initialize();
    }
}
