using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class UpgradeItem : MonoBehaviour
{
    public string upgrade_name;
    public string upgrade_description;
    public VideoClip upgrade_vClip;
    public bool acquired = false;
    public Button upgrade_button;
    public GameObject upgrade_infoBox;
    float p_timer = 0f;
    float max_p_timer = 1f;
    bool isPressed = false;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(isPressed && acquired == false)
        {
            p_timer += Time.deltaTime;
            Debug.Log(p_timer);
            if(p_timer >= max_p_timer)
            {
                Debug.Log("Upgrade Acquried");
                DisplayUpgradeInfo();
                acquired = true;
                this.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            }
        }
        else
        {
            p_timer = 0f;
        }
    }

    public void DisplayUpgradeInfo()
    {
        upgrade_infoBox.SetActive(true);
        upgrade_infoBox.GetComponent<UpgradeInfoBox>().DisplayUpgrade(this);
    }

    public void HideUpgradeInfo()
    {
        upgrade_infoBox.SetActive(false);
    }
    
    public void AcquireUpgrade()
    {
        Debug.Log("Mouse pressed");
        isPressed = true;
    }

    public void UpgradeCanceled()
    {
        Debug.Log("Mouse no longer pressed");
        isPressed = false;
    }

    public string GetUpgradeName()
    {
        return upgrade_name;
    }

    public string GetUpgradeDescription()
    {
        return upgrade_description;
    }

    public VideoClip GetUpgradeVideoClip()
    {
        return upgrade_vClip;
    }

    public bool IsAcquired()
    {
        return acquired;
    }
}
