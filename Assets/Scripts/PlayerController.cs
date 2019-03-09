using System;
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
    [SerializeField] GameObject playerSprite;
    [System.Serializable] class GroundVariables {
        public Vector2 groundPoint;
        public Vector2 groundRadius;
        public LayerMask groundMask;
    }

    [System.Serializable] class SlapMechanics {
        public Slapdata normalSlap;
        public Slapdata heavySlap;
        public float midAirDashSpeed;
        public Vector2 forwardLook;
        public float horizontalForwardLookDistance;
        public Transform raycastPos;

        //public RaycastHit2D pontentialTargetToBeSpanked;
    }
    void Awake () {
        rb = GetComponent<Rigidbody2D> ();
    }
    protected override void Update () {
        base.Update ();
        if (Input.GetButtonDown (smolSlapButtonSrc)) {
            OnNormalAttack ();
        }
        if (Input.GetButtonDown (bigSlapButtonSrc)) {
            OnHeavyAttack ();
        }

    }

    bool Jump () => Input.GetButton (jumpButtonSrc);

    private GameObject GetSlapRay () {
        return (!Physics2D.Raycast (new Vector2 ((slapInfo.raycastPos.position.x + slapInfo.horizontalForwardLookDistance), slapInfo.raycastPos.position.y), slapInfo.forwardLook, 0.5f)) ? GameObject.Find ("Dud") : Physics2D.Raycast (new Vector2 ((slapInfo.raycastPos.position.x + slapInfo.horizontalForwardLookDistance), slapInfo.raycastPos.position.y), slapInfo.forwardLook, 0.5f).collider.gameObject;
    }

    private void FixedUpdate () {
        Movement ();
    }

    public override void Slap (Slapdata slapdata, GameObject slapOrigin) {
        base.Slap (slapdata, slapOrigin);

    }

    bool GroundCheck () => Physics2D.OverlapBox (new Vector2 (transform.position.x, transform.position.y) + groundVariables.groundPoint, groundVariables.groundRadius, 0, groundVariables.groundMask);
    void OnHeavyAttack () {
        if (GetSlapRay () == null || GetSlapRay ().GetComponent<ISlappable> () == null) {
            return;
        }
        if (GetSlapRay ().GetComponent<ISlappable> () != null && !GetSlapRay ().transform.CompareTag ("GottaGoFast")) {
            GetSlapRay ().GetComponent<ISlappable> ().Slap (slapInfo.heavySlap, this.gameObject);
            Debug.Log ("Spanked someone!" + GetSlapRay ().transform.name);
        } else {
            return;
        }
    }
    void OnNormalAttack () {
        if (GetSlapRay () == null || GetSlapRay ().GetComponent<ISlappable> () == null) {
            return;
        }
        if (GetSlapRay ().GetComponent<ISlappable> () != null && !GetSlapRay ().transform.CompareTag ("GottaGoFast")) {
            GetSlapRay ().GetComponent<ISlappable> ().Slap (slapInfo.normalSlap, this.gameObject);
            Debug.Log ("Slapped someone!" + GetSlapRay ().transform.name);
        } else {
            return;
        }

    }
    void Movement () {
        Vector2 direction = new Vector3 (joy1X, 0);
        if (joy1X != 0) {
            slapInfo.forwardLook = (joy1X < 0) ? -transform.right : transform.right;
        }
        SpriteHandler.OnDirectionFlip (playerSprite.transform, joy1X);
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
        Gizmos.DrawRay (new Vector2 ((slapInfo.raycastPos.position.x + slapInfo.horizontalForwardLookDistance), slapInfo.raycastPos.position.y), slapInfo.forwardLook * 0.5f);
    }

    private void OnDrawGizmosSelected () {
        GizmoDraw ();
        GizmoDrawHitBox ();
    }
}