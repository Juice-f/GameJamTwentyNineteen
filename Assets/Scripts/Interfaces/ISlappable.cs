using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlappable
{
    void Slap(Slapdata slapdata,  GameObject slapOrigin);
}

[System.Serializable]
public class Slapdata
{

    public float slapForce;
    public float damage;
    public float stunTime;


    public Slapdata(float slapForce, float damage , float stunTime)
    {
        this.slapForce = slapForce;
        this.damage = damage;
        this.stunTime = stunTime;
    }
}