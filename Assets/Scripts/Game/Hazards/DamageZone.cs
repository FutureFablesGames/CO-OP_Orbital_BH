using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    public float DamagePerSecond = 5.0f;

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Character c = other.GetComponent<Character>();
            
            // -- Create the Damage Instance
            Damage newDamage = new Damage(null, new Vector2(DamagePerSecond * Time.deltaTime, DamagePerSecond * Time.deltaTime), DamageType.Elemental, DamageSubType.Nature);            

            // -- Damage the Character
            c.m_Health.DamageHealth(newDamage);
        }
    }
}
