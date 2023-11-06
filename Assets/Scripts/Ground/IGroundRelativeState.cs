using UnityEngine;

public interface IGroundRelativeState
{
    void Jump();

    Vector3 GetForce();

    float GetDrag();
}
