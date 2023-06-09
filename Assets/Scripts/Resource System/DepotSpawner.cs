using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DepotSpawner : MonoBehaviour
{
    public GameObject DepotPrefab;

    public int numPlayers = 2;    
    
    public List<Vector3> SpawnPoints = new List<Vector3>();

    public GameObject PlanetReference = null;

    private void Start()
    {
        SpawnDepots();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SpawnPoints.Clear();
            SpawnPoints = new List<Vector3>();
            SpawnDepots();
        }
    }

    private void SpawnDepots()
    {
        // Generate a point on bounds of the sphere.
        float radius = PlanetReference.transform.localScale.x;
        if (radius != PlanetReference.transform.localScale.y || radius != PlanetReference.transform.localScale.z)
            Debug.LogError("Planet is not a perfect sphere.  Spawning system cannot guarentee correct spawn points.");

        // Generate Points for Player Depots
        SpawnPoints = new List<Vector3>(SphereUtils.EquidistantPoints(numPlayers, radius));

        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            // Adjust the position to be relative to the spawners position.
            SpawnPoints[i] += transform.position;

            // Spawn the Depots
            GameObject go = Instantiate(DepotPrefab, SpawnPoints[i], Quaternion.identity);

            // Rotate to correct upwards orientation
            Vector3 normal = (SpawnPoints[i] - transform.position).normalized;
            go.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
        }
    }

    private void OnDrawGizmos()
    {        
        if (SpawnPoints.Count > 0)
        {
            foreach (Vector3 pt in SpawnPoints)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(pt, 1.0f);
            }
        }

        
    }
}
