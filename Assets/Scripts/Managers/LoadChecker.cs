using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadChecker : MonoBehaviour
{
    private void Awake()
    {
        // If the loader has not had the opportunity to be initialized
        if (!Manager.Initialized)
        {
            // Get the current active scene
            int index = SceneManager.GetActiveScene().buildIndex;
            
            // Unload the scene, but store it in the load manager as the followup scene.
            SceneManager.UnloadSceneAsync(index);
            MGR_Loading.NextSceneIndex = index;

            // Load the loading scene to initialize the game.
            SceneManager.LoadSceneAsync(0);
        }

        // Otherwise, just destroy this gameobject.
        else
        {
            Destroy(this.gameObject);
        }
    }
}
