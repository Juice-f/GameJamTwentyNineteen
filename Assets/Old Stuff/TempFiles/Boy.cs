using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy 
{
    public static void Flip (Transform theTransform, float dir)
    {
        theTransform.localScale = (dir < 0) ? new Vector3(-1, theTransform.localScale.y, theTransform.localScale.z) : new Vector3(1, theTransform.localScale.y, theTransform.localScale.z);
    }
}
