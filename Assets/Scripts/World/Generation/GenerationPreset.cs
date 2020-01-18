using UnityEngine;

[CreateAssetMenu()]
public class GenerationPreset : ScriptableObject {
    public float radius = 5f;
    public GameObject[] spawnableObjects;
    public int numToSpawn = 10;
    public float objectSpawnThreshold = 0.5f;
    public bool canRotate = false;
}
