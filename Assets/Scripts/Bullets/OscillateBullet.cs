using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OscillateBullet : BulletEmitter
{
    [HideInInspector] public List<Vector3> ArcTargets, PrelimTargets;
    private int VolleyDirection = 1;
    [HideInInspector] public WaitForSeconds ShotTimer = new WaitForSeconds(1);
    private Vector3 Difference;
    [Range(0, 2)] public float TimeBetweenShots = 1;
    
    [Header("BulletVolley Specifics" + "\n")]
    public bool UseMiddleShot, Alternate;
    
    
   
    public override void Awake()
    {
        base.Awake();
        for (int i = 0; i <= (int)BulletsPerVolley; i++)
        {
            ArcTargets.Add(Vector3.zero);
            PrelimTargets.Add(Vector3.zero);
        }
        VollyTimer = new WaitForSeconds(TimeBetweenVolleys);
        ShotTimer = new WaitForSeconds(TimeBetweenShots);
    }
    public void OnDrawGizmos()  
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SightRange);

        if (Target == null)
            return;
        Gizmos.DrawLine(transform.position, Target.position);

        for (int i = 0; i <= (int)BulletsPerVolley; i++)
        {
            if (i == (int)BulletsPerVolley / 2 && !UseMiddleShot)
                i++;
              //prelims
              Gizmos.color = Color.red;
              Gizmos.DrawSphere(PrelimTargets[i], 0.25f);
              Gizmos.DrawLine(PrelimTargets[i], ArcTargets[i]);
              Gizmos.DrawLine(PrelimTargets[0], PrelimTargets[PrelimTargets.Count - 1]);

              //to show actual trajectory
              Gizmos.color = Color.yellow;
              Gizmos.DrawSphere(ArcTargets[i], 0.25f);
              Gizmos.DrawLine(ArcTargets[i], transform.position);
              //if we want to arc across the surface
              Gizmos.DrawLine(ArcTargets[i], ArcTargets[i] + Target.rotation * Vector3.down * 5);

           

        }
        //   Gizmos.DrawLine(hit.point, hit.point + Difference);      
     
    }
    public override IEnumerator Shoot()
    {
        CanShoot = false;

        int i = 0;
        while (i < NumberOfVolleys)
        {
            int num = (int)Math.Clamp((int)BulletsPerVolley - ((int)BulletsPerVolley * VolleyDirection), 0, 1) * (int)BulletsPerVolley;
            for (int j = 0; j <= BulletsPerVolley; j++)
            { 
                //if half way through volley and not using middle shot, skip the middle shot
                if (j == (int)BulletsPerVolley / 2 && !UseMiddleShot)
                    j ++;
                //make da bullet
                GameObject Bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                //init bullet values
                //(ArcTargets[num+ VolleyDirection*j] is used to go one way or the other 
                Bullet.GetComponent<Rigidbody>().velocity = ((ArcTargets[num+ VolleyDirection*j] - transform.position).normalized * 5);
                Bullet.GetComponent<Bullet>().Target = ((ArcTargets[num + VolleyDirection * j] - transform.position).normalized);
                yield return ShotTimer;
            }
            //basically make it go back and forth if the alternate bool is active
            VolleyDirection -= Convert.ToInt32(Alternate) * 2 * (VolleyDirection / Mathf.Abs(VolleyDirection));
            yield return VollyTimer;
            i++;
        }
        StartCoroutine(ResetAttack());
    }
    private IEnumerator ResetAttack()
    {
        yield return AttackTimer;
        CanShoot = true;
    }
    public override void GetSpreadPattern()
    {

        RaycastHit hit;
        Physics.Raycast(transform.position, (Target.position - transform.position).normalized, out hit, 1000f, WhatIsPlayer);
        Difference = Quaternion.AngleAxis(90, Target.up) * hit.normal.normalized;
        if (Target == null)
            return;

        int j = 0;
        for (int i = -(int)BulletsPerVolley; i <= (int)BulletsPerVolley; i += 2)
        {
            PrelimTargets[j] = Target.position + (((i * Mathf.Abs(i)) / BulletsPerVolley) * BulletSpread * Difference);
            ArcTargets[j] = transform.position + (PrelimTargets[j] - transform.position).normalized * (hit.point - transform.position).magnitude;
            j++;
        }
    }
}
