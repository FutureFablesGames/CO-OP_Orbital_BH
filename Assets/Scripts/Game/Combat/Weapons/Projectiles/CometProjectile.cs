using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometProjectile : MonoBehaviour
{
    private void Update()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
    }
}
