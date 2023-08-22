using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{ 
    Physical, Elemental, True
}

public enum DamageSubType
{
    Ballistic,
    Fire,
    Electric,
    Nature,    
    Water,
}


public struct Damage 
{
    public Character Owner;
    public Vector2 Amount;
    public DamageType Type;
    public DamageSubType SubType;

    public void SetOwner(Character owner)
    {
        Owner = owner;
    }

    public Damage(Character owner, Vector2 amount, DamageType type, DamageSubType subType)
    {
        Owner = owner;
        Amount = amount;
        Type = type;
        SubType = subType;
    }

    public Damage(Damage reference)
    {
        Owner = reference.Owner;
        Amount = reference.Amount;
        Type = reference.Type;
        SubType = reference.SubType;
    }
}
