using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class MapLoader : MonoBehaviour
{
    // Sequence of Events
    // 1. Instantiate / Create the Planet
    // 2. Rescale the planet based on map size
    // 3. Generate Spawn Points based on number of players (includes player spawn and resource depot)    
    // 4. Generate Resource Nodes around the map

    public GameObject PlanetReference;
    public GameObject SpawnPrefab;
    public GameObject ResourcePrefab;

    public void Start()
    {
        if (Manager.Initialized)
        {
            BeginLoad();
        }
        
    }

    public async Task BeginLoad()
    {      
        await CreatePlanet();
        Debug.Log("Creation Complete");

        await GenerateSpawnPoints();
        Debug.Log("Spawn Points Set");

        await GenerateResources();
        Debug.Log("Resources Generated");

        Debug.Log("Load Complete.  Starting game.");

        Manager.Game.StartGame();
    }

    public async Task CreatePlanet()
    {
        // Create the Planet based on Prefab
        PlanetReference = Instantiate(Resources.Load<GameObject>("Prefabs/PlanetPrefab"), Vector3.zero, Quaternion.identity);

        // Rescale the planet based on map size
        PlanetReference.transform.localScale = GameSettings.GetScale(GameSettings.MapSize);

        // Set Reference in Game Manager
        Manager.Game.Planet = PlanetReference;

        await Task.Delay(500);
    }

    public async Task GenerateSpawnPoints()
    {
        // Calculate Planet Scale
        float radius = PlanetReference.transform.localScale.x;
        if (radius != PlanetReference.transform.localScale.y || radius != PlanetReference.transform.localScale.z)
            Debug.LogError("Planet is not a perfect sphere.  Spawning system cannot guarentee correct spawn points.");

        // Generate Points for Player Depots
        List<Vector3> SpawnPoints = new List<Vector3>(SphereUtils.EquidistantPoints(GameSettings.PlayerCount, radius));

        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            // Adjust the position to be relative to the spawners position.
            SpawnPoints[i] += PlanetReference.transform.position;

            // Spawn the Depots
            GameObject go = Instantiate(SpawnPrefab, SpawnPoints[i], Quaternion.identity);

            // Rotate to correct upwards orientation
            Vector3 normal = (SpawnPoints[i] - transform.position).normalized;
            go.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);

            // Set References in Game Manager
            Manager.Game.SpawnPoints.Add(go);
        }
    }

    public async Task GenerateResources()
    {
        // Calculate Planet Scale
        float radius = PlanetReference.transform.localScale.x;
        if (radius != PlanetReference.transform.localScale.y || radius != PlanetReference.transform.localScale.z)
            Debug.LogError("Planet is not a perfect sphere.  Spawning system cannot guarentee correct spawn points.");

        // Generate a point on bounds of the sphere.
        Vector3 bounds = Random.onUnitSphere * radius;                

        // Calculate the closest point (assuming bounds are not perfect).
        Collider col = PlanetReference.GetComponent<Collider>();
        Vector3 result = col.ClosestPoint(PlanetReference.transform.position + bounds);        

        // Spawn the prefab at the generated point
        GameObject spawnedPrefab = Instantiate(ResourcePrefab, result, Quaternion.identity);

        // Rotate to correct upwards orientation
        Vector3 normal = (result - transform.position).normalized;
        spawnedPrefab.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
    }
}
