using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class UpgradeInfoBox : MonoBehaviour
{
    public TextMeshProUGUI u_name;
    public TextMeshProUGUI u_description;
    public VideoPlayer vplayer;
    public Image u_tag;
    public TextMeshProUGUI u_cost;

    public Sprite acquiredTag;
    public Sprite availableTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUpgradeName(string newName)
    {
        u_name.text = newName;
    }

    public void UpdateUpgradeDescription(string newDescription)
    {
        u_description.text = newDescription;
    }
    public void UpdateUpgradeVideo(VideoClip newVideo)
    {
        vplayer.clip = newVideo;
    }

    public void UpgradeAcquired()
    {

    }

    public void DisplayUpgrade(UpgradeItem _upgrade)
    {
        u_name.text = _upgrade.GetUpgradeName();
        u_description.text = _upgrade.GetUpgradeDescription();
        vplayer.clip = _upgrade.GetUpgradeVideoClip();
        if(_upgrade.IsAcquired())
        {
            u_tag.sprite = acquiredTag;
            u_cost.gameObject.SetActive(false);
        }
        else
        {
            u_tag.sprite = availableTag;
            u_cost.gameObject.SetActive(true);
        }
    }
}
