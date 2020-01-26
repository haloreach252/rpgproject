using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEntity : MonoBehaviour {

    public bool isHarm;
    public float scale;
    public float effect;
    public List<Entity.DamageType> damageTypes;

    private void Start() {
        transform.localScale *= scale;
        if (isHarm) {
            GetComponent<Renderer>().material = GameManager.Instance.harmMat;
        } else {
            GetComponent<Renderer>().material = GameManager.Instance.healMat;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Entity>() != null) {
            Entity e = other.GetComponent<Entity>();
            if (isHarm) {
                e.TakeDamage(effect, damageTypes);
            } else {
                e.Heal(effect);
            }
            GameManager.Instance.SpawnParticle(e.transform.position, isHarm);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, scale / 2);
    }

}
