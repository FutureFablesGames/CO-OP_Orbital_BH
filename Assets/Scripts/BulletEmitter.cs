using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfEmitter
{
    Oscillating, Beam, Fan, Circle, AlternatingOscillation, 
}

public abstract class BulletEmitter : MonoBehaviour
{
    public LayerMask WhatIsPlayer,WhatIsGround;
    public GameObject BulletPrefab;
    [Range(0, 15)] public float SightRange = 1;
     public Transform Target;
    [Range(0, 15)] public float TimeBetweenAttacks = 1;
    [HideInInspector] public WaitForSeconds AttackTimer = new WaitForSeconds(1);
    [HideInInspector] public bool CanShoot = true;
    public abstract IEnumerator Shoot();
    public abstract void GetSpreadPattern();
    public virtual void Awake()
    {
        AttackTimer = new WaitForSeconds(TimeBetweenAttacks);
    }
    public virtual void Update()
    {
        if (Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer))
        {
            Target = CalculateTarget();
            GetSpreadPattern();
        }
        else Target = null;

        if (CanShoot&&Target!=null)
            StartCoroutine(Shoot());

    }
    public Transform CalculateTarget()
    {
        var AllPlayers = FindObjectsOfType(typeof(PlayerController));

        //init values for loop
        Transform ClosestPlayer = null;
        float MinimimDistance = Mathf.Infinity;

        //find the closest transform with the player controller component
        //this will be the closest player
        foreach (PlayerController Player in AllPlayers)
        {
            float Distance = Vector3.Distance(Player.transform.position, transform.position);
            if (Distance < MinimimDistance)
            {
                ClosestPlayer = Player.transform;
                MinimimDistance = Distance;
            }
        }
        return ClosestPlayer;

    }

}
