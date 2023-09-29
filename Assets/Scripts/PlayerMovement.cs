using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Transform orientation;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;

    public KeyCode jumpKey = KeyCode.Space;

    public KeyCode speedupKey = KeyCode.LeftShift;

    public float playerHeight;
    public LayerMask ground;
    private bool grounded;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rigidBody;

    private void Start()
    {
        readyToJump = true;

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        MyInput();
        MovePlayer();
        SpeedControl();

        if (grounded)
            rigidBody.drag = groundDrag;
        else
            rigidBody.drag = 0;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        float moveSpeed = this.moveSpeed;

        if (Input.GetKey(speedupKey))
            moveSpeed *= 2;
        
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
        else
            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10 * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new(rigidBody.velocity.x, 0, rigidBody.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rigidBody.velocity = new(limitedVel.x, rigidBody.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rigidBody.velocity = new(rigidBody.velocity.x, 0, rigidBody.velocity.z);

        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
