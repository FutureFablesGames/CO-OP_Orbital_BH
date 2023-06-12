using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject PrefabToSpawn = null;

    [Header("Spawner Settings")]
    public GameObject PlanetReference;
    public int SpawnCount = 10;
    public float SpawnRadius = 10.0f;

    [Header("Spawner Debug")]
    public List<Vector3> spawnPts = new List<Vector3>();
    public bool Debug_Spawn = true;

    private void Start()
    {        
        Begin();        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Begin();
        }
    }

    public void Begin()
    {
        // Spawn 10 resources on the surface of the level.
        spawnPts.Clear();
        spawnPts = new List<Vector3>();
        for (int i = 0; i < SpawnCount; i++)
        {
            SpawnOnSphere();
        }
    }

    public void Spawn()
    {
        // Generate a point offset from the origin of the spawner
        Vector3 spawnOffset = Vector3.zero;
        spawnOffset.x += Random.Range(-1f, 1f);
        spawnOffset.y += Random.Range(-1f, 1f);
        spawnOffset.z += Random.Range(-1f, 1f);
        
        // Calculate the direction from the origin of the planet to the generated point
        Vector3 point = transform.position + spawnOffset;
        Vector3 dir = transform.position - point;

        // Find the closest point along the surface of a mesh
        Collider col = PlanetReference.GetComponent<Collider>();       
        Vector3 result = col.ClosestPoint(transform.position + (dir * SpawnRadius));
        spawnPts.Add(result);

        // Spawn the prefab at the generated point
        //GameObject spawnedPrefab = Instantiate(PrefabToSpawn, result, Quaternion.identity);            
    }

    public void SpawnOnSphere()
    {
        // Generate a point on bounds of the sphere.
        float radius = PlanetReference.transform.localScale.x;
        if (radius != PlanetReference.transform.localScale.y || radius != PlanetReference.transform.localScale.z) 
            Debug.LogError("Planet is not a perfect sphere.  Spawning system cannot guarentee correct spawn points.");
        Vector3 bounds = Random.onUnitSphere * radius;
        //spawnPts.Add(bounds);


        // Calculate the closest point (assuming bounds are not perfect).
        Collider col = PlanetReference.GetComponent<Collider>();
        Vector3 result = col.ClosestPoint(transform.position + bounds);
        spawnPts.Add(result);

        // Spawn the prefab at the generated point
        GameObject spawnedPrefab = Instantiate(PrefabToSpawn, result, Quaternion.identity);

        // Rotate to correct upwards orientation
        Vector3 normal = (result - transform.position).normalized;
        spawnedPrefab.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
    }

    private void OnDrawGizmos()
    {
        if (Debug_Spawn)
        {
            if (PlanetReference != null) 
                Gizmos.DrawWireSphere(transform.position, PlanetReference.transform.localScale.x / 2);

            foreach (Vector3 v in spawnPts)
            {
                // Draw a line from the center of spawner to the spawn point
                Gizmos.DrawLine(transform.position, v);

                // Draw a sphere at the point that it should be spawning
                Gizmos.DrawWireSphere(v, 1.0f);
            }
        }
    }


}
