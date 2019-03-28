using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionUser : MonoBehaviour
{
    protected Collider2D currentPlatformCollider;

    protected Platform currentPlatform;

    [Header("Player Controller")]
    [SerializeField]
    protected Controller2D controller;

    public abstract bool IgnoreCollisions(RaycastHit2D hit, float direction = 0, bool isCrouching = false);

    protected void Initialise() {

        controller = GetComponent<Controller2D>();

        controller.ignoringCollisions += IgnoreCollisions;

        controller.onCollision += CheckPlatformCollider;
    }

    public void CheckPlatformCollider(RaycastHit2D hit) {

        if (hit.collider != currentPlatformCollider) {

            currentPlatformCollider = hit.collider;
            currentPlatform = hit.transform.GetComponent<Platform>();
        }
    }
}
