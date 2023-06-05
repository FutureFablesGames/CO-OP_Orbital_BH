using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class FanBullet : BulletEmitter
    {
    [Range(0, 2)] public float TimeBetweenVolleys = 1;
    [HideInInspector] public WaitForSeconds VollyTimer = new WaitForSeconds(1);
    [Range(0, 5)] public int NumberOfVolleys = 1;
    [VTRangeStep(2f, 10f, 2f)] public float BulletsPerVolley;
    [Range(0,1f)] public float BulletSpread = 1;
    public List<Vector3> Targets,OGTargets;
    private Vector3 Difference;
    public override void Awake()
    {
        base.Awake();
        for (int i = 0; i <= (int)BulletsPerVolley; i++)
        {
            Targets.Add(Vector3.zero);
            OGTargets.Add(Vector3.zero);
        }
        VollyTimer = new WaitForSeconds(TimeBetweenVolleys);
    }
    public void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position, SightRange);
      //
       if (Target == null)
           return;
        //  
        RaycastHit hit;
        Gizmos.DrawLine(transform.position, Target.position);
        Physics.Raycast(transform.position, (Target.position- transform.position ).normalized, out hit, 1000f,WhatIsPlayer);
        Gizmos.DrawLine(transform.position, hit.point);

        Difference = Quaternion.AngleAxis(90, Target.up)* hit.normal.normalized;
       
        int j = 0;
     
       for (int i = -(int)BulletsPerVolley; i <= (int)BulletsPerVolley; i += 2)
        {
            Gizmos.color = Color.yellow;
            OGTargets[j] = Target.position + (((i*Mathf.Abs(i)) / BulletsPerVolley) * BulletSpread * Difference);
            Targets[j] = transform.position + (OGTargets[j]-transform.position).normalized * (hit.point - transform.position).magnitude;


            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(OGTargets[j], 0.25f);
            Gizmos.DrawLine(OGTargets[j], Targets[j]);
            Gizmos.DrawLine(OGTargets[0], OGTargets[OGTargets.Count - 1]);

            //to show actual trajectory
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(Targets[j], 0.25f);
            Gizmos.DrawLine(Targets[j], transform.position);
            j++;
        }
        Gizmos.DrawLine(hit.point, hit.point + Difference);
       ////used j bc im too lazy to do the math
       //int j = 0;
       //for (int i = -(int)BulletsPerVolley; i <= (int)BulletsPerVolley; i += 2)
       //{
       //    //to show math for trajectory calculations
       //    Gizmos.color = Color.yellow;
       //    Gizmos.DrawSphere(OGTargets[j], 0.25f);
       //    Gizmos.DrawLine(OGTargets[j], Targets[j]);
       //    Gizmos.DrawLine(OGTargets[0], OGTargets[OGTargets.Count - 1]);
       //
       //    //to show actual trajectory
       //    Gizmos.color = Color.green;
       //    Gizmos.DrawSphere(Targets[j], 0.25f);we
       //    Gizmos.DrawLine(Targets[j], transform.position);
       //    j++;
       //}         

    }
    public override void Update()
    {
        base.Update();
    }
    public override void GetSpreadPattern()
    {
     //
     // if (Target != null)
     // {
     //     //rotate 90 to get perpendicular vec
     //     Difference = transform.position - Target.position;
     //     Difference = Quaternion.Euler(0, 0, -90) * Difference;
     //
     //     //used j bc im too lazy to do the math lol
     //     int j = 0;
     //     for (int i = -(int)BulletsPerVolley; i <= (int)BulletsPerVolley; i += 2)
     //     {
     //         OGTargets[j] = Target.position +  (((float)(i * (Mathf.Abs(i))) / BulletsPerVolley) * BulletSpread * Difference.normalized);
     //         Targets[j] = (OGTargets[j] - transform.position).normalized * (Target.position - transform.position).magnitude; ;
     //         Targets[j] += transform.position;
     //         j++;
     //     }
     // }
    }
    public override IEnumerator Shoot()
    {

      //  CanShoot = false;
      //
      //  int i = 0;
      //  while (i < NumberOfVolleys)
      //  {
      //     for (int j = 0; j < BulletsPerVolley; j++)
      //      {
      //          GameObject Bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
      //          Bullet.GetComponent<Rigidbody>().velocity= (( Targets[j]- transform.position ).normalized*5);
      //      }
      //
            yield return VollyTimer;
      //      i++;
      //  }
      //  StartCoroutine(ResetAttack());
    }
    private IEnumerator ResetAttack()
    {
        yield return AttackTimer;
        CanShoot = true;
    }
}

