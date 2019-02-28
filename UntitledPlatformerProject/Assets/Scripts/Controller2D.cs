using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour {

    public float Gravity { get { return gravity; } }

    public float MoveSpeed { get { return moveSpeed; } }

    public float JumpVelocity { get { return jumpVelocity; } }

    public float JumpHeight { get { return jumpHeight; } }

    public float TimeToJumpApex { get { return timeToJumpApex; } }

    public float VelocityXSmoothing { get { return velocityXSmoothing; } }

    public float VelocityYSmoothing { get { return velocityYSmoothing; } }

    public float AccelerationTimeAirborn { get { return accelerationTimeAirborn; } }

    public float AccelerationTimeGrounded { get { return accelerationTimeGrounded; } }

    const float skinWidth = 0.015f;

    [Header("Collision Mask")]
    [SerializeField]
    LayerMask layerMask;

    [Header("RayCast Counts")]
    [SerializeField]
    public int horizontalRayCount;

    [SerializeField]
    public int verticalRayCount;

    [Header("RayCast Spaces")]
    [SerializeField]
    [ReadOnly] float horizontalRaySpacing;

    [SerializeField]
    [ReadOnly] float verticalRaySpacing;

    [Header("Physics Configuration")]

    [SerializeField]
    float moveSpeed = 3;

    [SerializeField]
    float jumpHeight;

    [SerializeField]
    float timeToJumpApex;

    [SerializeField]
    float accelerationTimeAirborn;

    [SerializeField]
    float accelerationTimeGrounded;

    [Header ("Physics Results")]
    [SerializeField]
    [ReadOnly] float jumpVelocity;

    [SerializeField]
    [ReadOnly] float gravity;

    float velocityXSmoothing;

    float velocityYSmoothing;

    BoxCollider2D boxCollider;

    RayCastOrgins rayCastOrigins;

    public CollisionInformation collisionInformation;

    Vector3 velocity;
	
	void Start () {

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        boxCollider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();

    }

    public void Move(Vector3 input) {

        UpdateRayCastOrigins();
        collisionInformation.Reset();

        if (input.x != 0) {
            HorizontalCollisions(ref input);
        }
        
        if (input.y != 0) {
            VerticalCollisions(ref input);
        }
        
        transform.Translate(input);
    }

    public void MoveHorizontal(Vector3 input) {

        if (collisionInformation.isAbove || collisionInformation.isBelow) {
            velocity.y = 0;
        }

        float targetVelocityX = input.x * MoveSpeed;
        float xSmoothing = VelocityXSmoothing;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref xSmoothing, collisionInformation.isBelow ? AccelerationTimeGrounded : AccelerationTimeAirborn);
        velocity.y += Gravity * Time.deltaTime;
        Move(velocity * Time.deltaTime);
    }

    public void ApplyGravity() {

        velocity.y += Gravity * Time.deltaTime;
        Move(velocity * Time.deltaTime);
    }

    public void Jump() {

        velocity.y = jumpVelocity;
        
        Move(velocity * Time.deltaTime);
    }

    void HorizontalCollisions(ref Vector3 velocity) {

        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;
        

        for (int i = 0; i < horizontalRayCount; i++) {

            Vector2 rayOrigin = (directionX == -1) ? rayCastOrigins.bottomLeft : rayCastOrigins.bottomRight;

            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, layerMask);

            if (hit) {

                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisionInformation.isLeft = directionX == -1;
                collisionInformation.isRight = directionX == 1;
            }

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);
        }
    }

    void VerticalCollisions(ref Vector3 velocity) {

        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++) {

            Vector2 rayOrigin = (directionY == -1) ? rayCastOrigins.bottomLeft : rayCastOrigins.topLeft;

            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, layerMask); 

            if (hit) {

                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisionInformation.isBelow = directionY == -1;
                collisionInformation.isAbove = directionY == 1;
            }

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
        }
    }

    void UpdateRayCastOrigins() {

        Bounds bounds = boxCollider.bounds;

        CalculateColliderBounds(ref bounds);

        rayCastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        rayCastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rayCastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        rayCastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    Bounds CalculateColliderBounds(ref Bounds bounds) {

        bounds.Expand(skinWidth * -2);

        return bounds;
    }

    void CalculateRaySpacing() {

        Bounds bounds = boxCollider.bounds;

        CalculateColliderBounds(ref bounds);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    struct RayCastOrgins {

        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInformation {
        public bool isBelow, isAbove;
        public bool isRight, isLeft;

        public void Reset() {
            isBelow = isAbove = false;
            isRight = isLeft = false;
        }
    }
}




