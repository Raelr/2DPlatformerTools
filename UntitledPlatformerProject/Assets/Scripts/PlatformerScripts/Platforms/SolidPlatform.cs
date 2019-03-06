using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidPlatform : Platform {

    public override bool AllowedToJumpThrough(float direction, bool isCheckingDIrection) {

        return platformType == PlatformType.Solid ? false : true;
    }

    public override bool CanFallThrough() {
        
        return platformType == PlatformType.Solid ? false : true;
    }
}
