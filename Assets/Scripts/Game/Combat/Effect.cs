using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{ 
    Buff, 
    Debuff,
}

public class EffectSettings
{
    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    public int ID;  
    public string Name;
    public string Description;
    public Sprite icon;
    public EffectType Type;
    public Effect EffectRef;

    public Character Owner;
    public Character Target;
    public float Duration;
    public Coroutine CoroutineRef;
}

public class Effect
{
    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    public delegate IEnumerator EffectDelegate(EffectSettings settings);
    public delegate void EffectExpirationDelegate(EffectSettings settings);
    public EffectDelegate f_Function;
    public EffectExpirationDelegate f_Expiry;
    public EffectSettings Settings;
    public Coroutine CoroutineRef;

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================

    public Effect(EffectSettings settings)
    {
        Settings = settings;
    }

    public void Activate()
    {
        CoroutineRef = Manager.Coroutines.StartNewCoroutine(f_Function(Settings));
    }

    public void Stop()
    {
        f_Expiry.Invoke(Settings);
        Manager.Coroutines.StopCurrentCoroutine(CoroutineRef);
    }

    public void Refresh(float duration)
    {
        Settings.Duration = duration;
    }

}
