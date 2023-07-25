using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class Pickaxe : MeleeWeapon
{
    // -------------------------------------------------------
    //  Weapon Variables
    // -------------------------------------------------------

    const string weaponName = "Pickaxe";
    const float damage = 15.0f;         // Base Damage = 15f
    const float harvestPower = 25.0f;   // How much harvest power does the weapon have.  Unique to weapons that can harvest resource nodes.
    const float range = 2.0f;           // How far will the weapon reach?
    const float attackSpeed = 1.0f;     // How fast does the weapon attack?  ie. 1 swing per second (60 swings per minute) Should be relative to the animation  

    // -------------------------------------------------------
    // Overridable Functions
    // -------------------------------------------------------

    public override void PrimaryFire()
    {
        if (nextAttackTimer <= 0)
        {
            // Tie in any shot functionality here
            Debug.Log("Pickaxe Primary Called!");

            // Hitscan from player to mouse
            Ray ray = new Ray(Owner.mesh.transform.position, Owner.mesh.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, range))
            {
                // Declare Target
                GameObject targetHit = hit.transform.gameObject;
                
                // If it's a resource, mine it
                if (targetHit.tag == "Resource")
                {
                    targetHit.GetComponent<ResourceNode>().Harvest(Owner, damage, out float amount);
                    Debug.Log("Harvested " + amount +  " Resources!");
                }
            }

            nextAttackTimer += attackSpeed;
        }
    }

    public override void SecondaryFire()
    {
        // Tie in any secondary fire functionality here (ex. sniper zooming or burst fire)
        Debug.Log("Pickaxe Secondary Called!");
    }

    // -------------------------------------------------------
    // Getters & Setters
    // -------------------------------------------------------

    // -- Gun Variables
    override public string GetWeaponName() { return weaponName; }
    override public float GetBaseDamage() { return damage; }
    override public float GetRange() { return range; }
    override public float GetAttackSpeed() { return attackSpeed; }
    override public TriggerType GetTriggerType() { return TriggerType.SemiAuto; }


    
}
