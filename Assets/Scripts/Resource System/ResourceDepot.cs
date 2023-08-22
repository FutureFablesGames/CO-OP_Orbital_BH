using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ResourceDepot : Interactable
{
    public float Amount = 0.0f;


    public TMP_Text ValueDisplay;

    public override void Interact(PlayerController pc)
    {
        // If the player is the assigned player, make a deposit
        Deposit(pc, pc.player.CurrentResources);

        // If the player is not the assigned player, make a withdraw / steal
        Withdraw(pc, pc.player.CurrentResources);
    }

    private void Deposit(PlayerController pc, float amount)
    {
        Amount += amount;
        pc.player.CurrentResources -= amount;

        Manager.UI.UpdateResourcesDisplay(Amount.ToString("F2"));
        Manager.UI.UpdateInventoryDisplay(pc.player.CurrentResources.ToString("F2"));
        ValueDisplay.text = Amount.ToString("F2");
    }

    private void Withdraw(PlayerController pc, float amount)
    {
        Amount -= amount;
        pc.player.CurrentResources += amount;

        Manager.UI.UpdateResourcesDisplay(Amount.ToString("F2"));
        Manager.UI.UpdateInventoryDisplay(pc.player.CurrentResources.ToString("F2"));
        ValueDisplay.text = Amount.ToString("F2");
    }
}
