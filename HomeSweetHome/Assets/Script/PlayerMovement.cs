using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    public bool freeze;
    public bool activeGrapple;


    float horizontalInput;
    float veritcalInput;
    private Vector2 moveInputValue;

    Vector3 moveDirection;

    Rigidbody rb;

    Target targetScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
         targetScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Target>();

    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        //handle drag
        if (grounded && !activeGrapple)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (freeze)
        {
            rb.velocity = Vector3.zero;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            SpearAttack(hitBoxes[0]);
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnMove(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
        Debug.Log(moveInputValue);
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        veritcalInput = Input.GetAxisRaw("Vertical");
        

        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        if (activeGrapple) return;
        //calculate movement direction
        moveDirection = orientation.forward * veritcalInput + orientation.right * horizontalInput;

        //on ground
        if(grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        
        //in air
        else if (!grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;
    }

    private void SpeedControl()
    {
        if (activeGrapple) return;
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    public void ResetRestrictions()
    {
        activeGrapple = false;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<Grappling>().StopGrapple();
        }
    }

    private bool enableMovementOnNextTouch;
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;
        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

    public Collider[] hitBoxes;

    
    private void SpearAttack(Collider col)
    {
        var cols = Physics.OverlapBox(col.bounds.center,col.bounds.extents,col.transform.rotation,LayerMask.GetMask("HitBox"));
        foreach(Collider c in cols)
        {
            if(c.transform.root == transform)
                continue;
            

            float damage = 0;
            switch(c.name)
            {
            case "Body":
            damage = 10;
            break;

            default:
            Debug.Log("Body not found");
            break;
            }

            targetScript.TakeDamage(damage);
            
        }
        
    }

    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void ResetJump()
    {
        readyToJump = true;
    }
}
