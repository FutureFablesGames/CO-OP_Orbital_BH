using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    //character will hold this script
    //public Item currentItem; 

    public List<Item> currentItems; //what item are we currently holding, freely swappable

    //public int currentAmount;

    public int maxSlots;
    public int currentSlots;
 
   public void AddItem(Item newItem)
   {
       // currentItem = newItem;

        currentItems.Add(newItem);
        //currentAmount = newItem.usableCount;
   }
    
    // Start is called before the first frame update
    void Start()
    {
        //currentAmount = currentItem.usableCount;
    }

    // Update is called once per frame
    void Update()
    {
        //for using the item
        //go through current items?
        for (int i=0; i < currentItems.Count; i++)
        {   
            
            //check that we're pressing the right key and that we have enough of that item
            if (Input.GetKeyDown(currentItems[i].key) && currentItems[i].uses < currentItems[i].usableCount)
            {
                currentItems[i].Activate(gameObject); //activate the effect when the key is pressed

                if (currentItems[i].uses >= currentItems[i].usableCount) //we used it up
                {
                    //remove the item
                    Debug.Log("Used up the item");
                    currentSlots -=1;
                    currentItems.Remove(currentItems[i]);
                    

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

                Debug.Log("Picked up "+ item.name);
                //remove item
                GameObject.Destroy(collision.gameObject);

                //increase inv count
                currentSlots+=1;
            }
            else
            {
                Debug.Log("Max inv reached");
            }

        }
    }
}
