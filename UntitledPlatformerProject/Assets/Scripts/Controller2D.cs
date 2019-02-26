using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour {

    const float skinWidth = 0.015f;

    [SerializeField]
    public int horizontalRayCount;

    [SerializeField]
    public int verticalRayCount;

    [SerializeField]
    [ReadOnly] float horizontalRaySpacing;

    [SerializeField]
    [ReadOnly] float verticalRaySpacing;

    BoxCollider2D boxCollider;

    RayCastOrgins rayCastOrigins;
	
	void Start () {

        boxCollider = GetComponent<BoxCollider2D>();

	}

	void Update () {

        UpdateRayCastOrigins();
        CalculateRaySpacing();

        for (int i = 0; i < verticalRayCount; i++) {
            Debug.DrawRay(rayCastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.red);
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




