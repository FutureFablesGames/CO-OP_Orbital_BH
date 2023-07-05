using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Panel")]
    public GameObject MenuPanel;
    public Button SingleplayerBtn, MultiplayerBtn, LoadoutBtn, SettingsBtn, QuitBtn;   

    private void Awake()
    {
        // Initialize the buttons
        SingleplayerBtn.onClick.AddListener(() => OnSingleplayerPress());
        MultiplayerBtn.onClick.AddListener(() => OnMultiplayerPress());
        LoadoutBtn.onClick.AddListener(() => OnLoadoutPress());
        SettingsBtn.onClick.AddListener(() => OnSettingsPress());
        QuitBtn.onClick.AddListener(() => Manager.Scene.Quit());
    }

    private void OnSingleplayerPress()
    {
        Manager.Scene.Load((int)Scenes.game);
    }

    private void OnMultiplayerPress()
    {
        Debug.Log("Multiplayer");
    }

    private void OnLoadoutPress()
    {
        Debug.Log("Loadout");
    }

    private void OnSettingsPress()
    {
        Debug.Log("Settings");
    }

}
