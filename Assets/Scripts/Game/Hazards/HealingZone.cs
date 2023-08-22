using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    public float HealthPerSecond = 5.0f;   
        
    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Character c = other.GetComponent<Character>();

            // -- Create the Damage Instance
            Damage newHeal = new Damage(null, new Vector2(HealthPerSecond * Time.deltaTime, HealthPerSecond * Time.deltaTime), DamageType.Elemental, DamageSubType.Nature);

            // -- Damage the Character
            c.m_Health.Heal(newHeal);
        }
    }
}
