using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.LightTransport;

public static class Steering
{
    // "Seek is a static method that can be applied to any GameObject" -- Done for you here
    public static Vector2 Seek(Rigidbody2D seeker, Vector2 target, float moveSpeed, float turnSpeed)
    {
        // 2% -- Output seeking force (smooth acceleration leading to curved motion)
        Vector2 desiredVelocity = (target - seeker.position).normalized * moveSpeed;
        Vector2 seekForce = desiredVelocity - seeker.linearVelocity;

        // 2% -- Use seeker.MoveRotation to rotate the seeker towards its velocity
        // (Instead of rotating towards the mouse cursor, you need to rotate it towards its velocity direction)
        float targetAngle = Mathf.Atan2(seeker.linearVelocity.y, seeker.linearVelocity.x) * Mathf.Rad2Deg;
        seeker.MoveRotation(targetAngle);

        Vector3 direction = Quaternion.Euler(0.0f, 0.0f, seeker.rotation) * Vector3.right;
        Debug.DrawLine(seeker.position, new Vector3(seeker.position.x, seeker.position.y, 0.0f) + direction * Vector2.Distance(seeker.position, target));

        return seekForce;
    }
}
