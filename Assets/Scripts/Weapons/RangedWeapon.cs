using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    // -------------------------------------------------------
    // Ranged Weapon Variables
    // -------------------------------------------------------

    protected int clipCount;        // Number of bullets left in the current clip
    protected int ammoCount;        // Number of bullets left in the ammo reserve
    protected bool reloading;       // Is the gun currently reloading?
    protected float reloadTimer;    // How long is the reload going to take?

    protected WaitForSeconds shotDelay = new WaitForSeconds(0.07f);
    protected float nextShotTimer;

    // -------------------------------------------------------
    // Initialization
    // -------------------------------------------------------

    private void Start()
    {
        clipCount = GetClipSize();
        ammoCount = GetAmmoReserve();
        reloading = false;
        reloadTimer = 0f;
    }

    // -------------------------------------------------------
    // Methodology
    // -------------------------------------------------------

    private void Update()
    {
        // Update Shot Timer
        if (nextShotTimer > 0)
        {
            nextShotTimer -= Time.deltaTime;
        }
    }

    private void Reload()
    {
        StartCoroutine(I_Reload());
    }

    private IEnumerator I_Reload()
    {
        reloading = true;
        reloadTimer = GetReloadTime();

        yield return new WaitForSeconds(GetReloadTime());

        int refill = GetClipSize() - clipCount;

        if (ammoCount - refill >= 0)
        {
            clipCount = GetClipSize();
            ammoCount -= refill;
        }

        else if (ammoCount - refill < 0)
        {
            clipCount = ammoCount;
            ammoCount = 0;
        }

        reloading = false;
    }
    

    // -------------------------------------------------------
    // Getters & Setters
    // -------------------------------------------------------

    override public string GetWeaponType(){return "Ranged";}

    // -- Gun Variables    
    public abstract float GetReloadTime();
    public abstract float GetCurrentReloadTimer();
    public abstract float GetRPM();
    public abstract float GetRPS();    

    // -- Ammo Reserve
    public abstract int GetClipSize();
    public abstract int GetCurrentClip();
    public abstract int GetMaxAmmo();
    public abstract int GetAmmoReserve();    
    public abstract bool Reloading();

}
