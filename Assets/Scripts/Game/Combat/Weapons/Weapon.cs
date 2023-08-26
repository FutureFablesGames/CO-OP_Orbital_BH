using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    SemiAuto,
    Automatic,
    Charged
}

public enum WeaponType
{
    Melee,
    Ranged
}

public abstract class Weapon : MonoBehaviour
{
    public PlayerController Owner;

    public Coroutine PrimaryRoutine;
    public Coroutine SecondaryRoutine;

    // -------------------------------------------------------
    // Abstract Methodology
    // -------------------------------------------------------

    public abstract void PrimaryFire();
    public abstract void PrimaryCancel();
    public abstract void SecondaryFire();
    public abstract void SecondaryCancel();

    public abstract void OnEquip();
    public abstract void OnUnequip();

    // -------------------------------------------------------
    // Getters & Setters
    // -------------------------------------------------------

    public abstract string GetWeaponName();
    public abstract WeaponType GetWeaponType();
    public abstract float GetBaseDamage();
    public abstract float GetRange();
    public abstract TriggerType GetTriggerType();


}
