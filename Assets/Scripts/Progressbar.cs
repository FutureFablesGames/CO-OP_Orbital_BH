using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Progressbar : MonoBehaviour
{
    [Header ("Radial Timers")]
    [SerializeField] private float indicatorTimer = 0f;
    [SerializeField] private float maxIndicatorTimer = 1.0f;

    [Header ("UI Indicator")]
    [SerializeField] private Image radialIndicatorUI = null;

    [Header ("Key Codes")]
    [SerializeField] private KeyCode selectKey = KeyCode.Mouse0;

    [Header ("Unity Event")]
    [SerializeField] private UnityEvent myEvent = null;

    private bool shouldUpdate = false;

    private void Update()
    {
        if(Input.GetKey(selectKey))
        {
            UpdateRadialPosition();
            shouldUpdate = false;
            indicatorTimer += Time.deltaTime;
            radialIndicatorUI.enabled = true;
            radialIndicatorUI.fillAmount = indicatorTimer;

            if(indicatorTimer >= maxIndicatorTimer)
            {
                indicatorTimer = maxIndicatorTimer;
                radialIndicatorUI.fillAmount = maxIndicatorTimer;
                radialIndicatorUI.enabled = false;
                myEvent.Invoke();
            }
        }
        else
        {
            if(shouldUpdate)
            {
                indicatorTimer -= Time.deltaTime;
                radialIndicatorUI.fillAmount = indicatorTimer;

                if(indicatorTimer <= 0)
                {
                    indicatorTimer = 0;
                    radialIndicatorUI.fillAmount = 0;
                    radialIndicatorUI.enabled = false;
                    shouldUpdate = false;
                }
            }
        }

        if(Input.GetKeyUp(selectKey))
        {
            shouldUpdate = true;
        }
    }

    public void UpdateRadialPosition()
    {
        //Vector2 movePos;
        //Canvas _canvas = this.gameObject.GetComponentInParent<Canvas>();
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,Input.mousePosition, _canvas.worldCamera, out movePos);

        //transform.position = _canvas.transform.TransformPoint(movePos);

        //var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        //screenPoint.z = 10.0f; //distance of the plane from the camera
        //radialIndicatorUI.transform.position = Camera.main.WorldToScreenPoint(screenPoint);

    }
}
