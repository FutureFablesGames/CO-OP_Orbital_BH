using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public float Amount;

    private void Awake()
    {
        Amount = Random.Range(50f, 150f);
    }

    public void Harvest(PlayerController pc, float harvestPower, out float amount)
    {        
        // +/- 10% variability in harvesting
        amount = Random.Range(harvestPower * 0.9f, harvestPower * 1.1f);

        // Give resources to the player
        pc.CurrentResources += amount;
        Amount -= amount;

        // Update the Player Inventory Display
        Manager.UI.UpdateInventoryDisplay(pc.CurrentResources.ToString("F2"));

        // Check if there are resources left
        if (Amount <= 0)
        {
            Destroy(this.gameObject, 0.01f);            
        }
    }   
}
