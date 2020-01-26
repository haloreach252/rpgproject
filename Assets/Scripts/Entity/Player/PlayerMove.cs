using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float moveSpeed = 5f;
    public float rotSpeed = 5f;
    public float jumpForce = 10f;

    [SerializeField]
    private bool isGrounded;

    private Rigidbody rb;
    private Animator anim;

    private bool turned;

    private void Start() {
        isGrounded = true;
        turned = false;

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        if(rb == null) {
            gameObject.AddComponent<Rigidbody>();
        }

        rb.centerOfMass = Vector3.zero;
    }

    private void Update() {

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.15f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            anim.SetInteger("state", 3);
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }

        if (isGrounded) {
            anim.SetInteger("state", 0); // 0 IS IDLE, 1 IS FORWARD, 2 IS BACKWARD, 3 IS JUMP

            if (Input.GetKey(KeyCode.W)) {
                anim.SetInteger("state", 1);
                rb.AddRelativeForce(Vector3.forward * moveSpeed);
                turned = false;
            }

            if (Input.GetKey(KeyCode.S)) {
                if (!turned) {
                    transform.Rotate(new Vector3(0, 180, 0));
                    anim.SetInteger("state", 1);
                    rb.AddRelativeForce(Vector3.forward * moveSpeed);
                    turned = true;
                } else {
                    anim.SetInteger("state", 1);
                    rb.AddRelativeForce(Vector3.forward * moveSpeed);
                }
            }

            /* TEMP REMOVAL, for now just gonna have the player do a 180 then walk forward
            if (Input.GetKey(KeyCode.S)) {
                anim.SetInteger("state", 2);
                rb.AddRelativeForce(Vector3.back * moveSpeed);
            }*/
        }

        if (Input.GetKey(KeyCode.A)) {
            Vector3 rot = new Vector3(0, -1, 0) * rotSpeed * Time.deltaTime;
            rb.AddTorque(rot);
        }

        if (Input.GetKey(KeyCode.D)) {
            Vector3 rot = new Vector3(0, 1, 0) * rotSpeed * Time.deltaTime;
            rb.AddTorque(rot);
        }
    }
}
