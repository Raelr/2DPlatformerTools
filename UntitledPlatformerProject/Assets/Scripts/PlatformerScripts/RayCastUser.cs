using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastUser : MonoBehaviour {

    protected const float skinWidth = 0.015f;

    [Header("RayCast Counts")]
    [SerializeField]
    protected int horizontalRayCount;

    [SerializeField]
    protected int verticalRayCount;

    [Header("RayCast Spaces")]
    [SerializeField]
    [ReadOnly] protected float horizontalRaySpacing;

    [SerializeField]
    [ReadOnly] protected float verticalRaySpacing;

    [Header("Box Collider")]
    [SerializeField]
    protected BoxCollider2D boxCollider;

    protected RayCastOrgins rayCastOrigins;

    public virtual void Start() {

        boxCollider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }
    
    /// <summary>
    /// Realigns the raycast origin points to fit the player sprite.
    /// </summary>

    protected void UpdateRayCastOrigins() {

        Bounds bounds = boxCollider.bounds;

        // Resize the collider.
        CalculateColliderBounds(ref bounds);

        // Get the four corners of the sprite.
        rayCastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        rayCastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rayCastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        rayCastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    /// <summary>
    /// Resizes a collider so that the raycast can be thrown at an offset. 
    /// </summary>
    /// <param name="bounds">A reference to the player's collider bounds</param>
    /// <returns> Returns the resized collider </returns>

    protected Bounds CalculateColliderBounds(ref Bounds bounds) {

        // Shrink the bounds slightly to allow for the raycast to be thrown.
        bounds.Expand(skinWidth * -2);

        return bounds;
    }

    /// <summary>
    /// Determines what the spacing should be between each raycast being shot by the player. Scales with many simultaneous raycasts.
    /// </summary>

    protected void CalculateRaySpacing() {

        Bounds bounds = boxCollider.bounds;
        // Resize the collider.
        CalculateColliderBounds(ref bounds);

        // Clamp both values between 2 and any number.
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    /// <summary>
    /// Data structure used to store all raycast locations. 
    /// </summary>

    protected struct RayCastOrgins {

        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
