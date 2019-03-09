using UnityEngine;

public class DoDo : CharacterController
{

    Rigidbody2D rb;



    [SerializeField]
    LayerMask groundedLayers;
    [SerializeField]
    float moveSpeed, jumpTime, jumpSpeed;
    [SerializeField]
    bool canHangOnWalls = false;
    [SerializeField]
    Vector2 wallCheckBoxLeft, wallCheckBoxRight;
    [SerializeField]
    [Range(-5, 5)]
    float wallCheckBoxLeftOffset, wallCheckBoxRightOffset;

    float jumpTimeLeft;
    //[SerializeField]
    //string xAxisInputSrc = "Horizontal", yAxisInputSrc = "Vertical", jumpInput = "Jump", sprintInput = "Sprint";
    [Range(0, 1)]
    [SerializeField]
    float xSlowDown;
    [Range(0, 1)]
    public float currentXSlowDown;
    public float sprintMultiplier = 2;

    [SerializeField]
    float groundCheckOffset;
    [SerializeField]
    Vector2 groundCheckBoxSize;
    bool jumped = true;

    bool TouchingLeft
    {
        get
        {
            // Debug.Log("Checking left");
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + wallCheckBoxLeftOffset, transform.position.y), wallCheckBoxLeft, 0);
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                if (collider2Ds[i].gameObject != gameObject)
                {
                    //       Debug.Log("Touching");
                    return true;
                }
            }
            //Debug.Log("Not touching");
            return false;
        }
    }

    bool TouchingRight
    {
        get
        {
            //  Debug.Log("Checking right");
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + wallCheckBoxRightOffset, transform.position.y), wallCheckBoxRight, 0);
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                if (collider2Ds[i].gameObject != gameObject)
                {
                    return true;
                }
            }
            return false;
        }
    }



    bool IsGrounded
    {
        get
        {
            Collider2D[] collider2Ds =
            Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + groundCheckOffset), groundCheckBoxSize, 0);

            for (int i = 0; i < collider2Ds.Length; i++)
            {
                // Debug.Log(collider2Ds[i].name);
                if (collider2Ds[i].gameObject != gameObject)
                {
                    //Debug.Log(collider2Ds[i].name);
                    return true;
                }
                //   else Debug.Log("Collider me");
            }
            return false;

        }
    }

    public virtual void SetupComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        currentXSlowDown = xSlowDown;
    }


    public virtual void FixedUpdate()
    {
        if (!isSlapStunned)
        {
            HorizontalMovement(joy1X);
            if (Input.GetButton(jumpButtonSrc)) JumpAction();
        }



    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetButtonUp(jumpButtonSrc) && !isSlapStunned)
        {
            EndJump();
        }


    }

    public virtual void Start()
    {
        SetupComponents();
    }

    public virtual void JumpAction()
    {
        if (IsGrounded)
        {
            jumpTimeLeft = jumpTime;
            //  Debug.Log("Beginning jump");
        }
        float vel = Mathf.Clamp((jumpTimeLeft / jumpTime) * jumpSpeed, 0, jumpSpeed);
        if (vel >= rb.velocity.y && vel > 0)
            rb.velocity = new Vector2(rb.velocity.x, vel);
        // Debug.Log(Mathf.Clamp(((jumpTimeLeft / jumpTime) * jumpForce), 0, jumpForce));
        jumpTimeLeft -= Time.deltaTime;

    }
    public virtual void EndJump()
    {
        if (jumpTimeLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
            jumpTimeLeft = 0;
        }
        //Debug.Log("LET GO OF JUMP");
    }

    public virtual void HorizontalMovement(float input)
    {
        if (Input.GetAxis(gimmickButtonSrc) > 0) { input *= sprintMultiplier; }
        // Debug.Log(input);
        if (input != 0)
        {
            if (!canHangOnWalls && !IsGrounded)
            {
                if (input < 0)
                {
                    if (!TouchingLeft) rb.velocity = new Vector2(moveSpeed * input, rb.velocity.y);
                }
                else
                {
                    if (!TouchingRight)
                        rb.velocity = new Vector2(moveSpeed * input, rb.velocity.y);
                }
            }
            else rb.velocity = new Vector2(moveSpeed * input, rb.velocity.y);
        }


        else { rb.velocity = new Vector2(rb.velocity.x * currentXSlowDown, rb.velocity.y); }
    }

    private void OnMouseDown()
    {
        Slap(new Slapdata(10, 10, 10), null);
    }


    public override void Slap(Slapdata slapdata, GameObject slapOrigin)
    {
        base.Slap(slapdata, slapOrigin);
        Vector3 slapOriginPosition = (slapOrigin != null) ? slapOrigin.transform.position : transform.position -= new Vector3(0, -1, 0);
        Vector3 slapDir = (slapOriginPosition - transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0,50));



    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, groundCheckOffset, 0), new Vector3(groundCheckBoxSize.x, groundCheckBoxSize.y, 0));
        if (!canHangOnWalls)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + new Vector3(wallCheckBoxLeftOffset, 0), new Vector3(wallCheckBoxLeft.x, wallCheckBoxLeft.y, 0));
            Gizmos.DrawWireCube(transform.position + new Vector3(wallCheckBoxRightOffset, 0), new Vector3(wallCheckBoxRight.x, wallCheckBoxRight.y, 0));
        }
    }
#endif

}
