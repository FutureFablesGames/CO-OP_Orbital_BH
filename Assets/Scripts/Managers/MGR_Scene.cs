using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    loading,
    menu,    
    tutorial,
    game,
    sandbox,    
}


public class MGR_Scene
{    
    private static MGR_Scene instance;
    private static bool Initialized = false;
    public static MGR_Scene Initialize() 
    {
        if (Initialized) Debug.LogWarning("WARNING: Scene Manager already exists.  Please use Manager.Scene instead.");        

        if (instance == null) instance = new MGR_Scene();
        return instance;
    }

    public void Load(int index)
    {
        // Get the current active scene
        int current = SceneManager.GetActiveScene().buildIndex;

        // Unload the scene, but store it in the load manager as the followup scene.
        SceneManager.UnloadSceneAsync(current);

        // Load the loading scene to initialize the game.
        SceneManager.LoadSceneAsync(index);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}
