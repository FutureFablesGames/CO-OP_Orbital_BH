using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // ================================================
    // COMPONENTS / COMPONENTS / COMPONENTS / COMPONEN
    // ================================================

    public PlayerCharacter Owner;
    
    [Header("Weapons")]
    public Weapon CurrentWeapon;    
    [SerializeField] Weapon RangedWeapon;
    [SerializeField] Weapon MeleeWeapon;

    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    // -- Resources
    [Header("Resources")]
    public float CurrentResources;
    //public float MaxResources = 500.0f;

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================

    public void SwapWeapon()
    {
        // Swap weapons
        Weapon next = (CurrentWeapon == MeleeWeapon) ? RangedWeapon : MeleeWeapon;        
        Equip(next);               
    }

    public void Equip(Weapon w)
    {
        // Unequip the current weapon
        if (CurrentWeapon != null) Unequip(CurrentWeapon);

        // Check to see if we're replacing an existing weapon with a new one
        switch (w.GetWeaponType())
        {
            case WeaponType.Melee:
                if (w != MeleeWeapon)
                {
                    Destroy(MeleeWeapon);               // -- Destroy the current weapon to remove it from the scene
                    MeleeWeapon = w;                    // -- Set our new melee weapon reference
                    MeleeWeapon.Owner = Owner.PC;       // -- Make sure to initialize the owner of the weapon         
                }
                
                break;
            case WeaponType.Ranged:            
                if (w != RangedWeapon)
                {
                    Destroy(RangedWeapon);              // -- Destroy the current weapon to remove it from the scene
                    RangedWeapon = w;                   // -- Set our new ranged weapon reference
                    RangedWeapon.Owner = Owner.PC;      // -- Make sure to initialize the owner of the weapon        
                }                
                break;
        }
        
        CurrentWeapon = w;                          // -- Set our current weapon to our newly equipped weapon
        CurrentWeapon.OnEquip();
        CurrentWeapon.gameObject.SetActive(true);   // -- And don't forget to activate it on in case it's hidden.
        Owner.PC.animationHandler.animator.SetInteger("Stance", (CurrentWeapon.GetWeaponType() == WeaponType.Melee) ? 0 : 1);
    }

    public void Unequip(Weapon w)
    {
        CurrentWeapon.OnUnequip();
        CurrentWeapon.gameObject.SetActive(false);
        CurrentWeapon = null;        
    }
}
