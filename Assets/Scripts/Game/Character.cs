using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    public string m_CharacterName;
    public PlayerStatistics m_Stats;
    public Status m_Status = new Status();
    public Dictionary<int, Effect> m_CurrentEffects = new Dictionary<int, Effect>();
    [HideInInspector] public HealthScript m_Health;

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================
    
    public void Initialize()
    {
        m_Stats = PlayerStatistics.Default();
        m_Status.Dead = false;

        m_Health = GetComponent<HealthScript>();
        m_Health.Initialize(this, m_Stats.Health, m_Stats.Health);
    }

    public void Tick()
    {
        m_Status.Tick();
    }

    public void AddEffect(Effect effect)
    {
        m_CurrentEffects.Add(effect.Settings.ID, effect);
    }

    public bool HasEffect(Effect effect, out Effect result)
    {
        bool isAlreadyActive = false;
        result = null;
        foreach (var pair in m_CurrentEffects)
        {
            if (pair.Key == effect.Settings.ID)
            {
                isAlreadyActive = true;
                result = pair.Value;
            }
        }

        return isAlreadyActive;
    }

    public void RemoveEffect(Effect effect)
    {
        HasEffect(effect, out Effect result);
        {
            result.Stop();
            m_CurrentEffects.Remove(result.Settings.ID);
        }
        
    }
}
