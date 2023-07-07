using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this exists to give the player something to shoot at 
public class PlayerTarget : MonoBehaviour
{
    [HideInInspector] public Transform Target;
    private void Awake()
    {
        Target = this.transform;
    }
}
