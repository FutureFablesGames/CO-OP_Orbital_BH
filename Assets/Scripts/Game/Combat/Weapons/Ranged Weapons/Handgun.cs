using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : RangedWeapon
{
    // -------------------------------------------------------
    //  Weapon Variables
    // -------------------------------------------------------

    const string weaponName = "Handgun";
    const float damage = 20.0f;         // Base Damage = 15f
    const float range = 50.0f;          // How far will the bullet travel?  Can treat this as lifetime value
    const float reloadTime = 1.0f;      // How long does it take to reload your gun?
    const float rps = 1.0f;             // How fast does the gun shoot?  ie. 1 shot per second (60 shots per minute)
    const int clipSize = 8;             // How many bullets can be loaded in a clip before reloading?
    const int maxAmmo = 64;             // What's the maximum amount of ammo that can be carried for the gun?

    // -------------------------------------------------------
    // Overridable Functions
    // -------------------------------------------------------

    override public void PrimaryFire()
    {
        if (Owner.animationHandler.animator.GetBool("Attack"))
        {
            Debug.Log("Handgun Primary Called!");
            GameObject BulletPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/BulletPrefab");
            GameObject Bullet = Instantiate(BulletPrefab, shotOrigin.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody>().velocity = Owner.transform.forward * 5;
        }

        Owner.animationHandler.animator.SetBool("Attack", false);
    }

    override public void PrimaryCancel()
    {

    }

    override public void SecondaryFire()
    {
        Owner.b_IsAiming = true;
        Owner.animationHandler.currentState.Aiming = true;
    }

    override public void SecondaryCancel()
    {
        Owner.b_IsAiming = false;
        Owner.animationHandler.currentState.Aiming = false;
    }

    override public void OnEquip()
    {

    }

    override public void OnUnequip()
    {

    }

    // -------------------------------------------------------
    // Getters & Setters
    // -------------------------------------------------------

    // -- Gun Variables
    override public string GetWeaponName() { return weaponName; }
    override public WeaponType GetWeaponType() { return WeaponType.Ranged; }
    override public float GetBaseDamage() { return damage; }
    override public float GetRange() { return range; }
    override public float GetReloadTime() { return reloadTime; }
    override public float GetCurrentReloadTimer() { return reloadTimer; }
    override public float GetRPS() { return rps; }
    override public float GetRPM() { return rps * 60; }
    override public TriggerType GetTriggerType() { return TriggerType.SemiAuto; }

    // -- Ammo Reserve
    override public int GetClipSize() { return clipSize; }
    override public int GetCurrentClip() { return clipCount; }
    override public int GetMaxAmmo() { return maxAmmo; }
    override public int GetAmmoReserve() { return ammoCount; }
    override public bool Reloading() { return reloading; }
}
