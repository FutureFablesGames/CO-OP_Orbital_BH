using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : Interactable
{

    public GameObject shopUI;
    public override void Interact(PlayerController pc)
    {
        //turn it on/off
        shopUI.SetActive(!shopUI.activeSelf);
        //turn cursor on/off
         if (Cursor.lockState == CursorLockMode.Locked)
        {
             Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }  
        else
        {
             Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
        }
        Debug.Log("Shop interact");
    }
}
