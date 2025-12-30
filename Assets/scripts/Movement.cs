using System;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    private float inputHorizontal;
    private Vector2 mousePosition;



    private Rigidbody2D rb;
    RaycastHit2D ray;
    public float groundOffset = 0.11f;
    public float rayDistance = 0.11f;



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

        // if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        // {
        //     mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
        //     rb.AddForce(mousePosition * jumpForce, ForceMode2D.Impulse);
        // }


    }

    void FixedUpdate()
    {
        if (inputHorizontal != 0)
        {
            var tangent = Vector3.Cross(ray.normal, transform.forward); 
            Vector2 tangentV2 = tangent;
            rb.linearVelocity =  tangentV2 * inputHorizontal * movementSpeed * Time.fixedDeltaTime;
        }
        if (isGrounded)
            SnapToGround();
    }

    void GroundCheck()
    {
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, -transform.up, rayDistance, LayerMask.GetMask("Ground"));

        ray = rayDown;

        Debug.DrawRay(transform.position, -transform.up * rayDistance, Color.green);

        if (ray.collider != null)
        {
            isGrounded = true;
            rb.gravityScale = 0;

        }
        else
        {
            isGrounded = false;
            rb.gravityScale = 1;
        }
    }

    void SnapToGround()
    {
        if (ray.collider != null)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, ray.normal);
            transform.rotation =  Quaternion.Lerp(transform.rotation, rotation, 0.5f);



            transform.position = ray.point + ray.normal * groundOffset;

            // rb.MoveRotation(Quaternion.LookRotation(Vector3.forward, ray.normal));
            // rb.MovePosition(ray.point + ray.normal * groundOffset);
        }
    }


}
