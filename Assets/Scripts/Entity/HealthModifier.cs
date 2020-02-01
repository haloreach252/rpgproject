using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModifier : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Entity>()) {
            Entity e = collision.gameObject.GetComponent<Entity>();
            e.TakeDamage(690, new List<DamageType>() { DamageType.BLUDGEON, DamageType.POISON });
        }
    }

}
