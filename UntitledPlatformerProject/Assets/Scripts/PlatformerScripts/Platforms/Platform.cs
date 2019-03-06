using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour {

    public bool CanJumpThrough { get; set; }

    protected enum PlatformType {

        Solid,
        CanJumpThrough
    }

    [SerializeField]
    protected PlatformType platformType;

    public abstract bool AllowedToJumpThrough(float direction, bool isCheckingDirection = false);

    public abstract bool CanFallThrough();
}
