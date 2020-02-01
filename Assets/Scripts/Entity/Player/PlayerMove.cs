using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    // Move speed vars
    public float moveSpeed = 5f;
    public float rotSpeed = 5f;
    public float jumpForce = 10f;

    public float runSpeedMult = 1.6f;
    public float crouchSpeedMult = 0.6f;

    private float speedMult = 1.0f;

    // Stairs vars
    public float maxStepHeight = 0.4f;
    public float stepSearchOvershoot = 0.01f;
    private List<ContactPoint> allCp = new List<ContactPoint>();
    private Vector3 lastVelocity;

    // Control vars
    [SerializeField]
    private bool isGrounded;
    private bool crouched;
    [SerializeField]
    private bool isDead;

    private Rigidbody rb;
    private Animator anim;

	private void Start() {
        isGrounded = true;
        crouched = false;
        isDead = false;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            gameObject.AddComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
    }

    private void Update() {
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            anim.SetTrigger("jump");
            rb.AddForce(Vector3.up * jumpForce);
            anim.SetBool("grounded", false);
            isGrounded = false;
        }

        if (isGrounded) {
            anim.SetInteger("moveState", 0);
            anim.ResetTrigger("jump");

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) {
                crouched = !crouched;
                anim.SetBool("crouched", crouched);
            }

            if (Input.GetKey(KeyCode.W)) {
                if (!crouched) {
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                        anim.SetInteger("moveState", 2);
                        speedMult = runSpeedMult;
                    } else {
                        anim.SetInteger("moveState", 1);
                        speedMult = 1.0f;
                    }
                } else {
                    anim.SetInteger("moveState", 1);
                    speedMult = crouchSpeedMult;
                }

                rb.AddRelativeForce(Vector3.forward * moveSpeed * speedMult);
            }
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

    public void SetDead() {
        isDead = true;
    }

    #region Grounded check

    private void OnCollisionEnter(Collision collision) {
        allCp.AddRange(collision.contacts);
        anim.SetBool("grounded", true);
        isGrounded = true;
    }

    private void OnCollisionStay(Collision collision) {
        allCp.AddRange(collision.contacts);
    }

    private void OnCollisionExit(Collision collision) {
        // todo: only certain tags/layers can be used and also only on the bottom
        anim.SetBool("grounded", false);
        isGrounded = false;
    }

    #endregion

    #region STEPPING UP
    private void FixedUpdate() {
        Vector3 velocity = rb.velocity;

        ContactPoint groundCP = default;
        bool grounded = FindGround(out groundCP, allCp);

        Vector3 stepUpOffset = default;
        bool stepUp = false;
        if (grounded)
            stepUp = FindStep(out stepUpOffset, allCp, groundCP, velocity);

        if (stepUp) {
            rb.position += stepUpOffset;
            rb.velocity = lastVelocity;
        }

        allCp.Clear();
        lastVelocity = velocity;
    }

    private bool FindGround(out ContactPoint groundCp, List<ContactPoint> allCps) {
        groundCp = default;
        bool found = false;
        foreach (ContactPoint cp in allCps) {
            if (cp.normal.y > 0.0001f && (found == false || cp.normal.y > groundCp.normal.y)) {
                groundCp = cp;
                found = true;
            }
        }

        return found;
    }

    private bool FindStep(out Vector3 stepUpOffset, List<ContactPoint> allCps, ContactPoint groundCp, Vector3 currVelocity) {
        stepUpOffset = default;

        Vector2 velocityXZ = new Vector2(currVelocity.x, currVelocity.z);
        if (velocityXZ.sqrMagnitude < 0.0001f) {
            return false;
        }

        foreach (ContactPoint cp in allCps) {
            bool test = ResolveStepUp(out stepUpOffset, cp, groundCp);
            if (test) return test;
        }
        return false;
    }

    private bool ResolveStepUp(out Vector3 stepUpOffset, ContactPoint stepTestCp, ContactPoint groundCp) {
        stepUpOffset = default;
        Collider stepCol = stepTestCp.otherCollider;
        if (Mathf.Abs(stepTestCp.normal.y) >= 0.01f) {
            return false;
        }
        if (!(stepTestCp.point.y - groundCp.point.y < maxStepHeight)) {
            return false;
        }
        RaycastHit hit;
        float stepHeight = groundCp.point.y + maxStepHeight + 0.0001f;
        Vector3 stepTestInvDir = new Vector3(-stepTestCp.normal.x, 0, -stepTestCp.normal.z).normalized;
        Vector3 origin = new Vector3(stepTestCp.point.x, stepHeight, stepTestCp.point.z) + (stepTestInvDir * stepSearchOvershoot);
        Vector3 direction = Vector3.down;
        if (!stepCol.Raycast(new Ray(origin, direction), out hit, maxStepHeight)) {
            return false;
        }
        Vector3 stepUpPoint = new Vector3(stepTestCp.point.x, hit.point.y + 0.0001f, stepTestCp.point.z) + (stepTestInvDir * stepSearchOvershoot);
        Vector3 stepUpPointOffset = stepUpPoint - new Vector3(stepTestCp.point.x, groundCp.point.y, stepTestCp.point.z);

        stepUpOffset = stepUpPointOffset;
        return true;
    }
    #endregion
}
