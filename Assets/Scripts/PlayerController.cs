using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class PlayerController : CharacterController {
    [SerializeField] float xAxisMovementSpeed = 10;
    [SerializeField] float jumpForce = 5;
    public bool joystickJump = true;
    Rigidbody2D rb;
    [SerializeField] GroundVariables groundVariables;
    [System.Serializable]
    class GroundVariables {
        public Vector2 groundPoint;
        public float groundRadius;
        public LayerMask groundMask;
    }
    void Awake () {
        rb = GetComponent<Rigidbody2D> ();
    }
    protected override void Update () {
        base.Update ();

    }
    private void FixedUpdate () {
        Movement ();
    }

    bool GroundCheck () => Physics2D.OverlapCircle (groundVariables.groundPoint, groundVariables.groundRadius, groundVariables.groundMask);
    void Movement () {
        Vector2 direction = new Vector3 (joy1X, 0);
        rb.velocity = (direction.normalized * xAxisMovementSpeed) + new Vector2 (0, rb.velocity.y);
        Debug.Log (joy1Y);
        if (!joystickJump && !GroundCheck ()) {
            return;
        }
        if (joy1Y > 0) {
            rb.velocity = new Vector2 (rb.velocity.x, joy1Y) * jumpForce;
        }
    }
}