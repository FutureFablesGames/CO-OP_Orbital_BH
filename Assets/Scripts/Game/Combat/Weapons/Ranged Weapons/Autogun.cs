using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autogun : RangedWeapon
{
    // -------------------------------------------------------
    //  Weapon Variables
    // -------------------------------------------------------

    const string weaponName = "Autogun";
    const float damage = 5.0f;         // Base Damage = 15f
    const float range = 50.0f;          // How far will the bullet travel?  Can treat this as lifetime value
    const float reloadTime = 2.0f;      // How long does it take to reload your gun?
    const float rps = 5.0f;             // How fast does the gun shoot?  ie. 1 shot per second (60 shots per minute)
    const int clipSize = 100;             // How many bullets can be loaded in a clip before reloading?
    const int maxAmmo = 500;             // What's the maximum amount of ammo that can be carried for the gun?

    WaitForSeconds rate = new WaitForSeconds(1 / rps);

    // -------------------------------------------------------
    // Overridable Functions
    // -------------------------------------------------------

    override public void PrimaryFire()
    {
        // Tie in any shot functionality here
        Debug.Log("Autogun Primary Called!");

        PrimaryRoutine = StartCoroutine(Shoot());
        
    }

    override public void PrimaryCancel()
    {
        if (PrimaryRoutine != null)
        {
            StopCoroutine(PrimaryRoutine);
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            GameObject BulletPrefab = Resources.Load<GameObject>("Prefabs/BulletPrefab");
            GameObject Bullet = Instantiate(BulletPrefab, shotOrigin.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody>().velocity = Owner.mesh.transform.forward * 5;
            Bullet.GetComponent<Bullet>().Target = Owner.mesh.transform.forward;

            yield return rate;
        }    
    }

    override public void SecondaryFire()
    {
        // Tie in any secondary fire functionality here (ex. sniper zooming or burst fire)
        Debug.Log("Autogun Secondary Called!");
    }

    override public void SecondaryCancel()
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
    override public TriggerType GetTriggerType() { return TriggerType.Automatic; }

    // -- Ammo Reserve
    override public int GetClipSize() { return clipSize; }
    override public int GetCurrentClip() { return clipCount; }
    override public int GetMaxAmmo() { return maxAmmo; }
    override public int GetAmmoReserve() { return ammoCount; }
    override public bool Reloading() { return reloading; }
}
