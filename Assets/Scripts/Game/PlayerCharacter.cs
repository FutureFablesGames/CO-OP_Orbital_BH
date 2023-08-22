using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    public PlayerController PC;
    public Inventory inventory;

    // ================================================
    // MONOBEHAVIOUR / MONOBEHAVIOUR/ MONOBEHAVIOUR /    
    // ================================================

    public void Start()
    {
        // -- Load our weapons based on loadout / player settings
        GameObject melee = Instantiate(
            Resources.Load<GameObject>("Prefabs/Weapons/Crystal Dagger"),
            inventory.gameObject.transform
            );

        GameObject ranged = Instantiate(
            Resources.Load<GameObject>("Prefabs/Weapons/Comet"),
            inventory.gameObject.transform
            );

        // -- Equip the weapons to set ownership
        inventory.Equip(ranged.GetComponent<Weapon>());
        inventory.Equip(melee.GetComponent<Weapon>());
    }

}
