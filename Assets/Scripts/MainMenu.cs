using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Panel")]
    public GameObject MenuPanel;
    public Button SingleplayerBtn, MultiplayerBtn, SandboxBtn, LoadoutBtn, SettingsBtn, QuitBtn;   

    private void Awake()
    {
        // Initialize the buttons
        SingleplayerBtn.onClick.AddListener(() => OnSingleplayerPress());
        MultiplayerBtn.onClick.AddListener(() => OnMultiplayerPress());
        SandboxBtn.onClick.AddListener(() => OnSandboxPress());
        LoadoutBtn.onClick.AddListener(() => OnLoadoutPress());
        SettingsBtn.onClick.AddListener(() => OnSettingsPress());
        QuitBtn.onClick.AddListener(() => Manager.Scene.Quit());
    }

    private void Start()
    {
        // Reveal the Cursor when on main menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnSingleplayerPress()
    {
        Manager.Scene.Load((int)Scenes.game);
    }

    /* Multiplayer should take you to a matchmaking screen or a lobby screen where you can join others, create your own
     * multiplayer lobby, and more big brain networking stuff
     */
    private void OnMultiplayerPress()
    {
        Debug.Log("Multiplayer");
    }

    /* Sandbox mode should be like free-play or practice mode where you can select the map you want to play, the weather conditions, hazards, etc.
     Maybe have some AI or dummies to attack, etc.  */
    private void OnSandboxPress()
    {
        Debug.Log("Sandbox");
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
