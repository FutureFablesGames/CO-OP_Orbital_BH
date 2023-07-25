using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float originalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int test = 0;
    private void OnTriggerEnter(Collider other)
    {
        GameObject _player = GameObject.Find("Player");
        if(_player != null)
        {
            originalSpeed = _player.GetComponent<PlayerController>().f_MoveSpeed;
            _player.GetComponent<PlayerController>().f_MoveSpeed = originalSpeed * 0.5f;
        }
        else
        {
            Debug.Log("Player gameobject not found");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject _player = GameObject.Find("Player");
        if (_player != null)
        {
            _player.GetComponent<PlayerController>().f_MoveSpeed = originalSpeed;
        }
        else
        {
            Debug.Log("Player gameobject not found");
        }
    }
}
