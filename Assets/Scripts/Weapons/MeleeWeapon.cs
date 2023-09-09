using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Melee weapons should operate using a hitbox that detects if something is within range of it. 
 * Maybe we can also treat them as hitscan weapons which check to see if the target was within range of the player.
 */

public abstract class MeleeWeapon : Weapon
{
    // -------------------------------------------------------
    // Melee Weapon Variables
    // -------------------------------------------------------

    protected WaitForSeconds attackDelay = new WaitForSeconds(0.07f);
    protected float nextAttackTimer;

    // -------------------------------------------------------
    // Methodology
    // -------------------------------------------------------

    private void Update()
    {
        // Update Shot Timer
        if (nextAttackTimer > 0)
        {
            nextAttackTimer -= Time.deltaTime;
        }
    }

    // -------------------------------------------------------
    // Getters & Setters
    // -------------------------------------------------------

    // -- Melee Variables
    public abstract float GetAttackSpeed();
    override public string GetWeaponType(){return "Melee";}


}
