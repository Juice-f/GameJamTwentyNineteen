﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoDo : CharacterController
{

    Rigidbody2D rb;

    [SerializeField]
    GameObject slapSource;

    Animator animator { get => GetComponent<Animator>(); }

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
    bool isAttacking = false;
    bool IsAttacking
    {
        set { isAttacking = value; animator.SetBool("IsAttacking", value); }
        get => isAttacking;
    }

    [SerializeField]
    DoDoHitBox smalSlapBox, bigSlapBox;
    [SerializeField]
    DoDoHitBox[] smalSlapModified;

    [SerializeField]
    GameObject fistDownSpawn;
    [SerializeField]
    ElHando handObject;
    [SerializeField]
    float handoMoveSpd;
    [SerializeField]
    Slapdata handoSlapData = new Slapdata(0, 0, 0);



    [SerializeField]
    ElHandoStando handoStando;
    [SerializeField]
    GameObject handoStandoSpawn;
    [SerializeField]
    Slapdata handoStandoSlap;
    [SerializeField]
    float handoStandoLifeTime, handoStandoMoveSpeed, reslap;

    bool HoldingModifier
    {
        get
        {
            if (Input.GetButton(gimmickButtonSrc))
            {
                return true;
            }
            else return false;
        }
    }

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
            Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + groundCheckOffset), groundCheckBoxSize, 0,    groundedLayers);

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
        if (Input.GetButtonUp(jumpButtonSrc))
        {
            EndJump();
        }
        if (IsGrounded)
        {
         //   Debug.Log("heck");
            int num = (joy1X < 0) ? -1 : (joy1X > 0) ? 1 : 0;
            animator.SetInteger("MoveDirection", num);
            animator.SetBool("IsGrounded", true);
        }
        else
        {
            animator.SetBool("IsGrounded", false);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            int num = (joy1X < 0) ? -1 : (joy1X > 0) ? 1 : 0;
            //int num = (rb.velocity.x < 0) ? -1 : (rb.velocity.x > 0) ? 1 : 0;
            animator.SetInteger("MoveDirection", num);
        }

        if (Input.GetButtonDown(smolSlapButtonSrc))
        {
            if (!isSlapStunned && !IsAttacking)
            {
                StartCoroutine(SmallSlap());
            }
        }
        if (Input.GetButtonDown(bigSlapButtonSrc))
        {
            if (!isSlapStunned && !IsAttacking)
            { StartCoroutine(BigSlap()); }
        }

    }
    IEnumerator SmallSlap()
    {
        IsAttacking = true;
        ElHandoStando stand = Instantiate(handoStando);
        stand.transform.position = handoStandoSpawn.transform.position;
        float movSpd = (HoldingModifier) ? movSpd = handoStandoMoveSpeed : 0;
        stand.StartSlappin(handoStandoLifeTime, handoStandoSlap, gameObject, reslap, movSpd);
        yield return new WaitForSeconds(.4f);
        IsAttacking = false;
    }
    IEnumerator BigSlap()
    {

        IsAttacking = true;
        ElHando elHando = Instantiate(handObject);
        elHando.transform.position = fistDownSpawn.transform.position;
        elHando.moveDir = (!HoldingModifier) ? new Vector2(0, -1) : new Vector2(1, -1);
        elHando.moveSpeed = handoMoveSpd;
        elHando.IgnoreThisOne = gameObject;
        elHando.slapdata = handoSlapData;
        yield return new WaitForSeconds(.8f);
        IsAttacking = false;


    }


    public override void Start()
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
            if (!isSlapStunned)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
            jumpTimeLeft = 0;
        }
        //Debug.Log("LET GO OF JUMP");
    }

    public virtual void HorizontalMovement(float input)
    {
        if (IsAttacking) input = 0;
        //   Boy.Flip(transform, input);
        // if (Input.GetAxis(gimmickButtonSrc) > 0) { input *= sprintMultiplier; }
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
        Slap(new Slapdata(100, 10, 1), slapSource);
    }


    public override void Slap(Slapdata slapdata, GameObject slapOrigin)
    {
        base.Slap(slapdata, slapOrigin);
        //     isSlapStunned = true;




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

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(smalSlapBox.boxXOffset, smalSlapBox.boxYOffset), smalSlapBox.BoxDimensionsV3);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(bigSlapBox.boxXOffset, bigSlapBox.boxYOffset), bigSlapBox.BoxDimensionsV3);
        Gizmos.color = Color.cyan;
        for (int i = 0; i < smalSlapModified.Length; i++)
        {
            Gizmos.DrawWireCube(transform.position + new Vector3(smalSlapModified[i].boxXOffset, smalSlapModified[i].boxYOffset), smalSlapModified[i].BoxDimensionsV3);
        }
        

    }
#endif

}

[System.Serializable]
public class DoDoHitBox
{
    // public string name;
    [SerializeField]
    [Range(0, 5)]
    public float boxXOffset, boxYOffset;
    // [Range(0,2)]
    public Vector2 boxDimensions = new Vector2(1, 1);
    public Vector3 BoxDimensionsV3 { get { return new Vector3(boxDimensions.x, boxDimensions.y, 0); } }


}
