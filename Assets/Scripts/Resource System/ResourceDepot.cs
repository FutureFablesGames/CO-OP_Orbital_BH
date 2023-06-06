using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ResourceDepot : MonoBehaviour
{
    public float Amount = 0.0f;

    public TMP_Text ValueDisplay;

    public void Deposit(PlayerController pc, float amount)
    {
        Amount += amount;
        pc.CurrentResources -= amount;

        ValueDisplay.text = Amount.ToString("F2");
    }

    public void Withdraw(PlayerController pc, float amount)
    {
        Amount -= amount;
        pc.CurrentResources += amount;

        ValueDisplay.text = Amount.ToString("F2");
    }
}
