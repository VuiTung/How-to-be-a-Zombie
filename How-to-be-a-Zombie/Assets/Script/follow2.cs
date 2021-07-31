using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
public class follow2 : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float movementSpeed;
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _rigidbody2;
    private Vector2 colliderSize;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float slopeCheckDistance;
    private bool isOnSlope;
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;
    private Vector2 slopeNormalPerp;
    private bool canWalkOnSlope;
    [SerializeField]
    private float maxSlopeAngle;
    private bool isGrounded;
    private float groundCheckRadius = .2f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform yposi;
    [SerializeField]
    private Transform ypositarget;
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;
    private Vector2 newVelocity;
    private float xInput;
    private int facingDirection = 1;
    private int position =1;
    private float randomnum;
    private float randomnum2;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody2 = GetComponent<CapsuleCollider2D>();
        colliderSize = _rigidbody2.size;
        randomnum = Random.Range(0.5f, 1.0f);
        randomnum2 = Random.Range(-0.2f, 0.4f);
    }
    private void CheckGround()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;

            }
        }


    }
    private void slopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y * 1.6f);

        slopeCheckHorizontal(checkPos);
        slopeCheckVertical(checkPos);
    }
    private void slopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }
    private void slopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);

        if (hit)
        {
            
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != lastSlopeAngle)
            {
                
                isOnSlope = true;
            }

            lastSlopeAngle = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }
        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle && slopeSideAngle < 87)
        {
           
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }
        //here need put is moving
        if (isOnSlope && canWalkOnSlope && xInput == 0.0f)
        {
            _rigidbody.sharedMaterial = fullFriction;
        }
        else
        {   
            _rigidbody.sharedMaterial = noFriction;

        }
    }
    private void CheckDirection(Vector3 vec)
    {

        if (vec.x - transform.position.x <-0.1 && facingDirection == 1)
        {
            Flip();
        }
        else if(vec.x - transform.position.x >= 0 && facingDirection == -1)
        {
            Flip();
        }

    }
    
    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (xInput == 1 && facingDirection == -1)
        {
            Flip();
        }
        else if (xInput == -1 && facingDirection == 1)
        {
            Flip();
        }


    }
    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);


    }
    private Vector3 changeposition()
    {
        switch (position)
        {
            case 1:
                return new Vector3(randomnum, 0, 0);
                
            case 2:
                return new Vector3(randomnum2, 0, 0);
                
            case 3:
                return new Vector3(-1*(randomnum), 0, 0);
            default:
                return new Vector3(randomnum, 0, 0);
        }
        
    }
    private void checkgangposition()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Q pressed");
            position += 1;
            if (position > 3)
            {
                position = 3;
            }
            Debug.Log("position " + position);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("E pressed");
            position -= 1;
            if (position < 1)
            {
                position = 1;
            }
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        CheckGround();
        slopeCheck();
        checkgangposition();
        if (!isOnSlope && isGrounded) //if not on slope
        {

            Vector3 myVector = new Vector3(target.position.x, transform.position.y, transform.position.z) - changeposition();
            CheckDirection(myVector);
            transform.position = Vector3.MoveTowards(transform.position, myVector, movementSpeed * Time.deltaTime);

        }
        else if (isGrounded && isOnSlope && canWalkOnSlope) //If on slope

        {
           
            newVelocity.Set(5f * slopeNormalPerp.x * -xInput, 5f * slopeNormalPerp.y * -xInput);
            CheckInput();
            _rigidbody.velocity = newVelocity;
            //Vector3 myVector = new Vector3(target.position.x * slopeNormalPerp.x, target.position.y * slopeNormalPerp.y, transform.position.z) - new Vector3(1, 0, 0);
            //transform.position = Vector3.MoveTowards(transform.position, myVector, movementSpeed * Time.deltaTime);


        }

        //else if (isGrounded && isOnSlope && !canWalkOnSlope)//when on top of the slope 
        //{
        //    newVelocity.Set(movementSpeed * xInput, 0.0f);
        //    _rigidbody.velocity = newVelocity;
        //}
        
        
    }
}
