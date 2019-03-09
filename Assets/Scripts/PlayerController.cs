using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class PlayerController : CharacterController {
    [SerializeField] float xAxisMovementSpeed = 10;
    [SerializeField] float jumpForce = 5;
    public bool joystickJump = true;
    bool buttonJump;
    Rigidbody2D rb;
    [SerializeField] GroundVariables groundVariables;
    [System.Serializable]
    class GroundVariables {
        public Vector2 groundPoint;
        public Vector2 groundRadius;
        public LayerMask groundMask;
    }
    void Awake () {
        rb = GetComponent<Rigidbody2D> ();
    }
    protected override void Update () {
        base.Update ();
        buttonJump = Jump ();
        Debug.Log (buttonJump);

    }

    bool Jump () => Input.GetButton (jumpButtonSrc);
    private void FixedUpdate () {
        Movement ();
    }

    bool GroundCheck () => Physics2D.OverlapBox (new Vector2 (transform.position.x, transform.position.y) + groundVariables.groundPoint, groundVariables.groundRadius, 0, groundVariables.groundMask);
    void Movement () {
        Vector2 direction = new Vector3 (joy1X, 0);
        rb.velocity = (direction.normalized * xAxisMovementSpeed) + new Vector2 (0, rb.velocity.y);
        if (Jump () && GroundCheck ()) {
            //Debug.Log ("jumpbuttom");
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
            return;
        }
        //Debug.Log (GroundCheck ());
        if (!joystickJump) {
            return;
        }
        if (joy1Y < 0 && GroundCheck ()) {
            rb.velocity = new Vector2 (rb.velocity.x, -joy1Y * jumpForce);
            return;
        }
        Debug.Log (buttonJump);

    }

    void GizmoDraw () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube (new Vector2 (transform.position.x, transform.position.y) + groundVariables.groundPoint, groundVariables.groundRadius);
    }

    private void OnDrawGizmosSelected () {
        GizmoDraw ();
    }
}