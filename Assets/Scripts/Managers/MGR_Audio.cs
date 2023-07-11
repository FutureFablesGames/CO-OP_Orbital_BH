using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR_Audio : MonoBehaviour
{ 
    // ========================  AUDIO PLAYER COMPONENTS ==============================

    public FMODUnity.StudioEventEmitter SFX_Source;
    public FMODUnity.StudioEventEmitter BGM_Source;

    // ======================== PITCH ADJUSTMENT RANGE ==============================
    
    public float LowPitchRange = 0.95f;
    public float HighPitchRange = 1.05f;

    // ======================== SOUND LIBRARY ==============================

    private Dictionary<SoundFile, FMODUnity.EventReference> AudioLib = new Dictionary<SoundFile, FMODUnity.EventReference>();

    [Header("Sounds")]
    public FMODUnity.EventReference debug_Test;

    // ======================== MONOBEHAVIOUR ==============================

    private void Awake()
    {
        // Initialize Singleton
        if (Manager.Audio != null)
        {
            // Audio Manager already exists
            Destroy(this.gameObject);
        }

        Manager.Audio = this;

        // Populate AudioLib
        AudioLib.Add(SoundFile.Test, debug_Test);

        // Dont Destroy This When Switching Scenes
        DontDestroyOnLoad(this);
    }

    // ======================== FUNCTIONALITY ==============================

    public void PlayInstance(SoundFile file)
    {
        SFX_Source.EventReference = AudioLib[file];
        SFX_Source.Play();
    }     
}
