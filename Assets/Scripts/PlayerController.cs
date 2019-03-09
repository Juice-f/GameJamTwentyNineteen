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
    [SerializeField] SlapMechanics slapInfo;

    [System.Serializable] class GroundVariables {
        public Vector2 groundPoint;
        public Vector2 groundRadius;
        public LayerMask groundMask;
    }

    [System.Serializable] class SlapMechanics {
        public Slapdata normalSlap;
        public Slapdata heavySlap;
        public float midAirDashSpeed;

        public RaycastHit2D pontentialTargetToBeSpanked;
    }
    void Awake () {
        rb = GetComponent<Rigidbody2D> ();
    }
    protected override void Update () {
        base.Update ();

    }

    bool Jump () => Input.GetButton (jumpButtonSrc);
    RaycastHit2D SlapRay () => Physics2D.Raycast (transform.position, transform.right, 1);
    private void FixedUpdate () {
        Movement ();
    }

    public override void Slap (Slapdata slapdata, GameObject slapOrigin) {
        base.Slap (slapdata, slapOrigin);

    }

    bool GroundCheck () => Physics2D.OverlapBox (new Vector2 (transform.position.x, transform.position.y) + groundVariables.groundPoint, groundVariables.groundRadius, 0, groundVariables.groundMask);

    void OnNormalAttack () {
        if (SlapRay ().collider.GetComponent<ISlappable> () == null) {
            return;
        }
        Debug.Log ("Slapped someone!" + SlapRay ().collider.name);
    }
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

    void GizmoDrawHitBox () {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay (transform.position, transform.right);
    }

    private void OnDrawGizmosSelected () {
        GizmoDraw ();
        GizmoDrawHitBox ();
    }
}