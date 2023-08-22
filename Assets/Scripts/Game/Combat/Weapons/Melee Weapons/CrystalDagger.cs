using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDagger : MeleeWeapon
{
    // -------------------------------------------------------
    //  Weapon Variables
    // -------------------------------------------------------

    const string weaponName = "Crystal Dagger";
    const float damage = 7.0f;         // Base Damage = 15f
    const float harvestPower = 5.0f;   // How much harvest power does the weapon have.  Unique to weapons that can harvest resource nodes.
    const float range = 1.5f;           // How far will the weapon reach?
    const float attackSpeed = 2.0f;     // How fast does the weapon attack?  ie. 1 swing per second (60 swings per minute) Should be relative to the animation  
    const float knockback = 2.0f;

    // -------------------------------------------------------
    // Overridable Functions
    // -------------------------------------------------------

    public override void PrimaryFire()
    {
        if (nextAttackTimer <= 0)
        {
            // Tie in any shot functionality here
            Debug.Log("Dagger Primary Called!");

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
                    Debug.Log("Harvested " + amount + " Resources!");
                }

                // If it's a player, damage it
                if (targetHit.tag == "Player")
                {
                    // Deal Damage
                    Damage newDamage = new Damage(Owner.player, new Vector2(damage * 0.9f, damage * 1.1f), DamageType.Physical, DamageSubType.Ballistic);
                    Character t_character = targetHit.GetComponent<Character>();
                    t_character.m_Health.DamageHealth(newDamage);
                    Debug.Log("Damaged " + targetHit.name + ": Remaining Health = " + t_character.m_Health.Health.x);

                    // Apply knockback
                    Rigidbody t_rigid = targetHit.GetComponent<Rigidbody>();
                    Vector3 force_applied = (targetHit.transform.position - transform.position).normalized * knockback;
                    Vector3 force_upwards = targetHit.transform.up * knockback;
                    t_rigid.AddForceAtPosition(force_applied + force_upwards, hit.point, ForceMode.Impulse);
                }
            }

            nextAttackTimer += attackSpeed;
        }
    }

    override public void PrimaryCancel()
    {
        
    }

    public override void SecondaryFire()
    {
        // Tie in any secondary fire functionality here (ex. sniper zooming or burst fire)
        Debug.Log("Dagger Secondary Called!");
    }

    override public void SecondaryCancel()
    {
        
    }

    // -------------------------------------------------------
    // Getters & Setters
    // -------------------------------------------------------

    // -- Gun Variables
    override public string GetWeaponName() { return weaponName; }
    override public WeaponType GetWeaponType() { return WeaponType.Melee; }
    override public float GetBaseDamage() { return damage; }
    override public float GetRange() { return range; }
    override public float GetAttackSpeed() { return attackSpeed; }
    override public TriggerType GetTriggerType() { return TriggerType.SemiAuto; }
}
