using UnityEngine;

public class GroundedState : IGroundedState
{
    private readonly PlayerMovement movement;

    public GroundedState(PlayerMovement movement) => this.movement = movement;

    public void Jump()
    {
        movement.RigidBody.velocity = new(movement.RigidBody.velocity.x, 0, movement.RigidBody.velocity.z);

        movement.RigidBody.AddForce(movement.transform.up * movement.JumpForce, ForceMode.Impulse);
    }

    public float GetDrag() => movement.GroundDrag;

    public Vector3 GetForce()
    {
        float moveSpeed = movement.MoveSpeed;

        if (Input.GetKey(movement.SpeedupKey))
            moveSpeed *= 2;

        return 10 * moveSpeed * movement.MoveDirection.normalized;
    }
}
