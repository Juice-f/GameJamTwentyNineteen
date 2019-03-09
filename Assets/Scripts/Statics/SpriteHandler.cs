using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHandler {

    public static void OnDirectionFlip (Transform spriteToFlip, float direction) {
        if (direction != 0) {
            spriteToFlip.localScale = (direction < 0) ? new Vector3 (-spriteToFlip.localScale.x, spriteToFlip.localScale.y, spriteToFlip.localScale.z) : new Vector3 (spriteToFlip.localScale.x, spriteToFlip.localScale.y, spriteToFlip.localScale.z);
        }
    }
}