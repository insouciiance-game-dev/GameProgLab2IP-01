using UnityEngine;

public class JumpingState : IGroundRelativeState
{
    private readonly PlayerMovement movement;

    public JumpingState(PlayerMovement movement) => this.movement = movement;

    public void Jump() { }

    public float GetDrag() => 0;

    public Vector3 GetForce()
    {
        float moveSpeed = movement.MoveSpeed;

        if (Input.GetKey(movement.SpeedupKey))
            moveSpeed *= 2;

        return 10 * moveSpeed * movement.AirMultiplier * movement.MoveDirection.normalized;
    }
}
