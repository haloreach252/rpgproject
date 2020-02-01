using System;
using UnityEngine;

[Serializable]
public class PlayerEntity : Entity {

	public int invSize = 10;

	private Animator anim;
	private PlayerMove playerMove;
	private PlayerInventory playerInventory;

	override
	protected void Start() {
		anim = GetComponent<Animator>();
		playerMove = GetComponent<PlayerMove>();
		playerInventory = GetComponent<PlayerInventory>();
		SetInitialValues();
	}

	override
	protected void SetInitialValues() {
		entityStats = new EntityStats(10);
		maxHealth += entityStats.vitality;

		inventory = new EntityInventory(invSize);
		playerInventory.SetupUI();

		level = 1;
		health = maxHealth;
	}

	override
	protected void Die() {
		playerMove.SetDead();
		anim.SetBool("dead", true);
	}

}
