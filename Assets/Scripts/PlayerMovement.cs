using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundmask;

    [HideInInspector]
    public Vector3 move;
    Vector3 velocity;
    public bool isGrounded;

    [Header("Crouching")]
    public string crouchKey = "left shift";
    public bool isCrouching = false;
    public Transform ceilingCheck;
    public float ceilingDistance = 0.4f;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(crouchKey))
        {
            Crouch();
        }
    }

    void ActivateCrouching()
    {
        isCrouching = true;
        speed /= 2;
        transform.localScale = new Vector3(
                transform.localScale.x,
                transform.localScale.y / 2,
                transform.localScale.z
            );
    }

    void DisableCrouching()
    {
        isCrouching = false;
        speed *= 2;
        transform.localScale = new Vector3(
                transform.localScale.x,
                transform.localScale.y * 2,
                transform.localScale.z
            );
    }

    void Crouch()
    {
        if(isCrouching)
        {
            DisableCrouching();
            if (Physics.CheckSphere(ceilingCheck.position, ceilingDistance, groundmask))
            {
                ActivateCrouching();
            }
        }
        else
        {
            ActivateCrouching();
        }
    }
}