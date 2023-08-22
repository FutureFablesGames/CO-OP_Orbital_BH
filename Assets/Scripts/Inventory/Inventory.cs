using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // ================================================
    // COMPONENTS / COMPONENTS / COMPONENTS / COMPONEN
    // ================================================

    public PlayerCharacter Owner;
    
    [Header("Weapons")]
    public Weapon CurrentWeapon;    
    [SerializeField] Weapon RangedWeapon;
    [SerializeField] Weapon MeleeWeapon;

    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    // -- Resources
    [Header("Resources")]
    public float CurrentResources;
    //public float MaxResources = 500.0f;
        
    // -- Items
    [Header("Items")]
    public List<Item> currentItems; //what item are we currently holding, freely swappable
    public int maxSlots;
    public int currentSlots;

    // ================================================
    // MONOBEHAVIOUR / MONOBEHAVIOUR/ MONOBEHAVIOUR /    
    // ================================================

    private void OnEnable()
    {
        if (Manager.Input != null)
        {
            Manager.Input.Item1Callback += UseItem1;
            Manager.Input.SwapWeaponCallback += SwapWeapon;
        }

    }

    private void OnDisable()
    {
        if (Manager.Input != null)
        {
            Manager.Input.Item1Callback -= UseItem1;
            Manager.Input.SwapWeaponCallback -= SwapWeapon;
        }
    }

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================

    public void SwapWeapon()
    {
        // Swap weapons
        Weapon next = (CurrentWeapon == MeleeWeapon) ? RangedWeapon : MeleeWeapon;
        Unequip(CurrentWeapon);
        Equip(next);               
    }

    public void Equip(Weapon w)
    {
        switch (w.GetWeaponType())
        {
            case WeaponType.Melee:
                // Check if it's a new weapon we're equipping
                if (w != MeleeWeapon)
                {
                    Destroy(MeleeWeapon);               // -- Destroy the current weapon to remove it from the scene
                    MeleeWeapon = w;                    // -- Set our new melee weapon reference
                    MeleeWeapon.Owner = Owner.PC;       // -- Make sure to initialize the owner of the weapon         
                }
                
                break;
            case WeaponType.Ranged:
                // Check if it's a new weapon we're equipping               
                if (w != RangedWeapon)
                {
                    Destroy(RangedWeapon);              // -- Destroy the current weapon to remove it from the scene
                    RangedWeapon = w;                   // -- Set our new ranged weapon reference
                    RangedWeapon.Owner = Owner.PC;      // -- Make sure to initialize the owner of the weapon        
                }                
                break;
        }
        
        CurrentWeapon = w;                          // -- Set our current weapon to our newly equipped weapon
        CurrentWeapon.gameObject.SetActive(true);   // -- And don't forget to activate it on in case it's hidden.
        Owner.PC.animationHandler.animator.SetInteger("WeaponStance", (CurrentWeapon.GetWeaponType() == WeaponType.Melee) ? 0 : 1);
    }

    public void Unequip(Weapon w)
    {
        CurrentWeapon.gameObject.SetActive(false);
        CurrentWeapon = null;        
    }

    public void AddItem(Item newItem)
    {
        // currentItem = newItem;

        currentItems.Add(newItem);
        //currentAmount = newItem.usableCount;
    }

    

    // Update is called once per frame
    private void UseItem1()
    {
        //Debug.Log("Current items: " + currentItems.Count);

        //for using the item
        //go through current items?
        //for (int i=0; i < currentItems.Count; i++)
        if (currentItems.Count >= 1)
        {
            //for now only the 1st item
            //check that we're pressing the right key and that we have enough of that item
            if (currentItems[0].uses < currentItems[0].usableCount)
            {
                currentItems[0].Activate(gameObject); //activate the effect when the key is pressed

                if (currentItems[0].uses >= currentItems[0].usableCount) //we used it up
                {
                    //remove the item
                    Debug.Log("Used up the item");
                    currentSlots -= 1;

                    //reset uses for next item
                    currentItems[0].uses = 0;

                    currentItems.Remove(currentItems[0]);



                }


            }
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            //can we pick it up?
            if (currentSlots < maxSlots)
            {
                //collided with item
                var item = collision.gameObject.GetComponent<ItemHolder>().item;

                AddItem(item);

                Debug.Log("Picked up " + item.name);
                //remove item
                GameObject.Destroy(collision.gameObject);

                //increase inv count
                currentSlots += 1;
            }
            else
            {
                Debug.Log("Max inv reached");
            }

        }
    }
}
