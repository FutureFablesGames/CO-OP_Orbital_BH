using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    [HideInInspector] public PlayerController PC;
    public Inventory inventory;

    // ================================================
    // MONOBEHAVIOUR / MONOBEHAVIOUR/ MONOBEHAVIOUR /    
    // ================================================

    private void Awake()
    {
        PC = GetComponent<PlayerController>();
        inventory.Owner = this;
    }

    public void Start()
    {
        // -- Load our weapons based on loadout / player settings
        GameObject melee = Instantiate(
            Resources.Load<GameObject>("Prefabs/Weapons/Pickaxe"),
            inventory.gameObject.transform
            );

        GameObject ranged = Instantiate(
            Resources.Load<GameObject>("Prefabs/Weapons/Autogun"),
            inventory.gameObject.transform
            );

        // -- Equip the weapons to set ownership
        inventory.Equip(ranged.GetComponent<Weapon>());
        inventory.Equip(melee.GetComponent<Weapon>());
    }

}
