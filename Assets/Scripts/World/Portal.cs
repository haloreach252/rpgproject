using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	public Portal oppositePortal;
	public Transform teleportPos;

	public List<string> activeTags;

	public void Teleport(Transform transform) {
		transform.position = teleportPos.position;
		transform.rotation = teleportPos.rotation;
	}

	private void OnTriggerEnter(Collider other) {
		if (activeTags.Contains(other.tag)) {
			oppositePortal.Teleport(other.transform);
		}
	}

}
