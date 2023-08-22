using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerStatistics
{
    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    // -- DEFENSIVE STATS
    public int Health;      // -- How much damage can the character take before dying
    public int Armor;       // -- How much physical damage should be negated upon taking damage
    public int Resistance;  // -- How much elemental damage should be negated upon taking damage

    // -- RANGED STATS
    public int Ranged_Power;    // -- How much additional damage ranged weapons will deal
    public float Ranged_Speed;
    public float Ranged_Crit_Rate;           // -- How frequent will you deal bonus damage with a ranged weapon
    public float Ranged_Crit_Multiplier;     // -- How much bonus damage will you deal with a ranged critical hit

    // -- MELEE STATS
    public int Melee_Power;                 // -- How much additional damage melee weapons will deal
    public float Melee_Speed;               // -- How fast can you attack with a melee weapon
    public float Melee_Crit_Rate;           // -- How frequent will you deal bonus damage with a melee weapon
    public float Melee_Crit_Multiplier;     // -- How much bonus damage will you deal with a melee critical hit

    // -- OTHER STATS
    public int Movement_Speed;              // -- How fast can the character move
    public int Harvest_Power;               // -- How effective harvesting tools are
    public int Jump_Power;                  // -- How high can the character jump

    // ================================================
    // INITIALIZATION / INITIALIZATION / INITIALIZATIO
    // ================================================

    public static PlayerStatistics Zero()
    {
        PlayerStatistics result = new PlayerStatistics() { 
            Health = 0, Armor = 0, Resistance = 0,
            Ranged_Power = 0, Ranged_Speed = 0, Ranged_Crit_Rate = 0, Ranged_Crit_Multiplier = 0,
            Melee_Power = 0, Melee_Speed = 0, Melee_Crit_Rate = 0, Melee_Crit_Multiplier = 0,
            Movement_Speed = 0, Jump_Power = 0, Harvest_Power = 0,
        };

        return result;
    }

    public static PlayerStatistics Default()
    {
        PlayerStatistics result = new PlayerStatistics()
        {
            Health = 100, Armor = 0, Resistance = 0,
            Ranged_Power = 0, Ranged_Speed = 1f, Ranged_Crit_Rate = 0f, Ranged_Crit_Multiplier = 1.5f,
            Melee_Power = 0, Melee_Speed = 1f, Melee_Crit_Rate = 0f, Melee_Crit_Multiplier = 1.5f,
            Movement_Speed = 1, Jump_Power = 1, Harvest_Power = 1,
        };

        return result;
    }

    public PlayerStatistics Copy()
    {
        PlayerStatistics newCopy = new PlayerStatistics();
        newCopy.Health = Health;
        newCopy.Armor = Armor;
        newCopy.Resistance = Resistance;
        newCopy.Ranged_Power = Ranged_Power;
        newCopy.Ranged_Speed = Ranged_Speed;
        newCopy.Ranged_Crit_Rate = Ranged_Crit_Rate;
        newCopy.Ranged_Crit_Multiplier = Ranged_Crit_Multiplier;
        newCopy.Melee_Power = Melee_Power;
        newCopy.Melee_Speed = Melee_Speed;
        newCopy.Melee_Crit_Rate = Melee_Crit_Rate;
        newCopy.Melee_Crit_Multiplier = Melee_Crit_Multiplier;
        newCopy.Movement_Speed = Movement_Speed;
        newCopy.Jump_Power = Jump_Power;
        newCopy.Harvest_Power = Harvest_Power;
        return newCopy;          
    }
}
