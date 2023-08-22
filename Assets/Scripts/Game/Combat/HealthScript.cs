using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================
    
    public Character Owner;    

    [SerializeField] private Vector2 health;
    public Vector2 Health { get { return health; } private set { health = value; } }    

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================

    public void Initialize(Character _owner, Vector2 _health)
    {
        Owner = _owner;
        Health = _health;
    }

    public void Initialize(Character _owner, float currentHealth, float maximumHealth)
    {
        Owner = _owner;
        Health = new Vector2(currentHealth, maximumHealth);
    }

    public float DamageHealth(Damage damage)
    {
        // -- Calculate incoming damage 
        float result = Random.Range(damage.Amount.x, damage.Amount.y);
        int reduction = 0;
        switch (damage.Type)
        {
            case DamageType.Physical:
                reduction = Owner.m_Stats.Armor;
                break;
            case DamageType.Elemental:
                reduction = Owner.m_Stats.Resistance;
                break;
        }
        result = (result - reduction >= 0) ? (result - reduction) : 0;

        // -- If a shield is currently active, reduce damage from that first
        if (Owner.m_Status.Shielded)
        {
            // -- Loop through all shields and deal damage to them 
            for (int i = 0; i < Owner.m_Status.ActiveShields.Count; i++)
            {
                // -- If the damage exceeds the shield amount
                if (result - Owner.m_Status.ActiveShields[i].Amount >= 0)
                {
                    // -- Reduce the damage received and destroy the shield
                    result -= Owner.m_Status.ActiveShields[i].Amount;
                    Owner.m_Status.RemoveShield(Owner.m_Status.ActiveShields[i].Source.Settings.ID);
                }

                // -- Otherwise, negate the damage netirely and reduce the remaining value on the shield
                else
                {
                    Owner.m_Status.ActiveShields[i].Amount -= result;
                    result = 0;
                }
            }
        }

        // -- Handle Damage Calculation
        health.x -= result;

        // -- Update UI
        Manager.UI.UpdateHealthDisplay(Owner);

        // -- If health drops below zero, trigger death sequence
        if (health.x <= 0)
        {
            health.x = 0;
            Death();         
            return -1f;
        }        

        return result;
    }

    public void Heal(Damage heal)
    {
        // -- Calculate heal value
        float result = Random.Range(heal.Amount.x, heal.Amount.y);

        // -- Restore Health
        health.x += result;
        
        // -- Set the ceiling equal to maximum health
        if (health.x > health.y)
        {
            health.x = health.y;
        }

        // -- Update UI
        Manager.UI.UpdateHealthDisplay(Owner);
    }

    private void Death()
    {
        Debug.Log("Death Called");
        Destroy(this.gameObject);   // -- REPLACE WITH SPAWNING SYSTEM
    }

    public float GetHpRatio() { return Health.x / Health.y; }

    
}
