using UnityEngine;

public class MC : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    public float JumpForce = 8;
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _rigidbody2;
    private Vector2 colliderSize;
    [SerializeField]
    private float slopeCheckDistance;
    [SerializeField]
    private LayerMask whatIsGround;
    private float xInput;
    private int facingDirection = 1;
    private bool isOnSlope;
    private Vector2 newVelocity;
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;
    private Vector2 slopeNormalPerp;
    private bool canWalkOnSlope;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float maxSlopeAngle;
    private float groundCheckRadius = .2f;
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;
    private bool isGrounded;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody2 = GetComponent<CapsuleCollider2D>();
        colliderSize = _rigidbody2.size;
    }
    private void slopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y * 1.5f);

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

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle && slopeSideAngle < 89)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && canWalkOnSlope && xInput == 0.0f)
        {
            _rigidbody.sharedMaterial = fullFriction;
        }
        else
        {
            _rigidbody.sharedMaterial = noFriction;
        }
    }

    private void LateUpdate()

    {
        CheckInput();
        CheckGround();
        slopeCheck();
        ApplyMovement();

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
    private void ApplyMovement()
    {
        if (!isOnSlope && isGrounded) //if not on slope
        {

            newVelocity.Set(movementSpeed * xInput, 0.0f);
            _rigidbody.velocity = newVelocity;
        }
        else if (isGrounded && isOnSlope && canWalkOnSlope) //If on slope
        {

            newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput, movementSpeed * slopeNormalPerp.y * -xInput);
            _rigidbody.velocity = newVelocity;
        }
        //else if(isGrounded && isOnSlope && !canWalkOnSlope)//when on top of the slope 
        //{
        //    newVelocity.Set(movementSpeed * xInput, 0.0f);
        //    _rigidbody.velocity = newVelocity;
        //}
        Debug.Log("isonground"+ isGrounded);
        Debug.Log("isonslope "+ isOnSlope);
        Debug.Log("canwalkon slope" + canWalkOnSlope);
        Debug.Log("slopeNormalPerp" + slopeNormalPerp);
        Debug.Log("slopeDownAngle"+ slopeDownAngle);
        Debug.Log("lastSlopeAngle" + lastSlopeAngle);
        Debug.Log("slopeSideAngle" + slopeSideAngle);
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}