using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField]
    public float MoveSpeed { get; private set; } = 3;

    [field: SerializeField]
    public Transform Orientation { get; private set; }

    [field: SerializeField]
    public float GroundDrag { get; private set; } = 5;

    [field: SerializeField]
    public float JumpForce { get; private set; } = 3;
    
    [field: SerializeField]
    public float AirMultiplier { get; private set; } = 3;
    
    [field: SerializeField]
    public KeyCode JumpKey { get; private set; } = KeyCode.Space;

    [field: SerializeField]
    public KeyCode SpeedupKey { get; private set; } = KeyCode.LeftShift;

    [field: SerializeField]
    public float PlayerHeight { get; private set; }
    
    [field: SerializeField]
    public LayerMask Ground { get; private set; }

    public Rigidbody RigidBody { get; private set; }

    public Vector3 MoveDirection { get; private set; }

    private float horizontalInput;
    private float verticalInput;

    private IGroundedState groundedState;

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody>();
        RigidBody.freezeRotation = true;
    }

    private void Update()
    {
        bool grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, Ground);

        groundedState = grounded ? new GroundedState(this) : new JumpingState(this);

        MyInput();
        MovePlayer();
        SpeedControl();

        RigidBody.drag = groundedState.GetDrag();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(JumpKey))
            groundedState.Jump();
    }

    private void MovePlayer()
    {
        MoveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;

        var force = groundedState.GetForce();
        RigidBody.AddForce(force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new(RigidBody.velocity.x, 0, RigidBody.velocity.z);

        if (flatVel.magnitude > MoveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * MoveSpeed;
            RigidBody.velocity = new(limitedVel.x, RigidBody.velocity.y, limitedVel.z);
        }
    }
}
