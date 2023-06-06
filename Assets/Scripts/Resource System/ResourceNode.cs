using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : Interactable
{
    public float Amount;

    private void Awake()
    {
        Amount = Random.Range(50f, 150f);
    }

    public override void Interact(PlayerController pc)
    {
        // Determine how much space is available
        float available = pc.MaxResources - pc.CurrentResources;
        
        // Calculate the amount of resources being transfered
        float result;
        if (Amount <= available) {
            result = Amount;
            Amount -= result;
        }
        else {
            result = available;
            Amount -= available;
        }

        // Give resources to the player
        pc.CurrentResources += result;
        Amount -= result;
       
        // Check if there are resources left
        if (Amount <= 0)
        {
            Destroy(this.gameObject);
        }
    }    
}
