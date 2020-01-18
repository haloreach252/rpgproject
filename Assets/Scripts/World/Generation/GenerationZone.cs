using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationZone : MonoBehaviour {

    public GenerationPreset preset;

    private float radius;
    private GameObject[] spawnableObjects;
    private int numToSpawn;
    private float objectSpawnThreshold;
    private bool canRotate;

    private List<Vector3> positions;

    private void Start() {
        radius = preset.radius;
        spawnableObjects = preset.spawnableObjects;
        numToSpawn = preset.numToSpawn;
        objectSpawnThreshold = preset.objectSpawnThreshold;
        canRotate = preset.canRotate;

        positions = new List<Vector3>();
        SpawnObjects();
    }

    private void SpawnObjects() {
        if (spawnableObjects.Length <= 0) return;
        for (int i = 0; i < numToSpawn; i++) {
            int randomObject = Random.Range(0, spawnableObjects.Length - 1);
            Vector3 spawnPos = GenerateSpawnPos();
            Vector3 rotation = Vector3.zero;
            if (canRotate) {
                rotation = new Vector3(Random.Range(-8, 8), Random.Range(-180, 180), Random.Range(-8, 8));
            }
            GameObject spawnedObject = Instantiate(spawnableObjects[randomObject], spawnPos, Quaternion.Euler(rotation));
            spawnedObject.transform.parent = transform;
            positions.Add(spawnPos);
        }
    }

    private Vector3 GenerateSpawnPos() {
        float spawnX = Random.Range(-radius, radius);
        float spawnZ = Random.Range(-radius, radius);
        Vector3 pos = new Vector3(spawnX + transform.position.x, -0.01f, spawnZ + transform.position.z);
        if (!SpawnBlocked(pos)) {
            return pos;
        } else {
            return GenerateSpawnPos();
        }
    }

    private bool SpawnBlocked(Vector3 pos) {
        if (positions.Contains(pos)) {
            return true;
        } else {
            for (int i = 0; i < positions.Count; i++) {
                if (Vector3.Distance(pos, positions[i]) <= objectSpawnThreshold) {
                    Debug.Log("Object tried to spawn too close");
                    return true;
                }
            }
            return false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, preset.radius * 2);
    }
}
