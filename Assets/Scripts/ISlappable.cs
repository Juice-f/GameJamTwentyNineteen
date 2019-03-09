using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlappable
{
    void Slap(float slapForce, float damage, GameObject slapOrigin);
}
