using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    private float inputHorizontal;
    private bool isJumped;

    private CircleCollider2D circleCollider2D;
    private float colliderRadius;
    private Rigidbody2D rb;
    RaycastHit2D ray;
    public float groundOffset = 0.11f;



    [SerializeField] bool isGrounded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        GroundCheck();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            isJumped = true;
        }

        // if (isGrounded && isJumped == false)
        // {
        //     GroundRayCast();
        // }



    }

    void FixedUpdate()
    {
        if (inputHorizontal != 0)
        {
            rb.linearVelocityX = inputHorizontal * movementSpeed * Time.fixedDeltaTime;
            SnapToGround();
        }
    }

    void GroundCheck()
    {
        ray = Physics2D.Raycast(transform.position, -transform.up, groundOffset, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, -transform.up * groundOffset, Color.red);
        if (ray.collider != null)
        {
            isGrounded = true;
            rb.gravityScale = 0;
            isJumped = false;

        }
        else
        {
            isGrounded = false;
            rb.gravityScale = 1;
        }
    }

    void SnapToGround()
    {
        if (isGrounded)
        {
            if (ray.collider != null)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, ray.normal);
                transform.position = ray.point + ray.normal * groundOffset;
            }
        }

    }


    // private void GroundRayCast()
    // {
    //     Debug.DrawRay(transform.position, -transform.up * 0.3f, Color.red);


    //     Debug.Log(ray.point);
    //     Debug.Log(ray.normal);

    //     // ?????????????????????????????????????????
    //     // Vector2 v = rb.linearVelocity;
    //     // v -= Vector2.Dot(v, ray.normal) * ray.normal;
    //     // rb.linearVelocity = v;


    // }
    //     else
    //     {
    //         rb.gravityScale = 1;
    //     }
    // }




    // private bool GroundCheck()
    // { 
    //     if( 
    // }
}
