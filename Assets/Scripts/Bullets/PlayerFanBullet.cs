using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFanBullet : FanBullet
{
    public override void Update()
    {
        Target = transform.GetComponentInChildren<PlayerTarget>().Target;
        GetSpreadPattern();
        //if (Input.GetKeyDown(KeyCode.Mouse1)&& CanShoot)
        //{
        //    StartCoroutine(Shoot());
        //}
    }

    public override void GetSpreadPattern()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, (Target.position - transform.position).normalized, out hit, 1000f);
        Difference = Quaternion.AngleAxis(90, Target.up) * hit.normal.normalized;
        if (Target == null)
            return;

        int j = 0;
        for (int i = -(int)BulletsPerVolley; i <= (int)BulletsPerVolley; i += 2)
        {
            PrelimTargets[j] = Target.position + (((i * Mathf.Abs(i)) / BulletsPerVolley) * BulletSpread * (transform.GetChild(0).rotation*Vector3.right));
            ArcTargets[j] = transform.position + (PrelimTargets[j] - transform.position).normalized * (hit.point - transform.position).magnitude;
            j++;
        }

    }
}
