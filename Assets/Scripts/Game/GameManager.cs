using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Material harmMat;
    public Material healMat;

    public GameObject harmParticles;
    public GameObject healParticles;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SpawnParticle(Vector3 pos, GameObject particle) {
        GameObject t = Instantiate(particle, pos, Quaternion.Euler(-90, 0, 0));
        Destroy(t, 1.2f);
    }

    public void SpawnParticle(Vector3 pos, bool isHarm) {
        GameObject particle = isHarm ? harmParticles : healParticles;
        GameObject t = Instantiate(particle, pos, Quaternion.Euler(-90, 0, 0));
        Destroy(t, 1.2f);
    }
}
