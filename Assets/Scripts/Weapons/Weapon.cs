using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    SemiAuto,
    Automatic,
    Charged
}

public abstract class Weapon : MonoBehaviour
{
    public PlayerController Owner;

    // -------------------------------------------------------
    // Abstract Methodology
    // -------------------------------------------------------

    public abstract void PrimaryFire();
    public abstract void SecondaryFire();

    // -------------------------------------------------------
    // Getters & Setters
    // -------------------------------------------------------

    public abstract string GetWeaponName();
    public abstract float GetBaseDamage();
    public abstract float GetRange();
    public abstract TriggerType GetTriggerType();


}
