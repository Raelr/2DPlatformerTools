﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour {

    public float Gravity { get { return gravity; } }

    public float MoveSpeed { get { return moveSpeed; } }

    const float skinWidth = 0.015f;

    [Header ("Collision Mask")]
    [SerializeField]
    LayerMask layerMask;

    [Header ("RayCast Counts")]
    [SerializeField]
    public int horizontalRayCount;

    [SerializeField]
    public int verticalRayCount;

    [Header ("RayCast Spaces")]
    [SerializeField]
    [ReadOnly] float horizontalRaySpacing;

    [SerializeField]
    [ReadOnly] float verticalRaySpacing;

    [Header("Physics Configuration")]

    [SerializeField]
    float gravity = -20;

    [SerializeField]
    float moveSpeed = 3;

    BoxCollider2D boxCollider;

    RayCastOrgins rayCastOrigins;
	
	void Start () {

        boxCollider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();

    }

    public void Move(Vector3 velocity) {

        UpdateRayCastOrigins();

        if (velocity.x != 0) {
            HorizontalCollisions(ref velocity);
        }
        
        if (velocity.y != 0) {
            VerticalCollisions(ref velocity);
        }
        
        transform.Translate(velocity);
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
}




