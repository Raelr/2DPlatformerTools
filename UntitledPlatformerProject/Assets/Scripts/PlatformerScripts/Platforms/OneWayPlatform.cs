using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : Platform {

    public override bool AllowedToJumpThrough(float direction, bool isCheckingDirection) {

        bool allowed = isCheckingDirection ? platformType == PlatformType.CanJumpThrough && direction == 1 : platformType == PlatformType.CanJumpThrough;

        return allowed;
    }

    public override bool CanFallThrough() {
        return true;
    }
}
