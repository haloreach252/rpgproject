using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float moveSpeed = 5f;
    public float rotSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private Animator anim;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        if(rb == null) {
            gameObject.AddComponent<Rigidbody>();
        }
        SetRigidbodySettings();
    }

    private void SetRigidbodySettings() {
        
    }

    private void Update() {
        anim.SetInteger("state", 0); // 0 IS IDLE, 1 IS FORWARD, 2 IS BACKWARD, 3 IS JUMP
        if (Input.GetKey(KeyCode.W)) {
            anim.SetInteger("state", 1);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            //rb.AddRelativeForce(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S)) {
            anim.SetInteger("state", 2);
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            //rb.AddRelativeForce(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            anim.SetInteger("state", 3);
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
            //rb.AddForce(Vector3.up * jumpForce * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            anim.SetInteger("state", 5);
        }

    }
}
