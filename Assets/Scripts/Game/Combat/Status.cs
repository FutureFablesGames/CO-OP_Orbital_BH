using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StatusEffect
{ 
    public Effect Source;
    public float Duration;
}

[System.Serializable]
public class ShieldEffect
{
    public Effect Source;
    public float Duration;
    public float Amount;
}

[System.Serializable]
public class Status
{
    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    // Health-Related Status Effects
    public bool Dead { get; set; }
    public bool Immortal { get; set; }

    // Movement-Related Status Effects
    public bool Slowed { get; private set; }
    public bool Stunned { get; private set; }
    public bool Rooted { get; private set; }
    public bool Frozen { get; private set; }

    // Combat Status Effects 
    public bool Silenced { get; private set; }


    // ================================================
    // SHIELDS / SHIELDS / SHIELDS / SHIELDS / SHIELDS 
    // ================================================

    public List<ShieldEffect> ActiveShields = new List<ShieldEffect>();

    public bool Shielded {
        get { return ActiveShields.Count > 0; }
    }

    public void AddShield(ShieldEffect effect)
    {
        ActiveShields.Add(effect);
    }

    public void RemoveShield(int ID)
    {
        foreach (ShieldEffect e in ActiveShields)
        {
            if (e.Source.Settings.ID == ID)
            {
                e.Source.Settings.Target.RemoveEffect(e.Source);
                ActiveShields.Remove(e);
                break;
            }
        }
    }

    // ================================================
    // BURNS / BURNS / BURNS / BURNS / BURNS / BURNS / 
    // ================================================

    public List<StatusEffect> ActiveBurns = new List<StatusEffect>();
    public bool Burning {
        get { return ActiveBurns.Count > 0; }
    }

    public void AddBurn(StatusEffect effect)
    {
        ActiveBurns.Add(effect);
    }

    public void RemoveBurn(int ID)
    {
        foreach (StatusEffect e in ActiveBurns)
        {
            if (e.Source.Settings.ID == ID)
            {
                e.Source.Settings.Target.RemoveEffect(e.Source);
                ActiveBurns.Remove(e);
                break;
            }
        }
    }

    // ================================================
    // POISONS / POISONS / POISONS / POISONS / POISONS
    // ================================================

    public List<StatusEffect> ActivePoisons = new List<StatusEffect>();
    public bool Poisoned {
        get { return ActivePoisons.Count > 0; }
    }

    public void AddPoison(StatusEffect effect)
    {
        ActivePoisons.Add(effect);
    }

    public void RemovePoison(int ID)
    {
        foreach (StatusEffect e in ActivePoisons)
        {
            if (e.Source.Settings.ID == ID)
            {
                e.Source.Settings.Target.RemoveEffect(e.Source);
                ActivePoisons.Remove(e);
                break;
            }
        }
    }

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================

    public void Tick()
    {
        // Update Shield Timers
        foreach (ShieldEffect shield in ActiveShields)
        {
            shield.Duration -= Time.deltaTime;
        }

        // Update Burn Timers
        foreach (StatusEffect burn in ActiveBurns)
        {
            burn.Duration -= Time.deltaTime;
        }

        // Update Poison Timers
        foreach (StatusEffect poison in ActivePoisons)
        {
            poison.Duration -= Time.deltaTime;
        }
    }

}
