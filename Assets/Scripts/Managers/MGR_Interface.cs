using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class MGR_Interface : MonoBehaviour
{
    [Header("Pregame")]
    public TMP_Text CountdownDisplay;

    [Header("Game UI")]
    public TMP_Text InventoryDisplay;
    public TMP_Text ResourcesDisplay;
    public TMP_Text TimeDisplay;
    public Image HealthBar;
    public TMP_Text HealthDisplay;

    private void Awake()
    {
        if (Manager.UI != null)
        {
            Debug.LogWarning("Replacing previous UI Manager");
        }

        Manager.UI = this;
    }

    public void SetCountdown(string text)
    {
        if (!CountdownDisplay.enabled) CountdownDisplay.enabled = true;
        CountdownDisplay.text = text;
    }

    public void HideCountdown()
    {
        CountdownDisplay.enabled = false;
    }

    public void UpdateResourcesDisplay(string text)
    {
        ResourcesDisplay.text = "Resources: " + text;
    }

    public void UpdateTimeDisplay(float time)
    {
        TimeDisplay.text = "Time: " + time.ToString("F2");
    }

    public void UpdateInventoryDisplay(string text)
    {
        InventoryDisplay.text = "Inventory: " + text;
    }

    public void UpdateHealthDisplay(Character c)
    {
        HealthDisplay.text = Mathf.Ceil(c.m_Health.Health.x) + " / " + c.m_Health.Health.y;
        HealthBar.fillAmount = c.m_Health.GetHpRatio();
    }
}
