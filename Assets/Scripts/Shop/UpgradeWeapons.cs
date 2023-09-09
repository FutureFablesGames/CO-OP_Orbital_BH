using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWeapons : MonoBehaviour
{
    public void IncreaseWeaponLevel()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

       
        var weapon = player.GetComponent<PlayerController>().currentWeapon;
        if (weapon.GetLevel() <3) //3 is the highest tier
        {
            weapon.IncreaseLevel();
            Debug.Log("Increased tier");
        }
        else
            Debug.Log("Highest tier achieved.");
        
       
    }
}
