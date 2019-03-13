using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : CharacterController
{
    [SerializeField] float xAxisMovementSpeed = 10;
    [SerializeField] float jumpForce = 5;
    public bool joystickJump = true;
    bool buttonJump;
    Rigidbody2D rb;
    [SerializeField] GroundVariables groundVariables;
    [SerializeField] SlapMechanics slapInfo;
    [SerializeField] GameObject playerSprite;
    [System.Serializable]
    class GroundVariables
    {
        public Vector2 groundPoint;
        public Vector2 groundRadius;
        public LayerMask groundMask;
    }

    [System.Serializable]
    class SlapMechanics
    {
        public Slapdata normalSlap;
        public Slapdata heavySlap;
        public float midAirDashSpeed;
        public Vector2 forwardLook;
        public float horizontalForwardLookDistance;
        public Transform raycastPos;

        //public RaycastHit2D pontentialTargetToBeSpanked;
    }

    bool isAttacking;
    [SerializeField]
    Animator anim;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void Update()
    {
        base.Update();
        anim.SetBool("Grounded", GroundCheck());
        if (Input.GetButtonDown(smolSlapButtonSrc))
        {
            OnNormalAttack();
        }
        if (Input.GetButtonDown(bigSlapButtonSrc))
        {
            OnHeavyAttack();
        }

    }

    bool Jump()
    {
        return Input.GetButton(jumpButtonSrc);
    }

    private GameObject GetSlapRay()
    {
        return (!Physics2D.Raycast(new Vector2((slapInfo.raycastPos.position.x + slapInfo.horizontalForwardLookDistance), slapInfo.raycastPos.position.y), slapInfo.forwardLook, 0.5f)) ? GameObject.Find("SetupStage Caller") : Physics2D.Raycast(new Vector2((slapInfo.raycastPos.position.x + slapInfo.horizontalForwardLookDistance), slapInfo.raycastPos.position.y), slapInfo.forwardLook, 0.5f).collider.gameObject;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public override void Slap(Slapdata slapdata, GameObject slapOrigin)
    {
        base.Slap(slapdata, slapOrigin);

    }

    bool GroundCheck()
    {
        bool fuk = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y) + groundVariables.groundPoint, groundVariables.groundRadius, 0, groundVariables.groundMask);
        anim.SetBool("Grounded", fuk);
        return fuk;
    }
    void OnHeavyAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetBool("Attackin", true);
            StartCoroutine(setFalseAttack(1));
            if (GetSlapRay() == null || GetSlapRay().GetComponent<ISlappable>() == null)
            {
                return;
            }
            if (GetSlapRay().GetComponent<ISlappable>() != null)
            {
                GetSlapRay().GetComponent<ISlappable>().Slap(slapInfo.heavySlap, gameObject);
                Debug.Log("Spanked someone!" + GetSlapRay().transform.name);
            }
            else
            {
                return;
            }
        }
    }
    void OnNormalAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetBool("Attacking", true);
            StartCoroutine(setFalseAttack(.6f));
            if (GetSlapRay() == null || GetSlapRay().GetComponent<ISlappable>() == null)
            {
                return;
            }
            if (GetSlapRay().GetComponent<ISlappable>() != null)
            {
                GetSlapRay().GetComponent<ISlappable>().Slap(slapInfo.normalSlap, gameObject);
                Debug.Log("Slapped someone!" + GetSlapRay().transform.name);
            }
            else
            {
                return;
            }
        }
    }

    IEnumerator setFalseAttack(float t)
    {
        yield return new WaitForSeconds(t);
        anim.SetBool("Attacking", false);
        isAttacking = false;
    }

    void Movement()
    {
        anim.SetInteger("MoveDir", (joy1X < 0) ? -1 : (joy1X == 0) ? 0 : 1);
        Vector2 direction = new Vector3(joy1X, 0);
        if (joy1X != 0)
        {
            slapInfo.forwardLook = transform.right;
        }
        //SpriteHandler.OnDirectionFlip (playerSprite.transform, joy1X);
        if (!isSlapStunned)
        {
            rb.velocity = (direction.normalized * xAxisMovementSpeed) + new Vector2(0, rb.velocity.y);
        }

        if (Jump() && GroundCheck())
        {
            //Debug.Log ("jumpbuttom");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            return;
        }
        //Debug.Log (GroundCheck ());
        if (!joystickJump)
        {
            return;
        }
        if (joy1Y < 0 && GroundCheck())
        {
            rb.velocity = new Vector2(rb.velocity.x, -joy1Y * jumpForce);
            return;
        }
        //Debug.Log (buttonJump);

    }

    void GizmoDraw()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y) + groundVariables.groundPoint, groundVariables.groundRadius);
    }

    void GizmoDrawHitBox()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Vector2((slapInfo.raycastPos.position.x + slapInfo.horizontalForwardLookDistance), slapInfo.raycastPos.position.y), slapInfo.forwardLook * 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        GizmoDraw();
        GizmoDrawHitBox();
    }
}