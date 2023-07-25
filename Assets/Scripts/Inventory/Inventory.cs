using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void OnEnable()
    {
        if (Manager.Input != null)
        {
            Manager.Input.Item1Callback += UseItem1;

        }

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
