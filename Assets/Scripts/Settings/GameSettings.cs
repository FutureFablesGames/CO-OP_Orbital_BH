using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static float MatchDuration = 600;  // 10 minutes default
    public static int PlayerCount = 4;
    public enum MapScale
    {
        Tiny,       // -- 35 units
        Small,      // -- 70 units
        Medium,     // -- 140 units
        Large,      // -- 380 units
        Massive     // -- 720 units
    }

    public static MapScale MapSize = MapScale.Medium;

    public static Vector3 GetScale(MapScale scale)
    {
        switch (MapSize)
        { 
            
            case MapScale.Tiny:
                return new Vector3(35, 35, 35);
            case MapScale.Small:
                return new Vector3(70, 70, 70);
            default:
            case MapScale.Medium:
                return new Vector3(140, 140, 140);
            case MapScale.Large:
                return new Vector3(380, 380, 380);
            case MapScale.Massive:
                return new Vector3(720, 720, 720);
        }
    }

    public static void SetGlobalGravity(Vector3 gravity)
    {
        Physics.gravity = gravity;
    }
}
