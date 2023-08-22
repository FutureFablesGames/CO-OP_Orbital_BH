using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR_Coroutines : MonoBehaviour
{
    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    public List<Coroutine> ActiveCoroutines = new List<Coroutine>();

    // ================================================
    // MONOBEHAVIOR / MONOBEHAVIOR / MONOBEHAVIOR / MO
    // ================================================

    private void Awake()
    {
        if (Manager.Coroutines != null) Destroy(this.gameObject);
        Manager.Coroutines = this;
    }

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================

    public Coroutine StartNewCoroutine(IEnumerator routine)
    {
        Coroutine result = StartCoroutine(routine);
        ActiveCoroutines.Add(result);
        return result;
    }

    public Coroutine GetCoroutine(Coroutine cref, out Coroutine result)
    {
        if (ActiveCoroutines.Contains(cref)) {
            result = cref;
            return cref;
        } else {
            result = null;
            return null;
        }
    }

    public void StopCurrentCoroutine(Coroutine cref)
    {
        GetCoroutine(cref, out Coroutine result);
        {
            if (result != null)
            {
                StopCoroutine(result);
                ActiveCoroutines.Remove(result);
            }
        }
    }
}
