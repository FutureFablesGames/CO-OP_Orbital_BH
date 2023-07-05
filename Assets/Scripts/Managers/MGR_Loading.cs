using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class MGR_Loading : MonoBehaviour
{   
    [Header("Scene Loading")]    
    public static int NextSceneIndex = 1;  // The next scene to be loaded once the loading process is complete
    private float load_progress = 0.0f;
    private bool scene_changing = false;

    [Header("Interface")]
    public TMP_Text StatusDisplay;
    public TMP_Text VersionDisplay;
    public Image LoadingBar;
    public Image FadeImage;

    [Header("Debug")]
    public bool DEBUG_SkipDelaySequence = false;

    private void Start()
    {
        LoadingBar.fillAmount = 0.0f;
        FadeImage.color = new Color(0, 0, 0 , 0);

        CheckVersion();
        StartCoroutine(Initialize());
    }

    private void Update()
    {        
        LoadingBar.fillAmount = load_progress;

        if (LoadingBar.fillAmount >= 1.0f && Manager.Initialized && !scene_changing && !DEBUG_SkipDelaySequence)
        {
            StartCoroutine(LoadNextScene());
        } else
        {
            Manager.Scene.Load(NextSceneIndex);
        }
    }


    /* NOTE: I'm using the WaitForSeconds delay to simulate the delay of loading large files or connecting to a server.  This should be removed later */
    private IEnumerator Initialize()
    {   
        int numManagers = 7;

        if (!DEBUG_SkipDelaySequence) yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

        // Scene
        SetStatus("Initializing SceneManager");
        yield return Manager.Scene = MGR_Scene.Initialize();
        if (Manager.Scene == null) { Debug.LogError("Failed to Initialize Scene Manager"); yield break; }
        else load_progress += 1f / numManagers;

        if (!DEBUG_SkipDelaySequence) yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

        // Settings
        SetStatus("Initializing Settings");
        Manager.Settings = MGR_Settings.Initialize();
        if (Manager.Settings == null) { Debug.LogError("Failed to Initialize Settings Manager"); yield break; }
        else load_progress += 1f / numManagers;

        if (!DEBUG_SkipDelaySequence) yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

        // Sound
        SetStatus("Initializing Sound Manager");
        Manager.Sound = MGR_Sound.Initialize();
        if (Manager.Scene == null) { Debug.LogError("Failed to Initialize Sound Manager"); yield break; }
        else load_progress += 1f / numManagers;

        if (!DEBUG_SkipDelaySequence) yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));        

        // Input
        SetStatus("Initializing Input Manager");
        Manager.Input = MGR_Input.Initialize();
        if (Manager.Scene == null) { Debug.LogError("Failed to Initialize Input Manager"); yield break; }
        else load_progress += 1f / numManagers;

        if (!DEBUG_SkipDelaySequence) yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

        // Assets
        SetStatus("Initializing Assets");
        Manager.Assets = MGR_Asset.Initialize();
        if (Manager.Scene == null) { Debug.LogError("Failed to Initialize Asset Manager"); yield break; }
        else load_progress += 1f / numManagers;

        if (!DEBUG_SkipDelaySequence) yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

        // Network
        SetStatus("Initializing Network Manager");
        Manager.Network = MGR_Network.Initialize();
        if (Manager.Scene == null) { Debug.LogError("Failed to Initialize Network Manager"); yield break; }
        else load_progress += 1f / numManagers;

        if (!DEBUG_SkipDelaySequence) yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

        // Load Manager
        SetStatus("Finalizing Game Loader");
        Manager.Loader = this;
        if (Manager.Loader == null) { Debug.LogError("Failed to Initialize Load Manager"); yield break; }
        else load_progress += 1f / numManagers;

        if (!DEBUG_SkipDelaySequence) yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

        SetStatus("Loading Complete");

        if (!DEBUG_SkipDelaySequence) yield return new WaitForSeconds(2f);
        Manager.Initialized = true;
    }

    private void SetStatus(string output)
    {
        StatusDisplay.text = output;
    }

    private void CheckVersion()
    {
        // Check version of game files
        /*  For now we're not validating the game files, but we'll want to develop a way to do so in the future
         *  This way we can check to see if the game is out of date or if there was tampering with the game files for cheaters.
         *  If there's a version mismatch or tampering is detected, we eject the user from the application or force an update for the correct files.
        */


        // Set the Display
        VersionDisplay.text = "version v0.0.2 - \"Sprint 2\"  ©FutureFables"; // using 0.0.2 to represent the sprint we're currently on, rather than the number of pushes or updates made.  This way you guys don't have to remember to change this whenever you push, I'll handle it -- Cody
    }

    private IEnumerator LoadNextScene()
    {
        scene_changing = true;

        while (FadeImage.color.a < 1)
        {
            FadeImage.color = new Color(0, 0, 0, FadeImage.color.a + Time.deltaTime / 2);
            yield return null;
        }

        Manager.Scene.Load(NextSceneIndex);
    }
}
