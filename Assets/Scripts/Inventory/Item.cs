using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : ScriptableObject
{
    //an item needs a name, and an effect
    public new string name;

    //some properties: number
    public int usableCount;
    public int uses;

    //what key to press to use it
    public KeyCode key;

    //what happens when activating this effect
    public virtual void Activate(GameObject parent)
    {
        
    }
  
    
}
