using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeWeapons : MonoBehaviour
{
    
    public const int tier1Cost = 10;
    public const int tier2Cost = 20;
    public const int tier3Cost = 30;
     public TMP_Text meleeWeapon;
    public TMP_Text rangedWeapon;
    public TMP_Text errorText;
     void Start()
    {
        meleeWeapon.text = tier1Cost.ToString();
        rangedWeapon.text = tier1Cost.ToString();
        errorText.text = "";
        
    }
    /* public void ToggleUsability()
    {
        //turn interactability of this button on/off
        GetComponent<Button>().interactable = !GetComponent<Button>().interactable;
        //when switching weapon, toggle this
    } */
    public void IncreaseWeaponLevelMelee()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

       
        var weapon = player.GetComponent<PlayerController>().currentWeapon;
        var level = weapon.GetLevel();
        if (weapon.GetWeaponType() != "Melee")
        {
            Debug.Log("Can't upgrade right now");
            errorText.text = "Wrong weapon type equipped";
            //leave early
            return;
        }
       IncreaseLogic(player, level, weapon);    
       
    }
    public void IncreaseWeaponLevelRanged()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

       
        var weapon = player.GetComponent<PlayerController>().currentWeapon;
        var level = weapon.GetLevel();
        if (weapon.GetWeaponType() != "Ranged")
        {
            Debug.Log("Can't upgrade right now");
            errorText.text = "Wrong weapon type equipped";
            //leave early
            return;
        }
         IncreaseLogic(player, level, weapon);      
    }
    void IncreaseLogic(GameObject player, int level, Weapon weapon)
    {
        var pc = player.GetComponent<PlayerController>();
        
        switch (level)
        {
            case 1:
            {
                if (pc.CurrentResources >= tier1Cost) //if player can afford it
                {
                    weapon.IncreaseLevel();
                    Debug.Log("Increased tier");
                    pc.CurrentResources -= tier1Cost; //take away resources
                    if (weapon.GetWeaponType() == "Melee")
                        meleeWeapon.text = tier2Cost.ToString();
                    else
                        rangedWeapon.text = tier2Cost.ToString();
                    //reset error text so nothing shows
                    errorText.text = "";
                }
                else
                {
                    Debug.Log("Not enough funds!");
                    errorText.text = "Not enough funds!";
                }
            }
                break;
            case 2:
                if (pc.CurrentResources >= tier2Cost) //if player can afford it
                {
                    weapon.IncreaseLevel();
                    Debug.Log("Increased tier");
                    pc.CurrentResources -= tier2Cost;
                    if (weapon.GetWeaponType() == "Melee")
                        meleeWeapon.text = tier3Cost.ToString();
                    else
                        rangedWeapon.text = tier3Cost.ToString();
                    //reset error text so nothing shows
                    errorText.text = "";
                }
                else
                {
                    Debug.Log("Not enough funds!");
                    errorText.text = "Not enough funds!";
                }
                break;
            case 3:
                if (pc.CurrentResources >= tier3Cost) //if player can afford it
                {
                    weapon.IncreaseLevel();
                    Debug.Log("Increased tier");
                    pc.CurrentResources -= tier3Cost;
                    if (weapon.GetWeaponType() == "Melee")
                        meleeWeapon.text = "Max achieved";
                    else
                        rangedWeapon.text = "Max achieved";
                    //reset error text so nothing shows
                    errorText.text = "";
                }
                else
                {
                    Debug.Log("Not enough funds!");
                    errorText.text = "Not enough funds!";
                }
                break;
            default:
                Debug.Log("Highest tier achieved.");
                break;
        } 
        //update the UI
        // Update the Player Inventory Display
        Manager.UI.UpdateInventoryDisplay(pc.CurrentResources.ToString("F2"));       
    }
}
