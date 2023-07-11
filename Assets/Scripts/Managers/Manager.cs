using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Manager
{
    public static bool Initialized = false;

    // SceneLoaded
    public static MainMenu MainMenu;
    public static MGR_Game Game;    

    // GameLoaded
    public static MGR_Scene Scene;
    public static MGR_Loading Loader;
    public static MGR_Input Input;
    public static MGR_Network Network;
    public static MGR_Audio Audio;
    public static MGR_Asset Assets;    
    public static MGR_Settings Settings;
    public static MGR_Interface UI;
}




