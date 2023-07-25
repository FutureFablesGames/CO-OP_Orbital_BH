using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealthPickup : Item
{
    public override void Activate(GameObject parent)
    {
        //we've used it now
        uses += 1;
        Debug.Log("Currently have " + (usableCount - uses) + " uses left");
        //increase player health here
    }

}
