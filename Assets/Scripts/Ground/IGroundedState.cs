using UnityEngine;

public interface IGroundedState
{
    void Jump();

    Vector3 GetForce();

    float GetDrag();
}
