using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float originalSpeed;
    public float currentTime = 0f;
    public float effectMaxTime = 2f;
    public bool effectActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(effectActive)
        {
            if(currentTime <= effectMaxTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                Debug.Log("Status Effect Removed");
                GameObject _player = GameObject.Find("Player");
                _player.GetComponent<PlayerController>().f_MoveSpeed = originalSpeed;
                currentTime = 0f;
                effectActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject _player = GameObject.Find("Player");
        if(_player != null)
        {
            Debug.Log("Status Effect Triggered");
            effectActive = true;
            originalSpeed = _player.GetComponent<PlayerController>().f_MoveSpeed;
            _player.GetComponent<PlayerController>().f_MoveSpeed = originalSpeed * 0.5f;
        }
        else
        {
            Debug.Log("Player gameobject not found");
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    GameObject _player = GameObject.Find("Player");
    //    if (_player != null)
    //    {
    //        _player.GetComponent<PlayerController>().f_MoveSpeed = originalSpeed;
    //    }
    //    else
    //    {
    //        Debug.Log("Player gameobject not found");
    //    }
    //}

    public IEnumerator HazardEffect()
    {
        GameObject _player = GameObject.Find("Player");
        if (currentTime <= effectMaxTime)
        {
            currentTime += Time.deltaTime;
            Debug.Log(currentTime);
            yield return null;
        }
        else
        {
            _player.GetComponent<PlayerController>().f_MoveSpeed = originalSpeed;
            Debug.Log("Status Effect Removed");
            currentTime = 0f;
            yield return null;
        }

    }
}
