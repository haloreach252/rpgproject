using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerZone : MonoBehaviour {

    public float radius = 5f;
    public GameObject[] spawnableObjects;
    public int numToSpawn;

    private List<Vector3> positions;

    private int counter;

    private void Start() {
        positions = new List<Vector3>();
        SpawnObjects();
    }

    private void Update() {
        
    }

    private void SpawnObjects() {
        if (spawnableObjects.Length <= 0) return;
        for (int i = 0; i < numToSpawn; i++) {
            int randomObject = Random.Range(0, spawnableObjects.Length - 1);
            Vector3 spawnPos = GenerateSpawnPos();
            Vector3 rotation = new Vector3(Random.Range(-8,8), Random.Range(-180, 180), Random.Range(-8,8));
            GameObject flower = Instantiate(spawnableObjects[randomObject], spawnPos, Quaternion.Euler(rotation));
            flower.transform.parent = transform;
            positions.Add(spawnPos);
            counter++;
        }
        Debug.Log(counter);
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
                if(Vector3.Distance(pos, positions[i]) <= 0.2) {
                    Debug.Log("Object tried to spawn too close");
                    return true;
                }
            }
            return false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius * 2);
    }

}
